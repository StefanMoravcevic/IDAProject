using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Api.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Models.Dto.Employees;
using Microsoft.EntityFrameworkCore;

namespace IDAProject.Web.Api.Managers
{
    public class GoogleManager : IGoogleManager
    {
        private readonly IEmployeesRepository _employeeRepo;
        private readonly IRegularActivitiesRepository _regularActivitiesRepository;
        private readonly ITasksPlanningsRepository _tasksPlanningsRepository;
        private readonly ITasksRealizationsRepository _tasksRealizationsRepository;
        private readonly string REMOVED_SECRET = "232450643102-6pa08h9ihctl5scnpjsikqpngkat57e2.apps.googleusercontent.com";
        private readonly string REMOVED_SECRET = "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";

        public GoogleManager(IEmployeesRepository repo, ITasksPlanningsRepository tasksPlanningsRepository, IRegularActivitiesRepository regularActivitiesRepository, ITasksRealizationsRepository tasksRealizationsRepository)
        {
            _employeeRepo = repo;
            _tasksPlanningsRepository = tasksPlanningsRepository;
            _regularActivitiesRepository = regularActivitiesRepository;
            _tasksRealizationsRepository = tasksRealizationsRepository;
        }

        public string GetOAuthUrl(string redirectUri, int employeeId)
        {
            var flow = new GoogleAuthorizationCodeFlow(
                new GoogleAuthorizationCodeFlow.Initializer
                {
                    REMOVED_SECRETs = new REMOVED_SECRETs
                    {
                        REMOVED_SECRET = REMOVED_SECRET,
                        REMOVED_SECRET = REMOVED_SECRET
                    },
                    Scopes = new[]
                    {
                    "openid",
                    "email",
                    "https://www.googleapis.com/auth/calendar"
                    }
                });

            var request = flow.CreateAuthorizationCodeRequest(redirectUri);
            var url = request.Build().AbsoluteUri;

            if (!url.Contains("access_type"))
                url += "&access_type=offline&prompt=consent";

            url += $"&state={employeeId}";

            return url;
        }
        public async Task<EmployeeDto> HandleOAuthCallbackAsync(string code, string state, string redirectUri)
        {
            int employeeId = int.Parse(state);

            var flow = new GoogleAuthorizationCodeFlow(
                new GoogleAuthorizationCodeFlow.Initializer
                {
                    REMOVED_SECRETs = new REMOVED_SECRETs
                    {
                        REMOVED_SECRET = REMOVED_SECRET,
                        REMOVED_SECRET = REMOVED_SECRET
                    },
                    Scopes = new[]
                    {
                    "openid",
                    "email",
                    "https://www.googleapis.com/auth/calendar"
                    }
                });

            var tokenResponse = await flow.ExchangeCodeForTokenAsync(
                userId: employeeId.ToString(),
                code: code,
                redirectUri: redirectUri,
                CancellationToken.None);

            var payload = await Google.Apis.Auth.GoogleJsonWebSignature.ValidateAsync(tokenResponse.IdToken);

            var employee = await _employeeRepo.GetEmployeeByIdAsync(employeeId);

            employee.GoogleEmail = payload.Email;
            employee.GoogleAccessToken = tokenResponse.AccessToken;
            if (!string.IsNullOrEmpty(tokenResponse.RefreshToken))
            {
                employee.GoogleRefreshToken = tokenResponse.RefreshToken;
            }

            await _employeeRepo.SaveEmployeeAsync(employee);

            return employee;
        }

        public async Task SyncFutureEventsForAllEmployeesAsync()
        {
            var employees = await _employeeRepo.SearchEmployeesAsync(new Web.Models.RequestModels.Employees.SearchEmployeesParams { });
            var employeesWithToken = employees.Where(e => !string.IsNullOrEmpty(e.GoogleRefreshToken)).ToList();
            foreach (var emp in employeesWithToken)
            {
                try
                {
                    await SyncFutureEventsForEmployeeAsync(emp.Id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Greška za {emp.Email}: {ex.Message}");
                }
            }
        }

        public async Task SyncFutureEventsForEmployeeAsync(int employeeId)
{
    var employee = await _employeeRepo.GetEmployeeByIdAsync(employeeId);

    if (string.IsNullOrEmpty(employee.GoogleRefreshToken))
        throw new Exception("RefreshToken nedostaje, korisnik mora ponovo autorizovati aplikaciju.");

    var flow = new GoogleAuthorizationCodeFlow(
        new GoogleAuthorizationCodeFlow.Initializer
        {
            REMOVED_SECRETs = new REMOVED_SECRETs
            {
                REMOVED_SECRET = REMOVED_SECRET,
                REMOVED_SECRET = REMOVED_SECRET
            },
            Scopes = new[] { CalendarService.Scope.Calendar }
        });

    var token = new TokenResponse
    {
        AccessToken = employee.GoogleAccessToken,
        RefreshToken = employee.GoogleRefreshToken
    };

    var credential = new UserCredential(flow, employeeId.ToString(), token);
    await credential.RefreshTokenAsync(CancellationToken.None);

    var service = new CalendarService(new BaseClientService.Initializer
    {
        HttpClientInitializer = credential,
        ApplicationName = "IDAProject"
    });

    var nowUtc = DateTime.UtcNow;

    var request = service.Events.List("primary");
    request.TimeMin = nowUtc;
    request.TimeMax = nowUtc.AddDays(7);
    request.SingleEvents = true;
    request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

    var events = await request.ExecuteAsync();

    var regularActivityCache = new Dictionary<int, int>();
    var today = DateTime.Today;

    foreach (var ev in events.Items)
    {
        var attendees = ev.Attendees?.Select(a => a.Email).ToList() ?? new List<string>();
        if (!attendees.Any())
            attendees.Add(employee.GoogleEmail);


        foreach (var email in attendees)
        {
            var empList = await _employeeRepo.SearchEmployeesAsync(
                new Web.Models.RequestModels.Employees.SearchEmployeesParams { Email = email });

            if (empList == null) continue;

            var employeeData = empList.FirstOrDefault();
            if (employeeData == null || !employeeData.UserId.HasValue) continue;

            var start = ev.Start.DateTime ?? DateTime.Parse(ev.Start.Date);
            var end = ev.End.DateTime ?? DateTime.Parse(ev.End.Date);
            var created = ev.Created?.Date;

            var timeFrom = TimeOnly.FromDateTime(start);
            var timeTo = TimeOnly.FromDateTime(end);
            var duration = TimeOnly.FromTimeSpan(end - start);

            try
            {
                if (!regularActivityCache.ContainsKey(employeeData.UserId.Value))
                {
                    var id = await _regularActivitiesRepository
                        .GetOrCreateMeetingActivityId(employeeData.UserId.Value);

                    regularActivityCache[employeeData.UserId.Value] = id;
                }

                var regularActivityId = regularActivityCache[employeeData.UserId.Value];

                bool isCreatedToday = created.HasValue && created.Value.Date == today;
                bool isStartToday = start.Date == today;
                bool isEndToday = end.Date == today;

                // ================= REALIZACIJA =================
                if (isCreatedToday && isStartToday && isEndToday)
                {
                    var existsRealization =
                        await _tasksRealizationsRepository.SearchTasksRealizationsAsync(
                            new Web.Models.RequestModels.TasksRealizations.SearchTasksRealizationsParams
                            {
                                GoogleEventId = ev.Id,
                                UserId = employeeData.UserId
                            });

                    if (existsRealization.Count > 0)
                        continue;

                    await _tasksRealizationsRepository.SaveTasksRealizationAsync(
                        new Web.Models.Dto.TasksRealizations.SaveTasksRealizationRequestModel
                        {
                            UserId = employeeData.UserId,
                            CreatedDate = DateTime.Now,
                            ActivityTypeId = 3,
                            Activity = ev.Summary,
                            TimeFrom = timeFrom,
                            TimeTo = timeTo,
                            Duration = duration,
                            RegularActivityId = regularActivityId,
                            RealizationDate = start.Date,
                            GoogleEventId = ev.Id,
                            GoogleEventLink = ev.HtmlLink,
                            PlanNo = 0,
                            Finished = true,
                        });
                }
                else
                {
                    var plansForDate =
                        await _tasksPlanningsRepository.SearchTasksPlanningsAsync(
                            new Web.Models.RequestModels.TasksPlannings.SearchTasksPlanningsParams
                            {
                                PlanDate = start.Date.ToString("dd.MM.yyyy"),
                                UserId = employeeData.UserId
                            });

                    var lastPlanNo = plansForDate
                        .Where(p => p.EmployeeId == employeeData.Id &&
                                    p.PlanDate.Value.Date == start.Date)
                        .OrderByDescending(p => p.PlanNo)
                        .Select(p => p.PlanNo)
                        .FirstOrDefault();

                    int newPlanNo = (lastPlanNo ?? 0) + 1;

                    var existsPlan = await _tasksPlanningsRepository.SearchTasksPlanningsAsync(
                            new Web.Models.RequestModels.TasksPlannings.SearchTasksPlanningsParams
                            {
                               GoogleEventId = ev.Id,
                                UserId = employeeData.UserId
                            });

                            if (existsPlan.Count > 0)
                        continue;

                    await _tasksPlanningsRepository.SaveTasksPlanningAsync(
                        new Web.Models.Dto.TasksPlannings.SaveTasksPlanningRequestModel
                        {
                            UserId = employeeData.UserId,
                            EmployeeId = employeeData.Id,
                            CreatedAt = DateTime.Now,
                            ActivityTypeId = 3,
                            ActivityName = ev.Summary,
                            PlanStatusId = 1,
                            TimeFrom = timeFrom,
                            TimeTo = timeTo,
                            Duration = duration,
                            RegularActivityId = regularActivityId,
                            PlanNo = newPlanNo,
                            PlanDate = start.Date,
                            GoogleEventId = ev.Id,
                            GoogleEventLink = ev.HtmlLink

                        });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GRESKA:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }
    }
}
    }
}

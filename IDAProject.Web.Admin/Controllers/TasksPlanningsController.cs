using System.Globalization;
using IDAProject.Web.Admin.Managers;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.TasksPlannings;
using IDAProject.Web.Models.Dto.RegularActivities;
using IDAProject.Web.Models.Dto.TasksPlannings;
using IDAProject.Web.Models.Dto.TasksRealizations;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.RegularActivities;
using IDAProject.Web.Models.RequestModels.TasksPlannings;
using Microsoft.AspNetCore.Mvc;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class TasksPlanningsController : BaseController
    {
        private readonly ITasksPlanningsManager _TasksPlanningsManager;
        private readonly ITasksRealizationsManager _tasksRealizationsManager;
        private readonly IRegularActivitiesManager _regularActivitiesManager;
        private readonly IShiftsManager _shiftsManager;
        private readonly IMasterDataManager _masterDataManager;

        public TasksPlanningsController(
            ILogger<TasksPlanningsController> logger,
            IAccountManager accountManager,
            ITasksRealizationsManager tasksRealizationsManager,
            IRegularActivitiesManager regularActivitiesManager,
            ITasksPlanningsManager TasksPlanningsManager,
            IShiftsManager shiftsManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _TasksPlanningsManager = TasksPlanningsManager;
            _masterDataManager = masterDataManager;
            _tasksRealizationsManager = tasksRealizationsManager;
            _shiftsManager = shiftsManager;
            _regularActivitiesManager = regularActivitiesManager;
        }
        [HttpGet("TasksPlanningsList", Name = RouteNames.TasksPlannings_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new TasksPlanningsViewModel();
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "TasksPlannings");
            return View(viewModel);
        }
        [HttpGet("getById/{id}", Name = RouteNames.TasksPlannings_GetById)]
        public async Task<IActionResult> GetById(int id)
        {
            var responseModel = await _TasksPlanningsManager.GetTasksPlanningByIdAsync(id);
            return Json(responseModel.Payload);
        }
        [HttpGet("stats/{employeeId}", Name = RouteNames.TasksPlannings_GetStatsByEmployeeId)]
        public async Task<IActionResult> TasksPlanningsGetStatsByEmployeeId(int employeeId)
        {
            var responseModel = await _TasksPlanningsManager.GetLast30DaysStats(employeeId);
            return Json(responseModel.Payload);
        }
        [HttpGet("statsGeneric/{employeeId}", Name = RouteNames.TasksPlannings_GetStatsGenericByEmployeeId)]
        public async Task<IActionResult> TasksPlanningsGetStatsByEmployeeId(
     int employeeId,
     [FromQuery] string from,
     [FromQuery] string to)
        {
            DateTime fromDate = DateTime.ParseExact(from, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(to, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var responseModel = await _TasksPlanningsManager.GetStatsGeneric(employeeId, from, to);
            return Json(responseModel.Payload);
        }

        [HttpPost("search", Name = RouteNames.TasksPlannings_Search)]
        public async Task<IActionResult> SearchTasksPlannings(SearchTasksPlanningsParams searchParams)
        {
            var responseModel = await _TasksPlanningsManager.SearchTasksPlanningsAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new", Name = RouteNames.TasksPlannings_New)]
        public async Task<IActionResult> NewTasksPlanningAsync()
        {
            var viewModel = new TasksPlanningViewModel();

            viewModel.User = GetCurrentUser();
            return View("EditTasksPlanning", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.TasksPlannings_Edit)]
        public async Task<IActionResult> EditTasksPlanningAsync(int id)
        {
            var viewModel = new TasksPlanningViewModel();

            var TasksPlanningResponse = await _TasksPlanningsManager.GetTasksPlanningByIdAsync(id);

            viewModel.TasksPlanning = TasksPlanningResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditTasksPlanning", viewModel);
        }

        [HttpPost("save", Name = RouteNames.TasksPlannings_Save)]
        public async Task<IActionResult> SaveTasksPlanningAsync(SaveTasksPlanningRequestModel requestModel)
        {
            var user = GetCurrentUser();
            if (!string.IsNullOrEmpty(requestModel.PlanDateForSave))
            {
                string[] formats = { "dd.MM.yyyy.", "d.M.yyyy" };
                if (DateTime.TryParseExact(requestModel.PlanDateForSave, formats,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime planDate))
                {
                    requestModel.PlanDate = planDate;
                }
                else
                {
                    throw new ArgumentException($"Neispravan format datuma: {requestModel.PlanDateForSave}");
                }
            }
            requestModel.UserId = user.Id;
            if (requestModel.Id == 0)
            {
                requestModel.CreatedAt = DateTime.Now;
            }
            else
            {
                var taskplanning = await _TasksPlanningsManager.GetTasksPlanningByIdAsync(requestModel.Id);
                requestModel.CreatedAt = taskplanning.Payload.CreatedAt;
                requestModel.GoogleEventId = taskplanning.Payload.GoogleEventId;
                requestModel.GoogleEventLink = taskplanning.Payload.GoogleEventLink;
            }
                requestModel.EmployeeId = user.EmployeeId;
            var responseModel = await _TasksPlanningsManager.SaveTasksPlanningAsync(requestModel);
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.TasksPlannings_Delete)]
        public async Task<IActionResult> DeleteTasksPlanningAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _TasksPlanningsManager.DeleteTasksPlanningAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.TasksPlannings_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("tasks-plannings/create-by-shift", Name = RouteNames.TasksPlannings_CreateByShift)]
        public async Task<IActionResult> CreateByShift(string date, int shiftId)
        {
            var result = new ResponseModelBase();
            var user = GetCurrentUser();

            // =========================
            // 1?? PARSIRANJE DATUMA
            // =========================
            if (!DateTime.TryParseExact(
                    date,
                    "dd.MM.yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var parsedDate))
            {
                return BadRequest("Nevalidan datum.");
            }

            // =========================
            // 2?? SHIFT
            // =========================
            var shiftResult = await _shiftsManager.GetShiftByIdAsync(shiftId);

            if (shiftResult?.Payload == null)
                return BadRequest("Smena ne postoji.");

            var shift = shiftResult.Payload;

            if (shift.TimeFrom == null)
                return BadRequest("Smena nema definisano vreme.");

            var startTime = shift.TimeFrom.Value;
            var endTime = startTime.AddMinutes(15);

            // =========================
            // 3?? REGULAR ACTIVITY (OBAVEŠTAVANJE)
            // =========================
            var existingRA = await _regularActivitiesManager
                .SearchRegularActivitiesAsync(new SearchRegularActivitiesParams
                {
                    UserId = user.Id,
                    Name = "Obaveštavanje"
                });

            int regularActivityId;

            if (existingRA.Payload != null && existingRA.Payload.Any())
            {
                regularActivityId = existingRA.Payload.First().Id;
            }
            else
            {
                var saveRegularActivity = new SaveRegularActivityRequestModel
                {
                    Description = "Obaveštavanje",
                    Name = "Obaveštavanje",
                    UserId = user.Id
                };

                var savedRA = await _regularActivitiesManager
                    .SaveRegularActivityAsync(saveRegularActivity);

                regularActivityId = savedRA.Payload;
            }
            var existingPlan = await _TasksPlanningsManager.SearchTasksPlanningsAsync(
    new SearchTasksPlanningsParams
    {
        UserId = user.Id,
        PlanDate = date,
        RegularActivityId = regularActivityId
    });

            if (existingPlan?.Payload != null && existingPlan.Payload.Any())
            {
                return Ok(new ResponseModelBase
                {
                    Valid = true,
                    Message = "Plan ve? postoji za ovaj datum."
                });
            }

            var model = new SaveTasksPlanningRequestModel
            {
                UserId = user.Id,
                EmployeeId = user.EmployeeId,

                PlanDate = parsedDate.Date,

                ActivityName = "Obaveštavanje",
                PlanStatusId = 2,
                ActivityTypeId = 3,

                CreatedAt = DateTime.Now,
                PlanNo = 1,

                RegularActivityId = regularActivityId,

                TimeFromFormatted = startTime.ToString("HH:mm"),
                TimeToFormatted = endTime.ToString("HH:mm"),
                DurationFormatted = "00:15"
            };

            var savedPlan = await _TasksPlanningsManager.SaveTasksPlanningAsync(model);

            if (savedPlan.Valid)
            {
                var saveRealization = new SaveTasksRealizationRequestModel
                {
                    Activity = "Obaveštavanje",
                    ActivityTypeId = 3,
                    CreatedDate = DateTime.Now,
                    Finished = true,

                    DurationFormatted = "00:15",
                    RegularActivityId = regularActivityId,

                    PlanNo = 1,
                    TasksPlanningId = savedPlan.Payload,

                    RealizationDate = parsedDate.Date,

                    TimeFromFormatted = startTime.ToString("HH:mm"),
                    TimeToFormatted = endTime.ToString("HH:mm"),

                    UserId = user.Id
                };
                var savedRealization =
                    await _tasksRealizationsManager.SaveTasksRealizationAsync(saveRealization);

                result.Valid = savedRealization.Valid;
            }

            return Json(result);
        }

        [HttpPost("tasks-plannings/create-by-DefaultShift", Name = RouteNames.TasksPlannings_CreateByDefaultShift)]
        public async Task<IActionResult> CreateByDefaultShift(string date)
        {
            var result = new ResponseModelBase();
            var user = GetCurrentUser();

            // =========================
            // 1?? PARSIRANJE DATUMA
            // =========================
            if (!DateTime.TryParseExact(
                    date,
                    "dd.MM.yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var parsedDate))
            {
                return BadRequest("Nevalidan datum.");
            }

            // =========================
            // 2?? DEFAULT SHIFT (ID 4)
            // =========================
            var shiftResult = await _shiftsManager.GetShiftByIdAsync(4);

            if (shiftResult?.Payload == null)
                return BadRequest("Smena ne postoji.");

            var shift = shiftResult.Payload;

            if (shift.TimeFrom == null)
                return BadRequest("Smena nema definisano vreme.");

            var startTime = shift.TimeFrom.Value;
            var endTime = startTime.AddMinutes(15);

            

            // =========================
            // 4?? REGULAR ACTIVITY
            // =========================
            var existingRA = await _regularActivitiesManager
                .SearchRegularActivitiesAsync(new SearchRegularActivitiesParams
                {
                    UserId = user.Id,
                    Name = "Obaveštavanje"
                });

            int regularActivityId;

            if (existingRA.Payload != null && existingRA.Payload.Any())
            {
                regularActivityId = existingRA.Payload.First().Id;
            }
            else
            {
                var saveRegularActivity = new SaveRegularActivityRequestModel
                {
                    Description = "Obaveštavanje",
                    Name = "Obaveštavanje",
                    UserId = user.Id
                };

                var savedRA = await _regularActivitiesManager
                    .SaveRegularActivityAsync(saveRegularActivity);

                regularActivityId = savedRA.Payload;
            }

            // =========================
            // 3?? PROVERA DUPLIKATA (PLAN)
            // =========================
            var existingPlan = await _TasksPlanningsManager.SearchTasksPlanningsAsync(
                new SearchTasksPlanningsParams
                {
                    UserId = user.Id,
                    PlanDate = date,
                    RegularActivityId = regularActivityId
                });

            if (existingPlan?.Payload != null && existingPlan.Payload.Any())
            {
                return Ok(new ResponseModelBase
                {
                    Valid = true,
                    Message = "Plan ve? postoji za ovaj datum."
                });
            }
            var model = new SaveTasksPlanningRequestModel
            {
                UserId = user.Id,
                EmployeeId = user.EmployeeId,
                PlanDate = parsedDate.Date,

                ActivityName = "Obaveštavanje",
                PlanStatusId = 2,
                ActivityTypeId = 3,

                CreatedAt = DateTime.Now,
                PlanNo = 1,

                RegularActivityId = regularActivityId,

                TimeFromFormatted = startTime.ToString("HH:mm"),
                TimeToFormatted = endTime.ToString("HH:mm"),
                DurationFormatted = "00:15"
            };

            var savedPlan = await _TasksPlanningsManager.SaveTasksPlanningAsync(model);

            if (savedPlan.Valid)
            {
                var saveRealization = new SaveTasksRealizationRequestModel
                {
                    Activity = "Obaveštavanje",
                    ActivityTypeId = 3,
                    CreatedDate = DateTime.Now,
                    Finished = true,

                    DurationFormatted = "00:15",

                    PlanNo = 1,
                    TasksPlanningId = savedPlan.Payload,
                    RegularActivityId = regularActivityId,

                    RealizationDate = parsedDate.Date,

                    TimeFromFormatted = startTime.ToString("HH:mm"),
                    TimeToFormatted = endTime.ToString("HH:mm"),

                    UserId = user.Id
                };

                var savedRealization =
                    await _tasksRealizationsManager.SaveTasksRealizationAsync(saveRealization);

                result.Valid = savedRealization.Valid;
            }

            return Json(result);
        }
    }
}

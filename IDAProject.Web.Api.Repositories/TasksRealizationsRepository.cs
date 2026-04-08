using System.Globalization;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.TasksRealizations;
using IDAProject.Web.Models.RequestModels.TasksRealizations;
using Microsoft.EntityFrameworkCore;

namespace IDAProject.Web.Api.Repositories
{
    public class TasksRealizationsRepository : ITasksRealizationsRepository
    {
        private readonly IdaContext _dbContext;

        public TasksRealizationsRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TasksRealizationDto> GetTasksRealizationByIdAsync(int id)
        {
            var searchParams = new SearchTasksRealizationsParams
            {
                Id = id
            };
            var result = await SearchTasksRealizationsAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<TasksRealizationDto>> SearchTasksRealizationsAsync(SearchTasksRealizationsParams searchParams)
        {

            var result = new List<TasksRealizationDto>();
            IQueryable<TasksRealization> query = _dbContext.TasksRealizations.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (!string.IsNullOrEmpty(searchParams.CreatedDate))
                {
                    if (DateTime.TryParseExact(searchParams.CreatedDate,
                                               "dd.MM.yyyy",
                                               CultureInfo.InvariantCulture,
                                               DateTimeStyles.None,
                                               out var parsedDate))
                    {
                        query = query.Where(x => x.CreatedDate.HasValue &&
                                                 x.CreatedDate.Value.Date == parsedDate.Date);
                    }
                }
                if (!string.IsNullOrEmpty(searchParams.RealizationDate))
                {
                    if (DateTime.TryParseExact(searchParams.RealizationDate,
                                               "dd.MM.yyyy",
                                               CultureInfo.InvariantCulture,
                                               DateTimeStyles.None,
                                               out var parsedDate))
                    {
                        query = query.Where(x => x.RealizationDate.HasValue &&
                                                 x.RealizationDate.Value.Date == parsedDate.Date);
                    }
                }
                if (searchParams.UserId.HasValue)
                {
                    query = query.Where(x => x.UserId == searchParams.UserId);
                }
                if (!string.IsNullOrEmpty(searchParams.StartDate) && !string.IsNullOrEmpty(searchParams.EndDate))
                {
                    if (DateTime.TryParseExact(searchParams.StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var start) &&
                        DateTime.TryParseExact(searchParams.EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var end))
                    {
                        query = query.Where(x => x.RealizationDate.HasValue &&
                                                 x.RealizationDate.Value.Date >= start.Date &&
                                                 x.RealizationDate.Value.Date <= end.Date);
                    }
                }

                if (!string.IsNullOrEmpty(searchParams.GoogleEventId))
                {
                    query = query.Where(x => x.GoogleEventId == searchParams.GoogleEventId);
                }
            }

            result = await query.OrderBy(x => x.TimeFrom).Select(a => new TasksRealizationDto
            {
                Id = a.Id,
                ActivityTypeId = a.ActivityTypeId,
                ActivityType = a.ActivityType.Name,
                Activity = a.Activity,
                CreatedDate = a.CreatedDate,
                Duration = a.Duration,
                Finished = a.Finished,
                IdaTaskId = a.IdaTaskId,
                ProjectId  = a.ProjectId,
                RegularActivityId = a.RegularActivityId,
                Report = a.Report,
                Employee = a.TasksPlanning.Employee.Name + " " + a.TasksPlanning.Employee.Surname,
                TasksPlanningId = a.TasksPlanningId,
                TimeFrom  = a.TimeFrom,
                TimeTo = a.TimeTo,
                PlanNo = a.PlanNo,
                UserId = a.UserId,
                RealizationDate = a.RealizationDate,
                DisplayTask =
        a.ProjectId != null && a.IdaTaskId != null
            ? a.Project.Description + " - " + a.IdaTask.Name
            : a.IdaTaskId != null
                ? a.IdaTask.Name
                : a.RegularActivityId != null
                    ? a.RegularActivity.Name
                    : "",
                IsFinished =
    a.Finished != null
        ? a.Finished
        : (bool?)null,
                GoogleEventId = a.GoogleEventId


            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveTasksRealizationAsync(SaveTasksRealizationRequestModel requestModel)
        {
            TasksRealization? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.TasksRealizations.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveTasksRealizationRequestModel,TasksRealization>(requestModel);
                _dbContext.TasksRealizations.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteTasksRealizationAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.TasksRealizations.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.TasksRealizations.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<EmployeeRealizationStatsDto> GetLast30DaysRealizationStats(int employeeId)
        {
            var to = DateTime.Now;
            var from = to.AddDays(-30);

            // ============================
            // PLANOVI
            // ============================
            var plannedPlans = await _dbContext.TasksPlannings
                .Where(x => x.EmployeeId == employeeId
                         && x.PlanDate >= from
                         && x.PlanDate < to
                         && !x.IsDeleted)
                .Select(x => new { x.Id, x.PlanDate })
                .ToListAsync();

            var plannedPlanIds = plannedPlans.Select(p => p.Id).ToList();

            // ============================
            // REALIZACIJE
            // ============================
            var allRealizations = await _dbContext.TasksRealizations
                .Where(x => x.User.EmployeeId == employeeId
                         && x.RealizationDate >= from
                         && x.RealizationDate < to
                         && !x.IsDeleted)
                .ToListAsync();

            // ============================
            // TRAJANJE U SATIMA
            // ============================
            double plannedDuration = 0;
            double unplannedDuration = 0;

            double projectDuration = 0;
            double taskDuration = 0;
            double regularDuration = 0;

            foreach (var r in allRealizations)
            {
                if (!r.Duration.HasValue)
                    continue;

                double durationHours = r.Duration.Value.Hour + r.Duration.Value.Minute / 60.0 + r.Duration.Value.Second / 3600.0;

                // Planirano / Neplanirano
                if (r.TasksPlanningId.HasValue && plannedPlanIds.Contains(r.TasksPlanningId.Value))
                    plannedDuration += durationHours;
                else
                    unplannedDuration += durationHours;

                // Activity Type
                switch (r.ActivityTypeId)
                {
                    case 1:
                        projectDuration += durationHours;
                        break;
                    case 2:
                        taskDuration += durationHours;
                        break;
                    case 3:
                        regularDuration += durationHours;
                        break;
                }
            }

            double totalDuration = plannedDuration + unplannedDuration;

            decimal plannedPercentage = totalDuration > 0
                ? Math.Round((decimal)(plannedDuration / totalDuration * 100), 2)
                : 0;

            decimal unplannedPercentage = totalDuration > 0
                ? Math.Round((decimal)(unplannedDuration / totalDuration * 100), 2)
                : 0;

            decimal projectPercentage = totalDuration > 0
                ? Math.Round((decimal)(projectDuration / totalDuration * 100), 2)
                : 0;

            decimal taskPercentage = totalDuration > 0
                ? Math.Round((decimal)(taskDuration / totalDuration * 100), 2)
                : 0;

            decimal regularPercentage = totalDuration > 0
                ? Math.Round((decimal)(regularDuration / totalDuration * 100), 2)
                : 0;

            // ============================
            // TOTAL PLANNED DAYS
            // ============================
            int totalPlannedDays = plannedPlans
                .Select(p => p.PlanDate.Value.Date)
                .Distinct()
                .Count();

            // Teoretsko radno vreme: broj radnih dana * 7.25h
            double totalWorkingHours = totalPlannedDays * 7.25;

            int daysWithRealization = allRealizations
                .Select(r => r.RealizationDate)
                .Distinct()
                .Count();

            // ============================
            // USER LOGS
            // ============================
            var userLogs = await _dbContext.UserLogs
                .Where(l => l.AspNetUser.EmployeeId == employeeId
                         && l.LoginDateTime < to
                         && (l.LogoutDateTime == null || l.LogoutDateTime > from))
                .ToListAsync();

            double totalLoggedHours = 0;

            foreach (var log in userLogs)
            {
                if (!log.LoginDateTime.HasValue)
                    continue;

                var login = log.LoginDateTime.Value;
                var logout = log.LogoutDateTime ?? DateTime.Now;

                var start = login < from ? from : login;
                var end = logout > to ? to : logout;

                if (end > start)
                    totalLoggedHours += (end - start).TotalHours;
            }

            totalLoggedHours = Math.Round(totalLoggedHours, 2);

            // ============================
            // RETURN DTO
            // ============================
            return new EmployeeRealizationStatsDto
            {
                TotalWorkingDays = totalPlannedDays,
                DaysWithRealization = daysWithRealization,
                TotalWorkHours = Math.Round(totalWorkingHours, 2), // <-- promenjeno
                TotalLoggedHours = totalLoggedHours,
                PlannedCount = Math.Round(plannedDuration, 2),
                UnplannedCount = Math.Round(unplannedDuration, 2),
                PlannedPercentage = plannedPercentage,
                UnplannedPercentage = unplannedPercentage,
                ProjectCount = Math.Round(projectDuration, 2),
                ProjectPercentage = projectPercentage,
                TaskCount = Math.Round(taskDuration, 2),
                TaskPercentage = taskPercentage,
                RegularCount = Math.Round(regularDuration, 2),
                RegularPercentage = regularPercentage
            };
        }

        public async Task<EmployeeRealizationStatsDto> GetGenericStats(int employeeId, DateTime? from, DateTime? to)
        {
            if (!from.HasValue || !to.HasValue)
                throw new ArgumentException("From i To moraju imati vrednost.");

            var fromDate = from.Value;
            var toDate = to.Value;

            // ============================
            // PLANS
            // ============================
            var plannedPlans = await _dbContext.TasksPlannings
                .Where(x => x.EmployeeId == employeeId
                         && x.PlanDate >= fromDate
                         && x.PlanDate < toDate
                         && !x.IsDeleted)
                .Select(x => new { x.Id, x.PlanDate })
                .ToListAsync();

            var plannedPlanIds = plannedPlans.Select(p => p.Id).ToList();

            // ============================
            // REALIZATIONS
            // ============================
            var allRealizations = await _dbContext.TasksRealizations
                .Where(x => x.User.EmployeeId == employeeId
                         && x.RealizationDate >= fromDate
                         && x.RealizationDate < toDate
                         && !x.IsDeleted)
                .ToListAsync();

            // ============================
            // TRAJANJE U SATIMA
            // ============================
            double plannedDuration = 0;
            double unplannedDuration = 0;

            double projectDuration = 0;
            double taskDuration = 0;
            double regularDuration = 0;

            foreach (var r in allRealizations)
            {
                if (!r.Duration.HasValue)
                    continue;

                double durationHours = r.Duration.Value.Hour + r.Duration.Value.Minute / 60.0 + r.Duration.Value.Second / 3600.0;

                // Planned / Unplanned
                if (r.TasksPlanningId.HasValue && plannedPlanIds.Contains(r.TasksPlanningId.Value))
                    plannedDuration += durationHours;
                else
                    unplannedDuration += durationHours;

                // Activity Type
                switch (r.ActivityTypeId)
                {
                    case 1:
                        projectDuration += durationHours;
                        break;
                    case 2:
                        taskDuration += durationHours;
                        break;
                    case 3:
                        regularDuration += durationHours;
                        break;
                }
            }

            double totalDuration = plannedDuration + unplannedDuration;

            decimal plannedPercentage = totalDuration > 0
                ? Math.Round((decimal)(plannedDuration / totalDuration * 100), 2)
                : 0;

            decimal unplannedPercentage = totalDuration > 0
                ? Math.Round((decimal)(unplannedDuration / totalDuration * 100), 2)
                : 0;

            decimal projectPercentage = totalDuration > 0
                ? Math.Round((decimal)(projectDuration / totalDuration * 100), 2)
                : 0;

            decimal taskPercentage = totalDuration > 0
                ? Math.Round((decimal)(taskDuration / totalDuration * 100), 2)
                : 0;

            decimal regularPercentage = totalDuration > 0
                ? Math.Round((decimal)(regularDuration / totalDuration * 100), 2)
                : 0;

            // ============================
            // TOTAL PLANNED DAYS
            // ============================
            int totalPlannedDays = plannedPlans
                .Select(p => p.PlanDate.Value.Date)
                .Distinct()
                .Count();

            // Teoretsko radno vreme: broj radnih dana * 7.25h
            double totalWorkingHours = totalPlannedDays * 7.25;

            int daysWithRealization = allRealizations
                .Select(r => r.RealizationDate)
                .Distinct()
                .Count();

            // ============================
            // USER LOGS
            // ============================
            var userLogs = await _dbContext.UserLogs
                .Where(l => l.AspNetUser.EmployeeId == employeeId
                         && l.LoginDateTime < toDate
                         && (l.LogoutDateTime == null || l.LogoutDateTime > fromDate))
                .ToListAsync();

            double totalLoggedHours = 0;

            foreach (var log in userLogs)
            {
                if (!log.LoginDateTime.HasValue)
                    continue;

                var login = log.LoginDateTime.Value;
                var logout = log.LogoutDateTime ?? DateTime.Now;

                var start = login < fromDate ? fromDate : login;
                var end = logout > toDate ? toDate : logout;

                if (end > start)
                    totalLoggedHours += (end - start).TotalHours;
            }

            totalLoggedHours = Math.Round(totalLoggedHours, 2);

            // ============================
            // RETURN DTO
            // ============================
            return new EmployeeRealizationStatsDto
            {
                TotalWorkingDays = totalPlannedDays,
                DaysWithRealization = daysWithRealization,
                TotalWorkHours = Math.Round(totalWorkingHours, 2),  // <-- promenjeno
                TotalLoggedHours = totalLoggedHours,
                PlannedCount = Math.Round(plannedDuration, 2),
                UnplannedCount = Math.Round(unplannedDuration, 2),
                PlannedPercentage = plannedPercentage,
                UnplannedPercentage = unplannedPercentage,
                ProjectCount = Math.Round(projectDuration, 2),
                ProjectPercentage = projectPercentage,
                TaskCount = Math.Round(taskDuration, 2),
                TaskPercentage = taskPercentage,
                RegularCount = Math.Round(regularDuration, 2),
                RegularPercentage = regularPercentage
            };
        }
    }
}
    
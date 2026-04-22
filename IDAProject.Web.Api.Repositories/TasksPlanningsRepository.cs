using System.Globalization;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.TasksPlannings;
using IDAProject.Web.Models.RequestModels.TasksPlannings;
using Microsoft.EntityFrameworkCore;

namespace IDAProject.Web.Api.Repositories
{
    public class TasksPlanningsRepository : ITasksPlanningsRepository
    {
        private readonly IdaContext _dbContext;

        public TasksPlanningsRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TasksPlanningDto> GetTasksPlanningByIdAsync(int id)
        {
            var searchParams = new SearchTasksPlanningsParams
            {
                Id = id
            };
            var result = await SearchTasksPlanningsAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<TasksPlanningDto>> SearchTasksPlanningsAsync(SearchTasksPlanningsParams searchParams)
        {

            var result = new List<TasksPlanningDto>();
            IQueryable<TasksPlanning> query = _dbContext.TasksPlannings.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.ActivityTypeId.HasValue)
                {
                    query = query.Where(x => x.ActivityTypeId == searchParams.ActivityTypeId);
                }
                if (searchParams.RegularActivityId.HasValue)
                {
                    query = query.Where(x => x.RegularActivityId == searchParams.RegularActivityId);
                }
                if (!string.IsNullOrEmpty(searchParams.CreatedDate))
                {
                    if (DateTime.TryParseExact(searchParams.CreatedDate,
                                               "dd.MM.yyyy",
                                               CultureInfo.InvariantCulture,
                                               DateTimeStyles.None,
                                               out var parsedDate))
                    {
                        query = query.Where(x => x.CreatedAt.HasValue &&
                                                 x.CreatedAt.Value.Date == parsedDate.Date);
                    }
                }
                if (!string.IsNullOrEmpty(searchParams.PlanDate))
                {
                    if (DateTime.TryParseExact(searchParams.PlanDate,
                                               "dd.MM.yyyy",
                                               CultureInfo.InvariantCulture,
                                               DateTimeStyles.None,
                                               out var parsedDate))
                    {
                        query = query.Where(x => x.PlanDate.HasValue &&
                                                 x.PlanDate.Value.Date == parsedDate.Date);
                    }
                }
                if (!string.IsNullOrEmpty(searchParams.PlanDateForRowNumber))
                {
                    if (DateTime.TryParseExact(searchParams.PlanDateForRowNumber,
                                               "dd.MM.yyyy",
                                               CultureInfo.InvariantCulture,
                                               DateTimeStyles.None,
                                               out var parsedDate))
                    {
                        query = query.Where(x => x.PlanDate.HasValue &&
                                                 x.PlanDate.Value.Date == parsedDate.Date);
                    }
                }
                if (searchParams.UserId.HasValue)
                {
                    query = query.Where(x => x.UserId == searchParams.UserId);
                }
                if (searchParams.EmployeeId.HasValue)
                {
                    query = query.Where(x => x.User.EmployeeId == searchParams.EmployeeId);
                }
                if (!string.IsNullOrEmpty(searchParams.StartDate) && !string.IsNullOrEmpty(searchParams.EndDate))
                {
                    if (DateTime.TryParseExact(searchParams.StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var start) &&
                        DateTime.TryParseExact(searchParams.EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var end))
                    {
                        query = query.Where(x => x.PlanDate.HasValue &&
                                                 x.PlanDate.Value.Date >= start.Date &&
                                                 x.PlanDate.Value.Date <= end.Date);
                    }
                }
                if (searchParams.Finished.HasValue)
                {
                    query = query.Where(x =>
                        !x.TasksRealizations.Any() ||
                        x.TasksRealizations.Any(r => r.Finished == searchParams.Finished));
                }
                if (!string.IsNullOrEmpty(searchParams.GoogleEventId))
                {
                    query = query.Where(x => x.GoogleEventId.ToLower() == searchParams.GoogleEventId.ToLower());
                }
            }

            result = await query.OrderBy(x => x.TimeFrom).Select(a => new TasksPlanningDto
            {
                Id = a.Id,
                UserId = a.UserId,
                PlanStatusId = a.PlanStatusId,
                PlanStatus = a.PlanStatus.Name,
                ActivityName = a.ActivityName,
                ActivityTypeId = a.ActivityTypeId,
                ActivityTypeName = a.ActivityType.Name,
                CreatedAt = a.CreatedAt,
                Duration = a.Duration,
                EmployeeId = a.EmployeeId,
                PlanNo = a.PlanNo,
                Project = a.Project.Description,
                ProjectId = a.ProjectId,
                RegularActivityId = a.RegularActivityId,
                RegularActivity = a.RegularActivity.Name,
                TaskId = a.TaskId,
                Task = a.Task.Name,
                TimeFrom = a.TimeFrom,
                Employee = a.Employee.Name + " " + a.Employee.Surname,
                TimeTo = a.TimeTo,
                DisplayTask =
        a.ProjectId != null && a.TaskId != null
            ? a.Project.Description + " - " + a.Task.Name
            : a.TaskId != null
                ? a.Task.Name
                : a.RegularActivityId != null
                    ? a.RegularActivity.Name
                    : "",
                IsFinished = a.TasksRealizations.FirstOrDefault().Finished,
                PlanDate = a.PlanDate,
                GoogleEventId = a.GoogleEventId,
                GoogleEventLink = a.GoogleEventLink


            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveTasksPlanningAsync(SaveTasksPlanningRequestModel requestModel)
        {
            TasksPlanning? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.TasksPlannings.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveTasksPlanningRequestModel,TasksPlanning>(requestModel);
                _dbContext.TasksPlannings.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteTasksPlanningAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.TasksPlannings.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.TasksPlannings.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<EmployeePlanningStatsDto> GetLast30DaysStats(int employeeId)
        {
            var to = DateTime.Today;
            var from = to.AddDays(-30);

            var plans = await _dbContext.TasksPlannings
      .Where(x => x.EmployeeId == employeeId
               && x.PlanDate >= from
               && x.PlanDate < to
               && !x.IsDeleted
               && !(x.ActivityTypeId == 3
                    && x.RegularActivity.Name == "Obaveštavanje"))
      .ToListAsync();

            var groupedByDay = plans
                .GroupBy(x => x.PlanDate.Value.Date);

            int totalPlannedDays = 0;
            int plannedOnTimeDays = 0;

            foreach (var day in groupedByDay)
            {
                totalPlannedDays++;

                var totalDuration = day
                    .Select(x => TimeSpan.FromHours(x.Duration.Value.Hour)
                                      + TimeSpan.FromMinutes(x.Duration.Value.Minute))
                    .Aggregate(TimeSpan.Zero, (sum, t) => sum + t);

                var totalHours = totalDuration.TotalHours;

                bool allCreatedBeforeDeadline = day.All(x =>
                {
                    var deadline = x.CreatedAt.Value.Date.AddHours(12);
                    return x.CreatedAt <= deadline;
                });

                if ((decimal)totalHours >= 7.5m && allCreatedBeforeDeadline)
                {
                    plannedOnTimeDays++;
                }
            }

            decimal percentage = 0;

            if (totalPlannedDays > 0)
            {
                percentage = Math.Round(
                    (decimal)plannedOnTimeDays / totalPlannedDays * 100, 2);
            }

            return new EmployeePlanningStatsDto
            {
                TotalPlannedDays = totalPlannedDays,
                PlannedOnTimeDays = plannedOnTimeDays,
                Percentage = percentage
            };
        }

        public async Task<EmployeePlanningStatsDto> GetStatsGeneric(int employeeId, DateTime? from, DateTime? to)
        {
            var plans = await _dbContext.TasksPlannings
    .Where(x => x.EmployeeId == employeeId
             && x.PlanDate >= from
             && x.PlanDate < to
             && !x.IsDeleted
             && !(x.ActivityTypeId == 3
                  && x.RegularActivity.Name == "Obaveštavanje"))
    .ToListAsync();

            var groupedByDay = plans
                .GroupBy(x => x.PlanDate.Value.Date);

            int totalPlannedDays = 0;
            int plannedOnTimeDays = 0;

            foreach (var day in groupedByDay)
            {
                totalPlannedDays++;

                var totalDuration = day
                    .Select(x => TimeSpan.FromHours(x.Duration.Value.Hour)
                                      + TimeSpan.FromMinutes(x.Duration.Value.Minute))
                    .Aggregate(TimeSpan.Zero, (sum, t) => sum + t);

                var totalHours = totalDuration.TotalHours;

                bool allCreatedBeforeDeadline = day.All(x =>
                {
                    var deadline = x.CreatedAt.Value.Date.AddHours(12);
                    return x.CreatedAt <= deadline;
                });

                if ((decimal)totalHours >= 7.5m && allCreatedBeforeDeadline)
                {
                    plannedOnTimeDays++;
                }
            }

            decimal percentage = 0;

            if (totalPlannedDays > 0)
            {
                percentage = Math.Round(
                    (decimal)plannedOnTimeDays / totalPlannedDays * 100, 2);
            }

            return new EmployeePlanningStatsDto
            {
                TotalPlannedDays = totalPlannedDays,
                PlannedOnTimeDays = plannedOnTimeDays,
                Percentage = percentage
            };
        }
    }
}
    
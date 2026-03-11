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
                if (searchParams.UserId.HasValue)
                {
                    query = query.Where(x => x.UserId == searchParams.UserId);
                }
                if (searchParams.Finished.HasValue)
                {
                    query = query.Where(x =>
                        !x.TasksRealizations.Any() ||
                        x.TasksRealizations.Any(r => r.Finished == searchParams.Finished));
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
                TimeTo = a.TimeTo,
                DisplayTask =
        a.ProjectId != null && a.TaskId != null
            ? a.Project.Description + " - " + a.Task.Name
            : a.TaskId != null
                ? a.Task.Name
                : a.RegularActivityId != null
                    ? a.RegularActivity.Name
                    : "",
                IsFinished = a.TasksRealizations.FirstOrDefault().Finished

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

    }
}
    
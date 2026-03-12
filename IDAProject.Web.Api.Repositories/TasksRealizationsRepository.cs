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
                if (searchParams.UserId.HasValue)
                {
                    query = query.Where(x => x.UserId == searchParams.UserId);
                }
                if (!string.IsNullOrEmpty(searchParams.StartDate) && !string.IsNullOrEmpty(searchParams.EndDate))
                {
                    if (DateTime.TryParseExact(searchParams.StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var start) &&
                        DateTime.TryParseExact(searchParams.EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var end))
                    {
                        query = query.Where(x => x.CreatedDate.HasValue &&
                                                 x.CreatedDate.Value.Date >= start.Date &&
                                                 x.CreatedDate.Value.Date <= end.Date);
                    }
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
                TasksPlanningId = a.TasksPlanningId,
                TimeFrom  = a.TimeFrom,
                TimeTo = a.TimeTo,
                PlanNo = a.TasksPlanning.PlanNo,
                UserId = a.UserId,
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
        : (bool?)null


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

    }
}
    
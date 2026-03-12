using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.TasksPlanningComments;
using IDAProject.Web.Models.RequestModels.TasksPlanningComments;

namespace IDAProject.Web.Api.Repositories
{
    public class TasksPlanningCommentsRepository : ITasksPlanningCommentsRepository
    {
        private readonly IdaContext _dbContext;

        public TasksPlanningCommentsRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TasksPlanningCommentDto> GetTasksPlanningCommentByIdAsync(int id)
        {
            var searchParams = new SearchTasksPlanningCommentsParams
            {
                Id = id
            };
            var result = await SearchTasksPlanningCommentsAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<TasksPlanningCommentDto>> SearchTasksPlanningCommentsAsync(SearchTasksPlanningCommentsParams searchParams)
        {

            var result = new List<TasksPlanningCommentDto>();
            IQueryable<TasksPlanningComment> query = _dbContext.TasksPlanningComments.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.PlanId.HasValue)
                {
                    query = query.Where(x => x.TaskPlanningId == searchParams.PlanId);
                }
            }

            result = await query.Select(a => new TasksPlanningCommentDto
            {
                Id = a.Id,
                Comment = a.Comment,
                CreatedAt = a.CreatedAt,
                Photo = a.User.Employee.Photo,
                TaskPlanningId = a.TaskPlanningId,
                UserId = a.UserId,
                Username = a.User.Employee.Name + " " + a.User.Employee.Surname,
                ParentTaskPlanningCommentId = a.ParentTaskPlanningCommentId

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveTasksPlanningCommentAsync(SaveTasksPlanningCommentRequestModel requestModel)
        {
            TasksPlanningComment? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.TasksPlanningComments.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveTasksPlanningCommentRequestModel,TasksPlanningComment>(requestModel);
                _dbContext.TasksPlanningComments.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteTasksPlanningCommentAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.TasksPlanningComments.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.TasksPlanningComments.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    
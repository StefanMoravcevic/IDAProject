using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.TasksRealizationComments;
using IDAProject.Web.Models.RequestModels.TasksRealizationComments;

namespace IDAProject.Web.Api.Repositories
{
    public class TasksRealizationCommentsRepository : ITasksRealizationCommentsRepository
    {
        private readonly IdaContext _dbContext;

        public TasksRealizationCommentsRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TasksRealizationCommentDto> GetTasksRealizationCommentByIdAsync(int id)
        {
            var searchParams = new SearchTasksRealizationCommentsParams
            {
                Id = id
            };
            var result = await SearchTasksRealizationCommentsAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<TasksRealizationCommentDto>> SearchTasksRealizationCommentsAsync(SearchTasksRealizationCommentsParams searchParams)
        {

            var result = new List<TasksRealizationCommentDto>();
            IQueryable<TasksRealizationComment> query = _dbContext.TasksRealizationComments.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.RealizationId.HasValue)
                {
                    query = query.Where(x => x.TaskRealizationId == searchParams.RealizationId);
                }
            }

            result = await query.Select(a => new TasksRealizationCommentDto
            {
                Id = a.Id,
                Comment = a.Comment,
                CreatedAt = a.CreatedAt,
                Photo = a.User.Employee.Photo,
                TaskRealizationId = a.TaskRealizationId,
                UserId = a.UserId,
                Username = a.User.Employee.Name + " " + a.User.Employee.Surname,
                ParentTaskRealizationCommentId = a.ParentTaskRealizationCommentId

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveTasksRealizationCommentAsync(SaveTasksRealizationCommentRequestModel requestModel)
        {
            TasksRealizationComment? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.TasksRealizationComments.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveTasksRealizationCommentRequestModel,TasksRealizationComment>(requestModel);
                _dbContext.TasksRealizationComments.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteTasksRealizationCommentAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.TasksRealizationComments.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.TasksRealizationComments.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    
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
                if (searchParams.ParentCommentId.HasValue)
                {
                    query = query.Where(x => x.ParentTaskPlanningCommentId == searchParams.ParentCommentId);
                }
                if (searchParams.PlanId.HasValue)
                {
                    query = query.Where(x => x.TaskPlanningId == searchParams.PlanId);
                }
                if (searchParams.UserId.HasValue)
                {
                    query = query.Where(x => x.TaskPlanning.UserId == searchParams.UserId);
                }
                if (searchParams.EmployeeId.HasValue)
                {
                    query = query.Where(x => x.TaskPlanning.EmployeeId == searchParams.EmployeeId);
                }
                if (searchParams.HideFromHomePage.HasValue)
                {
                    query = query.Where(x => x.HiddenFromHomePage == searchParams.HideFromHomePage);
                }
                if (searchParams.HideFromHomePageAuthor.HasValue)
                {
                    query = query.Where(x => x.HiddenFromHomePageAuthor == searchParams.HideFromHomePageAuthor);
                }
                if(searchParams.EnteredUserId.HasValue)
                {
                    query = query.Where(x => x.UserId == searchParams.EnteredUserId);
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
                ParentTaskPlanningCommentId = a.ParentTaskPlanningCommentId,
                DisplayTask =
        a.TaskPlanning.ProjectId != null && a.TaskPlanning.TaskId != null
            ? a.TaskPlanning.Project.Description + " - " + a.TaskPlanning.Task.Name
            : a.TaskPlanning.TaskId != null
                ? a.TaskPlanning.Task.Name
                : a.TaskPlanning.RegularActivityId != null
                    ? a.TaskPlanning.RegularActivity.Name
                    : "",
                Activity = a.TaskPlanning.ActivityName,
                HiddenFromHomePage =a .HiddenFromHomePage,
                EmployeeId = a.TaskPlanning.Employee.Id,
                EmployeeForReplyId = a.User.EmployeeId,
                PlanDate = a.TaskPlanning.PlanDate,
                EnteredUsername = a.TaskPlanning.Employee.Name + " " + a.TaskPlanning.Employee.Surname,
                EnteredPhoto = a.TaskPlanning.Employee.Photo,
                HiddenFromHomePageAuthor = a .HiddenFromHomePageAuthor

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
    
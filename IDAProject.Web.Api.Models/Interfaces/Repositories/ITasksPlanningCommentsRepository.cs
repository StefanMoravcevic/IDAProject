
using IDAProject.Web.Models.Dto.TasksPlanningComments;
using IDAProject.Web.Models.RequestModels.TasksPlanningComments;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface ITasksPlanningCommentsRepository
    {
        Task<TasksPlanningCommentDto> GetTasksPlanningCommentByIdAsync(int id);
        Task<int> SaveTasksPlanningCommentAsync(SaveTasksPlanningCommentRequestModel requestModel);
        Task<List<TasksPlanningCommentDto>> SearchTasksPlanningCommentsAsync(SearchTasksPlanningCommentsParams searchParams);
        Task DeleteTasksPlanningCommentAsync(int id, int? userId);
    }
}

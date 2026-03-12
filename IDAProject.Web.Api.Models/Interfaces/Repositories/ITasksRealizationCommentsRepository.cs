
using IDAProject.Web.Models.Dto.TasksRealizationComments;
using IDAProject.Web.Models.RequestModels.TasksRealizationComments;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface ITasksRealizationCommentsRepository
    {
        Task<TasksRealizationCommentDto> GetTasksRealizationCommentByIdAsync(int id);
        Task<int> SaveTasksRealizationCommentAsync(SaveTasksRealizationCommentRequestModel requestModel);
        Task<List<TasksRealizationCommentDto>> SearchTasksRealizationCommentsAsync(SearchTasksRealizationCommentsParams searchParams);
        Task DeleteTasksRealizationCommentAsync(int id, int? userId);
    }
}

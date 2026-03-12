using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.TasksRealizationComments;
using IDAProject.Web.Models.RequestModels.TasksRealizationComments;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface ITasksRealizationCommentsManager
    {
        Task<ResponseModelList<TasksRealizationCommentDto>> SearchTasksRealizationCommentsAsync(SearchTasksRealizationCommentsParams searchParams);
        Task<ResponseModel<TasksRealizationCommentDto>> GetTasksRealizationCommentByIdAsync(int id);
        Task<ResponseModelBase> DeleteTasksRealizationCommentAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveTasksRealizationCommentAsync(SaveTasksRealizationCommentRequestModel requestModel);
    }
}

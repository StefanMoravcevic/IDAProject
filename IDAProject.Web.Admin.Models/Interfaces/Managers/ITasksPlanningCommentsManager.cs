using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.TasksPlanningComments;
using IDAProject.Web.Models.RequestModels.TasksPlanningComments;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface ITasksPlanningCommentsManager
    {
        Task<ResponseModelList<TasksPlanningCommentDto>> SearchTasksPlanningCommentsAsync(SearchTasksPlanningCommentsParams searchParams);
        Task<ResponseModel<TasksPlanningCommentDto>> GetTasksPlanningCommentByIdAsync(int id);
        Task<ResponseModelBase> DeleteTasksPlanningCommentAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveTasksPlanningCommentAsync(SaveTasksPlanningCommentRequestModel requestModel);
    }
}


using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.TasksPlannings;
using IDAProject.Web.Models.RequestModels.TasksPlannings;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface ITasksPlanningsManager
    {
        Task<ResponseModelList<TasksPlanningDto>> SearchTasksPlanningsAsync(SearchTasksPlanningsParams searchParams);
        Task<ResponseModel<TasksPlanningDto>> GetTasksPlanningByIdAsync(int id);
        Task<ResponseModelBase> DeleteTasksPlanningAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveTasksPlanningAsync(SaveTasksPlanningRequestModel requestModel);
    }
}

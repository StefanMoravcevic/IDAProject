using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.IdaTasks;
using IDAProject.Web.Models.RequestModels.IdaTasks;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IIdaTasksManager
    {
        Task<ResponseModelList<IdaTaskDto>> SearchIdaTasksAsync(SearchIdaTasksParams searchParams);
        Task<ResponseModelList<IdaTaskDto>> GetTasksByProjectAsync(int projectId);
        Task<ResponseModel<IdaTaskDto>> GetIdaTaskByIdAsync(int id);
        Task<ResponseModelList<IdaTaskDto>> GetTaskByTaskIdAsync(int taskId);
        Task<ResponseModelBase> DeleteIdaTaskAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveIdaTaskAsync(SaveIdaTaskRequestModel requestModel);
    }
}

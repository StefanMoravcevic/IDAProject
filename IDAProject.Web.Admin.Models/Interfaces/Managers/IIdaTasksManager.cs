using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.IdaTasks;
using IDAProject.Web.Models.RequestModels.IdaTasks;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IIdaTasksManager
    {
        Task<ResponseModelList<IdaTaskDto>> SearchIdaTasksAsync(SearchIdaTasksParams searchParams);
        Task<ResponseModel<IdaTaskDto>> GetIdaTaskByIdAsync(int id);
        Task<ResponseModelBase> DeleteIdaTaskAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveIdaTaskAsync(SaveIdaTaskRequestModel requestModel);
    }
}


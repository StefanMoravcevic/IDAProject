using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.EmployeeJobTypeControls;
using IDAProject.Web.Models.RequestModels.EmployeeJobTypeControls;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IEmployeeJobTypeControlsManager
    {
        Task<ResponseModelList<EmployeeJobTypeControlDto>> SearchEmployeeJobTypeControlsAsync(SearchEmployeeJobTypeControlsParams searchParams);
        Task<ResponseModel<EmployeeJobTypeControlDto>> GetEmployeeJobTypeControlByIdAsync(int id);
        Task<ResponseModelBase> DeleteEmployeeJobTypeControlAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveEmployeeJobTypeControlAsync(SaveEmployeeJobTypeControlRequestModel requestModel);
    }
}


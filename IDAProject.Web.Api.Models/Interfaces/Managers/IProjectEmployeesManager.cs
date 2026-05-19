using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.ProjectEmployees;
using IDAProject.Web.Models.RequestModels.ProjectEmployees;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IProjectEmployeesManager
    {
        Task<ResponseModelList<ProjectEmployeeDto>> SearchProjectEmployeesAsync(SearchProjectEmployeesParams searchParams);
        Task<ResponseModel<ProjectEmployeeDto>> GetProjectEmployeeByIdAsync(int id);
        Task<ResponseModelBase> DeleteProjectEmployeeAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveProjectEmployeeAsync(SaveProjectEmployeeRequestModel requestModel);
    }
}

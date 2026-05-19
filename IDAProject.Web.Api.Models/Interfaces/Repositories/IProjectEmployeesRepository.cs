
using IDAProject.Web.Models.Dto.ProjectEmployees;
using IDAProject.Web.Models.RequestModels.ProjectEmployees;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IProjectEmployeesRepository
    {
        Task<ProjectEmployeeDto> GetProjectEmployeeByIdAsync(int id);
        Task<int> SaveProjectEmployeeAsync(SaveProjectEmployeeRequestModel requestModel);
        Task<List<ProjectEmployeeDto>> SearchProjectEmployeesAsync(SearchProjectEmployeesParams searchParams);
        Task DeleteProjectEmployeeAsync(int id, int? userId);
    }
}

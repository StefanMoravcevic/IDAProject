using IDAProject.Web.Models.Dto.Contacts;
using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.RequestModels.Employees;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IEmployeesRepository
    {
        Task<List<EmployeeDto>> SearchEmployeesAsync(SearchEmployeesParams searchParams);

        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<EmployeeSearchResponseModel?> GetEmailPhoneByEmployeeIdAsync(int id);
        Task DeleteEmployeeAsync(int id, int? userId);
        Task<int> SaveEmployeeAsync(SaveEmployeeRequestModel requestModel);
		Task<EmployeeDto> GetNextEmployeeAsync(int currentId);
		Task<EmployeeDto> GetPreviousEmployeeAsync(int currentId);

        
    }
}

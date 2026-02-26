using IDAProject.Web.Models.Dto.Contacts;
using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.Dto.Users;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Employees;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IEmployeesManager
    {
        Task<ResponseModelList<EmployeeDto>> SearchEmployeesAsync(SearchEmployeesParams searchParams);

        Task<ResponseModel<EmployeeDto>> GetEmployeeByIdAsync(int id);
        Task<ResponseModel<EmployeeSearchResponseModel>> GetEmailPhoneByEmployeeIdAsync(int id);
        Task<ResponseModelBase> DeleteEmployeeAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveEmployeeAsync(SaveEmployeeRequestModel requestModel);

		Task<ResponseModel<EmployeeDto>> GetNextRowAsync(int currentId);
		Task<ResponseModel<EmployeeDto>> GetPreviousRowAsync(int currentId);


    }
}

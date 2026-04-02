
using IDAProject.Web.Models.Dto.EmployeeJobTypeControls;
using IDAProject.Web.Models.RequestModels.EmployeeJobTypeControls;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IEmployeeJobTypeControlsRepository
    {
        Task<EmployeeJobTypeControlDto> GetEmployeeJobTypeControlByIdAsync(int id);
        Task<int> SaveEmployeeJobTypeControlAsync(SaveEmployeeJobTypeControlRequestModel requestModel);
        Task<List<EmployeeJobTypeControlDto>> SearchEmployeeJobTypeControlsAsync(SearchEmployeeJobTypeControlsParams searchParams);
        Task DeleteEmployeeJobTypeControlAsync(int id, int? userId);
    }
}

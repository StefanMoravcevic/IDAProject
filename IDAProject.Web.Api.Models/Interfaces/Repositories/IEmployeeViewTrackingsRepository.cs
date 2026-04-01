
using IDAProject.Web.Models.Dto.EmployeeViewTrackings;
using IDAProject.Web.Models.RequestModels.EmployeeViewTrackings;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IEmployeeViewTrackingsRepository
    {
        Task<EmployeeViewTrackingDto> GetEmployeeViewTrackingByIdAsync(int id);
        Task<int> SaveEmployeeViewTrackingAsync(SaveEmployeeViewTrackingRequestModel requestModel);
        Task<List<EmployeeViewTrackingDto>> SearchEmployeeViewTrackingsAsync(SearchEmployeeViewTrackingsParams searchParams);
        Task DeleteEmployeeViewTrackingAsync(int id, int? userId);
    }
}

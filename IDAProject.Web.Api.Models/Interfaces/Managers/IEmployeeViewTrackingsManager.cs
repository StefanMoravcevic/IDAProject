using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.EmployeeViewTrackings;
using IDAProject.Web.Models.RequestModels.EmployeeViewTrackings;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IEmployeeViewTrackingsManager
    {
        Task<ResponseModelList<EmployeeViewTrackingDto>> SearchEmployeeViewTrackingsAsync(SearchEmployeeViewTrackingsParams searchParams);
        Task<ResponseModel<EmployeeViewTrackingDto>> GetEmployeeViewTrackingByIdAsync(int id);
        Task<ResponseModelBase> DeleteEmployeeViewTrackingAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveEmployeeViewTrackingAsync(SaveEmployeeViewTrackingRequestModel requestModel);
    }
}

using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.RegularActivities;
using IDAProject.Web.Models.RequestModels.RegularActivities;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IRegularActivitiesManager
    {
        Task<ResponseModelList<RegularActivityDto>> SearchRegularActivitiesAsync(SearchRegularActivitiesParams searchParams);
        Task<ResponseModel<RegularActivityDto>> GetRegularActivityByIdAsync(int id);
        Task<ResponseModelBase> DeleteRegularActivityAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveRegularActivityAsync(SaveRegularActivityRequestModel requestModel);
    }
}

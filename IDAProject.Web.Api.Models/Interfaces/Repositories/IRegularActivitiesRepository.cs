
using IDAProject.Web.Models.Dto.RegularActivities;
using IDAProject.Web.Models.RequestModels.RegularActivities;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IRegularActivitiesRepository
    {
        Task<RegularActivityDto> GetRegularActivityByIdAsync(int id);
        Task<int> SaveRegularActivityAsync(SaveRegularActivityRequestModel requestModel);
        Task<List<RegularActivityDto>> SearchRegularActivitiesAsync(SearchRegularActivitiesParams searchParams);
        Task DeleteRegularActivityAsync(int id, int? userId);
    }
}

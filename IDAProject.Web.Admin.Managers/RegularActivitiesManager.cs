using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.RegularActivities;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.RegularActivities;

namespace IDAProject.Web.Admin.Managers
{
    public class RegularActivitiesManager : BaseManager, IRegularActivitiesManager
    {
        public RegularActivitiesManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<RegularActivitiesManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<RegularActivityDto>> SearchRegularActivitiesAsync(SearchRegularActivitiesParams searchParams)
        {
            var result =
                await PostAsync<SearchRegularActivitiesParams, ResponseModelList<RegularActivityDto>>($"api/RegularActivities/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<RegularActivityDto>> GetRegularActivityByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<RegularActivityDto>>($"api/RegularActivities/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteRegularActivityAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/RegularActivities/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveRegularActivityAsync(SaveRegularActivityRequestModel requestModel)
        {
            var result = await PostAsync<SaveRegularActivityRequestModel, ResponseModel<int>>($"api/RegularActivities", requestModel);
            return result;
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.FebiItems;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.FebiItems;

namespace IDAProject.Web.Admin.Managers
{
    public class FebiItemsManager : BaseManager, IFebiItemsManager
    {
        public FebiItemsManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<FebiItemsManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<FebiItemDto>> SearchFebiItemsAsync(SearchFebiItemsParams searchParams)
        {
            var result =
                await PostAsync<SearchFebiItemsParams, ResponseModelList<FebiItemDto>>($"api/FebiItems/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<FebiItemDto>> GetFebiItemByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<FebiItemDto>>($"api/FebiItems/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteFebiItemAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/FebiItems/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveFebiItemAsync(SaveFebiItemRequestModel requestModel)
        {
            var result = await PostAsync<SaveFebiItemRequestModel, ResponseModel<int>>($"api/FebiItems", requestModel);
            return result;
        }
    }
}

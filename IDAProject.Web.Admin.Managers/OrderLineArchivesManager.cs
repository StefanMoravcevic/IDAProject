using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.OrderLineArchives;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.OrderLineArchives;

namespace IDAProject.Web.Admin.Managers
{
    public class OrderLineArchivesManager : BaseManager, IOrderLineArchivesManager
    {
        public OrderLineArchivesManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<OrderLineArchivesManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<OrderLineArchiveDto>> SearchOrderLineArchivesAsync(SearchOrderLineArchivesParams searchParams)
        {
            var result =
                await PostAsync<SearchOrderLineArchivesParams, ResponseModelList<OrderLineArchiveDto>>($"api/OrderLineArchives/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<OrderLineArchiveDto>> GetOrderLineArchiveByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<OrderLineArchiveDto>>($"api/OrderLineArchives/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteOrderLineArchiveAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/OrderLineArchives/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveOrderLineArchiveAsync(SaveOrderLineArchiveRequestModel requestModel)
        {
            var result = await PostAsync<SaveOrderLineArchiveRequestModel, ResponseModel<int>>($"api/OrderLineArchives", requestModel);
            return result;
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.OrderHeaders;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.OrderHeaders;

namespace IDAProject.Web.Admin.Managers
{
    public class OrderHeadersManager : BaseManager, IOrderHeadersManager
    {
        public OrderHeadersManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<OrderHeadersManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<OrderHeaderDto>> SearchOrderHeadersAsync(SearchOrderHeadersParams searchParams)
        {
            var result =
                await PostAsync<SearchOrderHeadersParams, ResponseModelList<OrderHeaderDto>>($"api/OrderHeaders/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<OrderHeaderDto>> GetOrderHeaderByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<OrderHeaderDto>>($"api/OrderHeaders/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteOrderHeaderAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/OrderHeaders/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveOrderHeaderAsync(SaveOrderHeaderRequestModel requestModel)
        {
            var result = await PostAsync<SaveOrderHeaderRequestModel, ResponseModel<int>>($"api/OrderHeaders", requestModel);
            return result;
        }
    }
}

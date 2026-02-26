using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.OrderLines;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.OrderLines;

namespace IDAProject.Web.Admin.Managers
{
    public class OrderLinesManager : BaseManager, IOrderLinesManager
    {
        public OrderLinesManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<OrderLinesManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<OrderLineDto>> SearchOrderLinesAsync(SearchOrderLinesParams searchParams)
        {
            var result =
                await PostAsync<SearchOrderLinesParams, ResponseModelList<OrderLineDto>>($"api/OrderLines/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<OrderLineDto>> GetOrderLineByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<OrderLineDto>>($"api/OrderLines/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteOrderLineAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/OrderLines/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveOrderLineAsync(SaveOrderLineRequestModel requestModel)
        {
            var result = await PostAsync<SaveOrderLineRequestModel, ResponseModel<int>>($"api/OrderLines", requestModel);
            return result;
        }

        public async Task<ResponseModel<int>> IncrementOrderLineCheckedQuantityAsync(SearchOrderLinesParams searchParams)
        {
            var result = await PostAsync<SearchOrderLinesParams, ResponseModel<int>>($"api/OrderLines/incrementScannedQuantity", searchParams);
            return result;
        }

        public async Task<ResponseModelList<OrderLineDto>> SearchArchivedOrderLinesAsync(SearchOrderLinesParams searchParams)
        {
            var result = await PostAsync<SearchOrderLinesParams, ResponseModelList<OrderLineDto>>($"api/OrderLines/searchArchivedLines", searchParams);
            return result;
        }

        public async Task<ResponseModelBase> SearchOrderLines()
        {
            var result = await GetAsync<ResponseModelBase>($"api/OrderLines/run");
            return result;
        }
    }
}

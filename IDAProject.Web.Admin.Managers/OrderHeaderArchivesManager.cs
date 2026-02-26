using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.OrderHeaderArchives;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.OrderHeaderArchives;

namespace IDAProject.Web.Admin.Managers
{
    public class OrderHeaderArchivesManager : BaseManager, IOrderHeaderArchivesManager
    {
        public OrderHeaderArchivesManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<OrderHeaderArchivesManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<OrderHeaderArchiveDto>> SearchOrderHeaderArchivesAsync(SearchOrderHeaderArchivesParams searchParams)
        {
            var result =
                await PostAsync<SearchOrderHeaderArchivesParams, ResponseModelList<OrderHeaderArchiveDto>>($"api/OrderHeaderArchives/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<OrderHeaderArchiveDto>> GetOrderHeaderArchiveByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<OrderHeaderArchiveDto>>($"api/OrderHeaderArchives/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteOrderHeaderArchiveAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/OrderHeaderArchives/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveOrderHeaderArchiveAsync(SaveOrderHeaderArchiveRequestModel requestModel)
        {
            var result = await PostAsync<SaveOrderHeaderArchiveRequestModel, ResponseModel<int>>($"api/OrderHeaderArchives", requestModel);
            return result;
        }
    }
}

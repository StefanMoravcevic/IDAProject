using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.OrderHeaders;
using IDAProject.Web.Models.RequestModels.OrderHeaders;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IOrderHeadersManager
    {
        Task<ResponseModelList<OrderHeaderDto>> SearchOrderHeadersAsync(SearchOrderHeadersParams searchParams);
        Task<ResponseModel<OrderHeaderDto>> GetOrderHeaderByIdAsync(int id);
        Task<ResponseModelBase> DeleteOrderHeaderAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveOrderHeaderAsync(SaveOrderHeaderRequestModel requestModel);

        Task ArchiveCompletedOrdersAsync();
    }
}

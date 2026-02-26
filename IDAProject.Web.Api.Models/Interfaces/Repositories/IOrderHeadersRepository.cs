
using IDAProject.Web.Models.Dto.OrderHeaders;
using IDAProject.Web.Models.RequestModels.OrderHeaders;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IOrderHeadersRepository
    {
        Task<OrderHeaderDto> GetOrderHeaderByIdAsync(int id);
        Task<int> SaveOrderHeaderAsync(SaveOrderHeaderRequestModel requestModel);
        Task<List<OrderHeaderDto>> SearchOrderHeadersAsync(SearchOrderHeadersParams searchParams);
        Task DeleteOrderHeaderAsync(int id, int? userId);
    }
}

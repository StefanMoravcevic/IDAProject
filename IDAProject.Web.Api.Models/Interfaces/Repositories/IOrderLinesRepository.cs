
using IDAProject.Web.Models.Dto.OrderLines;
using IDAProject.Web.Models.RequestModels.OrderLines;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IOrderLinesRepository
    {
        Task<OrderLineDto> GetOrderLineByIdAsync(int id);
        Task<int> SaveOrderLineAsync(SaveOrderLineRequestModel requestModel);
        Task<List<OrderLineDto>> SearchOrderLinesAsync(SearchOrderLinesParams searchParams);
        Task<List<OrderLineDto>> SearchArchivedOrderLinesAsync(SearchOrderLinesParams searchParams);
        Task DeleteOrderLineAsync(int id, int? userId);
    }
}

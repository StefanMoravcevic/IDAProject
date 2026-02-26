
using IDAProject.Web.Models.Dto.OrderLineArchives;
using IDAProject.Web.Models.RequestModels.OrderLineArchives;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IOrderLineArchivesRepository
    {
        Task<OrderLineArchiveDto> GetOrderLineArchiveByIdAsync(int id);
        Task<int> SaveOrderLineArchiveAsync(SaveOrderLineArchiveRequestModel requestModel);
        Task<List<OrderLineArchiveDto>> SearchOrderLineArchivesAsync(SearchOrderLineArchivesParams searchParams);
        Task DeleteOrderLineArchiveAsync(int id, int? userId);
    }
}

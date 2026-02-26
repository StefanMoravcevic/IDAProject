
using IDAProject.Web.Models.Dto.OrderHeaderArchives;
using IDAProject.Web.Models.RequestModels.OrderHeaderArchives;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IOrderHeaderArchivesRepository
    {
        Task<OrderHeaderArchiveDto> GetOrderHeaderArchiveByIdAsync(int id);
        Task<int> SaveOrderHeaderArchiveAsync(SaveOrderHeaderArchiveRequestModel requestModel);
        Task<List<OrderHeaderArchiveDto>> SearchOrderHeaderArchivesAsync(SearchOrderHeaderArchivesParams searchParams);
        Task DeleteOrderHeaderArchiveAsync(int id, int? userId);
    }
}

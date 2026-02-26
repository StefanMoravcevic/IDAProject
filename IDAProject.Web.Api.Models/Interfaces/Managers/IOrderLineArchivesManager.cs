using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.OrderLineArchives;
using IDAProject.Web.Models.RequestModels.OrderLineArchives;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IOrderLineArchivesManager
    {
        Task<ResponseModelList<OrderLineArchiveDto>> SearchOrderLineArchivesAsync(SearchOrderLineArchivesParams searchParams);
        Task<ResponseModel<OrderLineArchiveDto>> GetOrderLineArchiveByIdAsync(int id);
        Task<ResponseModelBase> DeleteOrderLineArchiveAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveOrderLineArchiveAsync(SaveOrderLineArchiveRequestModel requestModel);
    }
}

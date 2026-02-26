using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.OrderHeaderArchives;
using IDAProject.Web.Models.RequestModels.OrderHeaderArchives;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IOrderHeaderArchivesManager
    {
        Task<ResponseModelList<OrderHeaderArchiveDto>> SearchOrderHeaderArchivesAsync(SearchOrderHeaderArchivesParams searchParams);
        Task<ResponseModel<OrderHeaderArchiveDto>> GetOrderHeaderArchiveByIdAsync(int id);
        Task<ResponseModelBase> DeleteOrderHeaderArchiveAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveOrderHeaderArchiveAsync(SaveOrderHeaderArchiveRequestModel requestModel);
    }
}

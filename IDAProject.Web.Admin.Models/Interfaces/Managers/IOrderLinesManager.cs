using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.OrderLines;
using IDAProject.Web.Models.RequestModels.OrderLines;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IOrderLinesManager
    {
        Task<ResponseModelList<OrderLineDto>> SearchOrderLinesAsync(SearchOrderLinesParams searchParams);
        Task<ResponseModel<OrderLineDto>> GetOrderLineByIdAsync(int id);
        Task<ResponseModelBase> DeleteOrderLineAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveOrderLineAsync(SaveOrderLineRequestModel requestModel);
        Task<ResponseModel<int>> IncrementOrderLineCheckedQuantityAsync(SearchOrderLinesParams searchParams);
        Task<ResponseModelList<OrderLineDto>> SearchArchivedOrderLinesAsync(SearchOrderLinesParams searchParams);
        Task<ResponseModelBase> SearchOrderLines();
    }
}


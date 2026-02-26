using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.OrderLines;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.OrderLines;
using Microsoft.AspNetCore.SignalR;
using IDAProject.Web.Api.Hubs;
using IDAProject.Web.Api.Models.Interfaces.Hubs;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLinesController : ControllerBase
    {
        private readonly IOrderLinesManager _OrderLinesManager;
        private readonly IHubContext<OrderLineHub, IOrderLinesClient> _hubContext;

        public OrderLinesController(IOrderLinesManager OrderLinesManager, IHubContext<OrderLineHub, IOrderLinesClient> hubContext)
        {
            _OrderLinesManager = OrderLinesManager;
            _hubContext = hubContext;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<OrderLineDto>> GetOrderLineByIdAsync(int id)
        {
            var response = await _OrderLinesManager.GetOrderLineByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteOrderLineAsync(int id, int? userId)
        {
            var response = await _OrderLinesManager.DeleteOrderLineAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<OrderLineDto>> SearchOrderLinesAsync(SearchOrderLinesParams searchParams)
        {
            var response = await _OrderLinesManager.SearchOrderLinesAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveOrderLineAsync(SaveOrderLineRequestModel requestModel)
        {
            var response = await _OrderLinesManager.SaveOrderLineAsync(requestModel);
            return response;
        }

        [HttpPost("incrementScannedQuantity")]
        public async Task<ResponseModel<int>> IncrementScannedQuantityAsync(SearchOrderLinesParams searchParams)
        {
            var response = await _OrderLinesManager.IncrementOrderLineCheckedQuantityAsync(searchParams);
            return response;
        }

        [HttpPost("searchArchivedLines")]
        public async Task<ResponseModelList<OrderLineDto>> SearchArchivedOrderLinesAsync(SearchOrderLinesParams searchParams)
        {
            var response = await _OrderLinesManager.SearchArchivedOrderLinesAsync(searchParams);
            return response;
        }

        [HttpGet("run")]
        public async Task<ResponseModelBase> SendDataToClients()
        {
            await _hubContext.Clients.All.SearchOrderLines();

            return new ResponseModelBase { Valid = true };
        }

    }
}

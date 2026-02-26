using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.OrderLineArchives;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.OrderLineArchives;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLineArchivesController : ControllerBase
    {
        private readonly IOrderLineArchivesManager _OrderLineArchivesManager;

        public OrderLineArchivesController(IOrderLineArchivesManager OrderLineArchivesManager)
        {
            _OrderLineArchivesManager = OrderLineArchivesManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<OrderLineArchiveDto>> GetOrderLineArchiveByIdAsync(int id)
        {
            var response = await _OrderLineArchivesManager.GetOrderLineArchiveByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteOrderLineArchiveAsync(int id, int? userId)
        {
            var response = await _OrderLineArchivesManager.DeleteOrderLineArchiveAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<OrderLineArchiveDto>> SearchOrderLineArchivesAsync(SearchOrderLineArchivesParams searchParams)
        {
            var response = await _OrderLineArchivesManager.SearchOrderLineArchivesAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveOrderLineArchiveAsync(SaveOrderLineArchiveRequestModel requestModel)
        {
            var response = await _OrderLineArchivesManager.SaveOrderLineArchiveAsync(requestModel);
            return response;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.OrderHeaderArchives;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.OrderHeaderArchives;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHeaderArchivesController : ControllerBase
    {
        private readonly IOrderHeaderArchivesManager _OrderHeaderArchivesManager;

        public OrderHeaderArchivesController(IOrderHeaderArchivesManager OrderHeaderArchivesManager)
        {
            _OrderHeaderArchivesManager = OrderHeaderArchivesManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<OrderHeaderArchiveDto>> GetOrderHeaderArchiveByIdAsync(int id)
        {
            var response = await _OrderHeaderArchivesManager.GetOrderHeaderArchiveByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteOrderHeaderArchiveAsync(int id, int? userId)
        {
            var response = await _OrderHeaderArchivesManager.DeleteOrderHeaderArchiveAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<OrderHeaderArchiveDto>> SearchOrderHeaderArchivesAsync(SearchOrderHeaderArchivesParams searchParams)
        {
            var response = await _OrderHeaderArchivesManager.SearchOrderHeaderArchivesAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveOrderHeaderArchiveAsync(SaveOrderHeaderArchiveRequestModel requestModel)
        {
            var response = await _OrderHeaderArchivesManager.SaveOrderHeaderArchiveAsync(requestModel);
            return response;
        }
    }
}

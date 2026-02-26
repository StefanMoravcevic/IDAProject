using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.FebiItems;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.FebiItems;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FebiItemsController : ControllerBase
    {
        private readonly IFebiItemsManager _FebiItemsManager;

        public FebiItemsController(IFebiItemsManager FebiItemsManager)
        {
            _FebiItemsManager = FebiItemsManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<FebiItemDto>> GetFebiItemByIdAsync(int id)
        {
            var response = await _FebiItemsManager.GetFebiItemByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteFebiItemAsync(int id, int? userId)
        {
            var response = await _FebiItemsManager.DeleteFebiItemAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<FebiItemDto>> SearchFebiItemsAsync(SearchFebiItemsParams searchParams)
        {
            var response = await _FebiItemsManager.SearchFebiItemsAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveFebiItemAsync(SaveFebiItemRequestModel requestModel)
        {
            var response = await _FebiItemsManager.SaveFebiItemAsync(requestModel);
            return response;
        }
    }
}

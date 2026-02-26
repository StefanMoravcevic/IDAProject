using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Addresses;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Addresses;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressesManager _addressesManager;

        public AddressesController(IAddressesManager addressesManager)
        {
            _addressesManager = addressesManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<AddressDto>> GetAddressByIdAsync(int id)
        {
            var response = await _addressesManager.GetAddressByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteAddressAsync(int id, int? userId)
        {
            var response = await _addressesManager.DeleteAddressAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<AddressDto>> SearchAddressesAsync(SearchAddressesParams searchParams)
        {
            var response = await _addressesManager.SearchAddressesAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveAddressAsync(SaveAddressRequestModel requestModel)
        {
            var response = await _addressesManager.SaveAddressAsync(requestModel);
            return response;
        }
    }
}

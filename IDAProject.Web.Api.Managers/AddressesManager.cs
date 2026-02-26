using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.Addresses;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Addresses;

namespace IDAProject.Web.Api.Managers
{
    public class AddressesManager : IAddressesManager
    {
        private readonly IAddressesRepository _addressesRepository;
        private readonly ILogger _logger;

        public AddressesManager(ILogger<AddressesManager> logger, IAddressesRepository addressesRepository)
        {
            _logger = logger;
            _addressesRepository = addressesRepository;
        }
        public async Task<ResponseModelList<AddressDto>> SearchAddressesAsync(SearchAddressesParams searchParams)
        {
            var result = new ResponseModelList<AddressDto>();
            try
            {
                result.Payload = await _addressesRepository.SearchAddressesAsync(searchParams);
                result.Valid = true;
            }   
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(searchParams);
                _logger.LogError(e,$"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModel<AddressDto>> GetAddressByIdAsync(int id)
        {
            var result = new ResponseModel<AddressDto>();
            try
            {
                result.Payload = await _addressesRepository.GetAddressByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The address  with the specified id could not be found.";
                }
                else
                {
                    result.Valid = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModelBase> DeleteAddressAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _addressesRepository.DeleteAddressAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveAddressAsync(SaveAddressRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _addressesRepository.SaveAddressAsync(requestModel);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }
    }
}

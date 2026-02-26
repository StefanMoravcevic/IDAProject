using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Addresses;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Addresses;

namespace IDAProject.Web.Admin.Managers
{
    public class AddressesManager : BaseManager, IAddressesManager
    {
        public AddressesManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<AddressesManager> logger ,IHttpContextAccessor httpContextAccessor) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<AddressDto>> SearchAddressesAsync(SearchAddressesParams searchParams)
        {
            var result =
                await PostAsync<SearchAddressesParams, ResponseModelList<AddressDto>>($"api/addresses/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<AddressDto>> GetAddressByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<AddressDto>>($"api/addresses/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteAddressAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/addresses/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveAddressAsync(SaveAddressRequestModel requestModel)
        {
            var result = await PostAsync<SaveAddressRequestModel, ResponseModel<int>>($"api/addresses", requestModel);
            return result;
        }

        public Task<ResponseModelList<AddressDto>> GetAddressesByIdAndAddressTypeAsync(int addressType, int id)
        {
            throw new NotImplementedException();
        }
    }
}

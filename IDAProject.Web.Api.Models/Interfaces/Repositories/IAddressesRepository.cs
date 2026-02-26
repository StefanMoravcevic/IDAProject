using IDAProject.Web.Models.Dto.Addresses;
using IDAProject.Web.Models.RequestModels.Addresses;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IAddressesRepository
    {
        Task<AddressDto> GetAddressByIdAsync(int id);
        Task<int> SaveAddressAsync(SaveAddressRequestModel requestModel);
        Task<List<AddressDto>> SearchAddressesAsync(SearchAddressesParams searchParams);
        Task DeleteAddressAsync(int id, int? userId);
    }
}

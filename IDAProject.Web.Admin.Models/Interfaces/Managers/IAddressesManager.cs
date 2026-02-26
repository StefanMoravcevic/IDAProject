using IDAProject.Web.Models.Dto.Addresses;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Addresses;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IAddressesManager
    {
        Task<ResponseModelList<AddressDto>> SearchAddressesAsync(SearchAddressesParams searchParams);
        Task<ResponseModelList<AddressDto>> GetAddressesByIdAndAddressTypeAsync(int addressType, int id);
        Task<ResponseModel<AddressDto>> GetAddressByIdAsync(int id);
        Task<ResponseModelBase> DeleteAddressAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveAddressAsync(SaveAddressRequestModel requestModel);
    }
}

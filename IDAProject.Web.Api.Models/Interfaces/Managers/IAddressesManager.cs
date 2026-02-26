using IDAProject.Web.Models.Dto.Companies;
using IDAProject.Web.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Models.Dto.Addresses;
using IDAProject.Web.Models.RequestModels.Addresses;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IAddressesManager
    {
        Task<ResponseModelList<AddressDto>> SearchAddressesAsync(SearchAddressesParams searchParams);
        Task<ResponseModel<AddressDto>> GetAddressByIdAsync(int id);
        Task<ResponseModelBase> DeleteAddressAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveAddressAsync(SaveAddressRequestModel requestModel);
    }
}

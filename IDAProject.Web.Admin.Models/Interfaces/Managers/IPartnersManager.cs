using IDAProject.Web.Models.Dto.Partners;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Partners;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IPartnersManager
    {
        Task<ResponseModelList<PartnerDto>> SearchPartnersAsync(SearchPartnersParams searchParams);

        Task<ResponseModel<PartnerDto>> GetPartnerByIdAsync(int id);

        Task<ResponseModel<int>> SavePartnerAsync(SavePartnerRequestModel requestModel);

        Task<ResponseModelList<GenericSelectOption>> GetPartnersOptionsByCategoryAsync(int partnerCategory);

        Task<ResponseModelBase> DeletePartnerAsync(int id, int userId);
        
    }
}

using IDAProject.Web.Models.Dto.Partners;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Partners;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
	public interface IPartnersRepository
    {
        Task<List<PartnerDto>> SearchPartnersAsync(SearchPartnersParams searchParams);

        Task<PartnerDto?> GetPartnerByIdAsync(int id);

        Task<int> SavePartnerAsync(SavePartnerRequestModel requestModel);

        Task DeletePartnerAsync(int id, int? userId);

        Task<List<GenericSelectOption>> GetPartnersOptionsByCategoryAsync(int partnerCategory);

    }
}

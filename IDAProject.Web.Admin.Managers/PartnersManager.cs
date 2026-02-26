using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Partners;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Partners;

namespace IDAProject.Web.Admin.Managers
{

    public class PartnersManager : BaseManager, IPartnersManager
    {
        public PartnersManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<PartnersManager> logger, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, configuration, logger)
        {
        }

        public async Task<ResponseModel<PartnerDto>> GetPartnerByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<PartnerDto>>($"api/partners/{id}");
            return result;
        }

        public async Task<ResponseModelList<PartnerDto>> SearchPartnersAsync(SearchPartnersParams searchParams)
        {
            var result = await PostAsync<SearchPartnersParams, ResponseModelList<PartnerDto>>($"api/partners/search", searchParams);
            return result;
        }

        public async Task<ResponseModel<int>> SavePartnerAsync(SavePartnerRequestModel requestModel)
        {
            var result = await PostAsync<SavePartnerRequestModel, ResponseModel<int>>($"api/partners", requestModel);
            return result;
        }

        public async Task<ResponseModelBase> DeletePartnerAsync(int id, int userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/partners/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModelList<GenericSelectOption>> GetPartnersOptionsByCategoryAsync(int partnerCategory)
        {
            var result = await GetAsync<ResponseModelList<GenericSelectOption>>($"api/partners/optionsByCategory/{partnerCategory}");
            return result;
        }


    }
}

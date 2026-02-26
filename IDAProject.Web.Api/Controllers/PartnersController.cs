using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Managers;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.Dto.Partners;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Employees;
using IDAProject.Web.Models.RequestModels.Partners;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly IPartnersManager _partnersManager;

        public PartnersController(IPartnersManager partnersManager)
        {
            _partnersManager = partnersManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<PartnerDto>> GetPartnerByIdAsync(int id)
        {
            var response = await _partnersManager.GetPartnerByIdAsync(id);
            return response;
        }


        [HttpPost("search")]
        public async Task<ResponseModelList<PartnerDto>> SearchPartnersAsync(SearchPartnersParams searchParams)
        {
            var response = await _partnersManager.SearchPartnersAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SavePartnerAsync(SavePartnerRequestModel requestModel)
        {
            var response = await _partnersManager.SavePartnerAsync(requestModel);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeletePartnerAsync(int id, int? userId)
        {
            var response = await _partnersManager.DeletePartnerAsync(id, userId);
            return response;
        }

        [HttpGet("optionsByCategory/{partnerCategory}")]
        public async Task<ResponseModelList<GenericSelectOption>> GetPartnersOptionsByCategoryAsync(int partnerCategory)
        {
            var response = await _partnersManager.GetPartnersOptionsByCategoryAsync(partnerCategory);
            return response;
        }


    }
}
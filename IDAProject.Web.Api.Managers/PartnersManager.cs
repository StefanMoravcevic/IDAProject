using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Api.Repositories;
using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.Dto.Partners;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Partners;

namespace IDAProject.Web.Api.Managers
{
    public class PartnersManager : IPartnersManager
    {
        private readonly IPartnersRepository _partnersRepository;
        private readonly ILogger _logger;

        public PartnersManager(ILogger<PartnersManager> logger, IPartnersRepository partnersRepository)
        {
            _logger = logger;
            _partnersRepository = partnersRepository;
        }


        public async Task<ResponseModel<PartnerDto>> GetPartnerByIdAsync(int id)
        {
            var result = new ResponseModel<PartnerDto>();
            try
            {
                result.Payload = await _partnersRepository.GetPartnerByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The partner with the specified id could not be found.";
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

        public async Task<ResponseModel<int>> SavePartnerAsync(SavePartnerRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _partnersRepository.SavePartnerAsync(requestModel);
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

        public async Task<ResponseModelList<PartnerDto>> SearchPartnersAsync(SearchPartnersParams searchParams)
        {
            var result = new ResponseModelList<PartnerDto>();
            try
            {
                result.Payload = await _partnersRepository.SearchPartnersAsync(searchParams);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(searchParams);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModelBase> DeletePartnerAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _partnersRepository.DeletePartnerAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModelList<GenericSelectOption>> GetPartnersOptionsByCategoryAsync(int partnerCategory)
        {
            var result = new ResponseModelList<GenericSelectOption>();
            try
            {
                result.Payload = await _partnersRepository.GetPartnersOptionsByCategoryAsync(partnerCategory);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, "GetPartnersOptionsByCategoryAsync");
            }
            return result;
        }

    }
}
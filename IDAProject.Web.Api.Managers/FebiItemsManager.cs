using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.FebiItems;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.FebiItems;

namespace IDAProject.Web.Api.Managers
{
    public class FebiItemsManager : IFebiItemsManager
    {
        private readonly IFebiItemsRepository _FebiItemsRepository;
        private readonly ILogger _logger;

        public FebiItemsManager(ILogger<FebiItemsManager> logger, IFebiItemsRepository FebiItemsRepository)
        {
            _logger = logger;
            _FebiItemsRepository = FebiItemsRepository;
        }
        public async Task<ResponseModelList<FebiItemDto>> SearchFebiItemsAsync(SearchFebiItemsParams searchParams)
        {
            var result = new ResponseModelList<FebiItemDto>();
            try
            {
                result.Payload = await _FebiItemsRepository.SearchFebiItemsAsync(searchParams);
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

        public async Task<ResponseModel<FebiItemDto>> GetFebiItemByIdAsync(int id)
        {
            var result = new ResponseModel<FebiItemDto>();
            try
            {
                result.Payload = await _FebiItemsRepository.GetFebiItemByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The FebiItem  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteFebiItemAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _FebiItemsRepository.DeleteFebiItemAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveFebiItemAsync(SaveFebiItemRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _FebiItemsRepository.SaveFebiItemAsync(requestModel);
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

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.RegularActivities;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.RegularActivities;

namespace IDAProject.Web.Api.Managers
{
    public class RegularActivitiesManager : IRegularActivitiesManager
    {
        private readonly IRegularActivitiesRepository _RegularActivitiesRepository;
        private readonly ILogger _logger;

        public RegularActivitiesManager(ILogger<RegularActivitiesManager> logger, IRegularActivitiesRepository RegularActivitiesRepository)
        {
            _logger = logger;
            _RegularActivitiesRepository = RegularActivitiesRepository;
        }
        public async Task<ResponseModelList<RegularActivityDto>> SearchRegularActivitiesAsync(SearchRegularActivitiesParams searchParams)
        {
            var result = new ResponseModelList<RegularActivityDto>();
            try
            {
                result.Payload = await _RegularActivitiesRepository.SearchRegularActivitiesAsync(searchParams);
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

        public async Task<ResponseModel<RegularActivityDto>> GetRegularActivityByIdAsync(int id)
        {
            var result = new ResponseModel<RegularActivityDto>();
            try
            {
                result.Payload = await _RegularActivitiesRepository.GetRegularActivityByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The RegularActivity  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteRegularActivityAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _RegularActivitiesRepository.DeleteRegularActivityAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveRegularActivityAsync(SaveRegularActivityRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _RegularActivitiesRepository.SaveRegularActivityAsync(requestModel);
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

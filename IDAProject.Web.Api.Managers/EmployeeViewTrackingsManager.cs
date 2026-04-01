using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.EmployeeViewTrackings;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.EmployeeViewTrackings;

namespace IDAProject.Web.Api.Managers
{
    public class EmployeeViewTrackingsManager : IEmployeeViewTrackingsManager
    {
        private readonly IEmployeeViewTrackingsRepository _EmployeeViewTrackingsRepository;
        private readonly ILogger _logger;

        public EmployeeViewTrackingsManager(ILogger<EmployeeViewTrackingsManager> logger, IEmployeeViewTrackingsRepository EmployeeViewTrackingsRepository)
        {
            _logger = logger;
            _EmployeeViewTrackingsRepository = EmployeeViewTrackingsRepository;
        }
        public async Task<ResponseModelList<EmployeeViewTrackingDto>> SearchEmployeeViewTrackingsAsync(SearchEmployeeViewTrackingsParams searchParams)
        {
            var result = new ResponseModelList<EmployeeViewTrackingDto>();
            try
            {
                result.Payload = await _EmployeeViewTrackingsRepository.SearchEmployeeViewTrackingsAsync(searchParams);
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

        public async Task<ResponseModel<EmployeeViewTrackingDto>> GetEmployeeViewTrackingByIdAsync(int id)
        {
            var result = new ResponseModel<EmployeeViewTrackingDto>();
            try
            {
                result.Payload = await _EmployeeViewTrackingsRepository.GetEmployeeViewTrackingByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The EmployeeViewTracking  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteEmployeeViewTrackingAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _EmployeeViewTrackingsRepository.DeleteEmployeeViewTrackingAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveEmployeeViewTrackingAsync(SaveEmployeeViewTrackingRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _EmployeeViewTrackingsRepository.SaveEmployeeViewTrackingAsync(requestModel);
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

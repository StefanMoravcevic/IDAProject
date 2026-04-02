using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.EmployeeJobTypeControls;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.EmployeeJobTypeControls;

namespace IDAProject.Web.Api.Managers
{
    public class EmployeeJobTypeControlsManager : IEmployeeJobTypeControlsManager
    {
        private readonly IEmployeeJobTypeControlsRepository _EmployeeJobTypeControlsRepository;
        private readonly ILogger _logger;

        public EmployeeJobTypeControlsManager(ILogger<EmployeeJobTypeControlsManager> logger, IEmployeeJobTypeControlsRepository EmployeeJobTypeControlsRepository)
        {
            _logger = logger;
            _EmployeeJobTypeControlsRepository = EmployeeJobTypeControlsRepository;
        }
        public async Task<ResponseModelList<EmployeeJobTypeControlDto>> SearchEmployeeJobTypeControlsAsync(SearchEmployeeJobTypeControlsParams searchParams)
        {
            var result = new ResponseModelList<EmployeeJobTypeControlDto>();
            try
            {
                result.Payload = await _EmployeeJobTypeControlsRepository.SearchEmployeeJobTypeControlsAsync(searchParams);
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

        public async Task<ResponseModel<EmployeeJobTypeControlDto>> GetEmployeeJobTypeControlByIdAsync(int id)
        {
            var result = new ResponseModel<EmployeeJobTypeControlDto>();
            try
            {
                result.Payload = await _EmployeeJobTypeControlsRepository.GetEmployeeJobTypeControlByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The EmployeeJobTypeControl  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteEmployeeJobTypeControlAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _EmployeeJobTypeControlsRepository.DeleteEmployeeJobTypeControlAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveEmployeeJobTypeControlAsync(SaveEmployeeJobTypeControlRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _EmployeeJobTypeControlsRepository.SaveEmployeeJobTypeControlAsync(requestModel);
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

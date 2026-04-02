using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.EmployeeGoals;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.EmployeeGoals;

namespace IDAProject.Web.Api.Managers
{
    public class EmployeeGoalsManager : IEmployeeGoalsManager
    {
        private readonly IEmployeeGoalsRepository _EmployeeGoalsRepository;
        private readonly ILogger _logger;

        public EmployeeGoalsManager(ILogger<EmployeeGoalsManager> logger, IEmployeeGoalsRepository EmployeeGoalsRepository)
        {
            _logger = logger;
            _EmployeeGoalsRepository = EmployeeGoalsRepository;
        }
        public async Task<ResponseModelList<EmployeeGoalDto>> SearchEmployeeGoalsAsync(SearchEmployeeGoalsParams searchParams)
        {
            var result = new ResponseModelList<EmployeeGoalDto>();
            try
            {
                result.Payload = await _EmployeeGoalsRepository.SearchEmployeeGoalsAsync(searchParams);
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

        public async Task<ResponseModel<EmployeeGoalDto>> GetEmployeeGoalByIdAsync(int id)
        {
            var result = new ResponseModel<EmployeeGoalDto>();
            try
            {
                result.Payload = await _EmployeeGoalsRepository.GetEmployeeGoalByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The EmployeeGoal  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteEmployeeGoalAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _EmployeeGoalsRepository.DeleteEmployeeGoalAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveEmployeeGoalAsync(SaveEmployeeGoalRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _EmployeeGoalsRepository.SaveEmployeeGoalAsync(requestModel);
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

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.TasksPlannings;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.TasksPlannings;

namespace IDAProject.Web.Api.Managers
{
    public class TasksPlanningsManager : ITasksPlanningsManager
    {
        private readonly ITasksPlanningsRepository _TasksPlanningsRepository;
        private readonly ILogger _logger;

        public TasksPlanningsManager(ILogger<TasksPlanningsManager> logger, ITasksPlanningsRepository TasksPlanningsRepository)
        {
            _logger = logger;
            _TasksPlanningsRepository = TasksPlanningsRepository;
        }
        public async Task<ResponseModelList<TasksPlanningDto>> SearchTasksPlanningsAsync(SearchTasksPlanningsParams searchParams)
        {
            var result = new ResponseModelList<TasksPlanningDto>();
            try
            {
                result.Payload = await _TasksPlanningsRepository.SearchTasksPlanningsAsync(searchParams);
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

        public async Task<ResponseModel<TasksPlanningDto>> GetTasksPlanningByIdAsync(int id)
        {
            var result = new ResponseModel<TasksPlanningDto>();
            try
            {
                result.Payload = await _TasksPlanningsRepository.GetTasksPlanningByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The TasksPlanning  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteTasksPlanningAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _TasksPlanningsRepository.DeleteTasksPlanningAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveTasksPlanningAsync(SaveTasksPlanningRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _TasksPlanningsRepository.SaveTasksPlanningAsync(requestModel);
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

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.TasksRealizations;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.TasksRealizations;

namespace IDAProject.Web.Api.Managers
{
    public class TasksRealizationsManager : ITasksRealizationsManager
    {
        private readonly ITasksRealizationsRepository _TasksRealizationsRepository;
        private readonly ILogger _logger;

        public TasksRealizationsManager(ILogger<TasksRealizationsManager> logger, ITasksRealizationsRepository TasksRealizationsRepository)
        {
            _logger = logger;
            _TasksRealizationsRepository = TasksRealizationsRepository;
        }
        public async Task<ResponseModelList<TasksRealizationDto>> SearchTasksRealizationsAsync(SearchTasksRealizationsParams searchParams)
        {
            var result = new ResponseModelList<TasksRealizationDto>();
            try
            {
                result.Payload = await _TasksRealizationsRepository.SearchTasksRealizationsAsync(searchParams);
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

        public async Task<ResponseModel<TasksRealizationDto>> GetTasksRealizationByIdAsync(int id)
        {
            var result = new ResponseModel<TasksRealizationDto>();
            try
            {
                result.Payload = await _TasksRealizationsRepository.GetTasksRealizationByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The TasksRealization  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteTasksRealizationAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _TasksRealizationsRepository.DeleteTasksRealizationAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveTasksRealizationAsync(SaveTasksRealizationRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _TasksRealizationsRepository.SaveTasksRealizationAsync(requestModel);
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

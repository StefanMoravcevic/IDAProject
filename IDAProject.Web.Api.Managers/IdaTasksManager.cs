using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Models.Dto.IdaTasks;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.IdaTasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IDAProject.Web.Api.Managers
{
    public class IdaTasksManager : IIdaTasksManager
    {
        private readonly IIdaTasksRepository _IdaTasksRepository;
        private readonly ILogger _logger;

        public IdaTasksManager(ILogger<IdaTasksManager> logger, IIdaTasksRepository IdaTasksRepository)
        {
            _logger = logger;
            _IdaTasksRepository = IdaTasksRepository;
        }
        public async Task<ResponseModelList<IdaTaskDto>> SearchIdaTasksAsync(SearchIdaTasksParams searchParams)
        {
            var result = new ResponseModelList<IdaTaskDto>();
            try
            {
                result.Payload = await _IdaTasksRepository.SearchIdaTasksAsync(searchParams);
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

        public async Task<ResponseModel<IdaTaskDto>> GetIdaTaskByIdAsync(int id)
        {
            var result = new ResponseModel<IdaTaskDto>();
            try
            {
                result.Payload = await _IdaTasksRepository.GetIdaTaskByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The IdaTask  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteIdaTaskAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _IdaTasksRepository.DeleteIdaTaskAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveIdaTaskAsync(SaveIdaTaskRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _IdaTasksRepository.SaveIdaTaskAsync(requestModel);
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

        public async Task<ResponseModelList<IdaTaskDto>> GetTasksByProjectAsync(int projectId)
        {
            var result = new ResponseModelList<IdaTaskDto>();
            try
            {
                result.Payload = await _IdaTasksRepository.GetTasksByProjectAsync(projectId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(projectId);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModelList<IdaTaskDto>> GetTaskByTaskIdAsync(int taskId)
        {
            var result = new ResponseModelList<IdaTaskDto>();
            try
            {
                result.Payload = await _IdaTasksRepository.GetTaskByTaskIdAsync(taskId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(taskId);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }
    }
}

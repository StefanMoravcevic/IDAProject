using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.TasksRealizationComments;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.TasksRealizationComments;

namespace IDAProject.Web.Api.Managers
{
    public class TasksRealizationCommentsManager : ITasksRealizationCommentsManager
    {
        private readonly ITasksRealizationCommentsRepository _TasksRealizationCommentsRepository;
        private readonly ILogger _logger;

        public TasksRealizationCommentsManager(ILogger<TasksRealizationCommentsManager> logger, ITasksRealizationCommentsRepository TasksRealizationCommentsRepository)
        {
            _logger = logger;
            _TasksRealizationCommentsRepository = TasksRealizationCommentsRepository;
        }
        public async Task<ResponseModelList<TasksRealizationCommentDto>> SearchTasksRealizationCommentsAsync(SearchTasksRealizationCommentsParams searchParams)
        {
            var result = new ResponseModelList<TasksRealizationCommentDto>();
            try
            {
                result.Payload = await _TasksRealizationCommentsRepository.SearchTasksRealizationCommentsAsync(searchParams);
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

        public async Task<ResponseModel<TasksRealizationCommentDto>> GetTasksRealizationCommentByIdAsync(int id)
        {
            var result = new ResponseModel<TasksRealizationCommentDto>();
            try
            {
                result.Payload = await _TasksRealizationCommentsRepository.GetTasksRealizationCommentByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The TasksRealizationComment  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteTasksRealizationCommentAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _TasksRealizationCommentsRepository.DeleteTasksRealizationCommentAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveTasksRealizationCommentAsync(SaveTasksRealizationCommentRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _TasksRealizationCommentsRepository.SaveTasksRealizationCommentAsync(requestModel);
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

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.TasksPlanningComments;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.TasksPlanningComments;

namespace IDAProject.Web.Api.Managers
{
    public class TasksPlanningCommentsManager : ITasksPlanningCommentsManager
    {
        private readonly ITasksPlanningCommentsRepository _TasksPlanningCommentsRepository;
        private readonly ILogger _logger;

        public TasksPlanningCommentsManager(ILogger<TasksPlanningCommentsManager> logger, ITasksPlanningCommentsRepository TasksPlanningCommentsRepository)
        {
            _logger = logger;
            _TasksPlanningCommentsRepository = TasksPlanningCommentsRepository;
        }
        public async Task<ResponseModelList<TasksPlanningCommentDto>> SearchTasksPlanningCommentsAsync(SearchTasksPlanningCommentsParams searchParams)
        {
            var result = new ResponseModelList<TasksPlanningCommentDto>();
            try
            {
                result.Payload = await _TasksPlanningCommentsRepository.SearchTasksPlanningCommentsAsync(searchParams);
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

        public async Task<ResponseModel<TasksPlanningCommentDto>> GetTasksPlanningCommentByIdAsync(int id)
        {
            var result = new ResponseModel<TasksPlanningCommentDto>();
            try
            {
                result.Payload = await _TasksPlanningCommentsRepository.GetTasksPlanningCommentByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The TasksPlanningComment  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteTasksPlanningCommentAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _TasksPlanningCommentsRepository.DeleteTasksPlanningCommentAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveTasksPlanningCommentAsync(SaveTasksPlanningCommentRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _TasksPlanningCommentsRepository.SaveTasksPlanningCommentAsync(requestModel);
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

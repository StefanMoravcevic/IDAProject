using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.TasksPlanningComments;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.TasksPlanningComments;

namespace IDAProject.Web.Admin.Managers
{
    public class TasksPlanningCommentsManager : BaseManager, ITasksPlanningCommentsManager
    {
        public TasksPlanningCommentsManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TasksPlanningCommentsManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<TasksPlanningCommentDto>> SearchTasksPlanningCommentsAsync(SearchTasksPlanningCommentsParams searchParams)
        {
            var result =
                await PostAsync<SearchTasksPlanningCommentsParams, ResponseModelList<TasksPlanningCommentDto>>($"api/TasksPlanningComments/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<TasksPlanningCommentDto>> GetTasksPlanningCommentByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<TasksPlanningCommentDto>>($"api/TasksPlanningComments/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteTasksPlanningCommentAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/TasksPlanningComments/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveTasksPlanningCommentAsync(SaveTasksPlanningCommentRequestModel requestModel)
        {
            var result = await PostAsync<SaveTasksPlanningCommentRequestModel, ResponseModel<int>>($"api/TasksPlanningComments", requestModel);
            return result;
        }
    }
}

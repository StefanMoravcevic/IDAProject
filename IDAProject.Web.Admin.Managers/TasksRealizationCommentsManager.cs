using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.TasksRealizationComments;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.TasksRealizationComments;

namespace IDAProject.Web.Admin.Managers
{
    public class TasksRealizationCommentsManager : BaseManager, ITasksRealizationCommentsManager
    {
        public TasksRealizationCommentsManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TasksRealizationCommentsManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<TasksRealizationCommentDto>> SearchTasksRealizationCommentsAsync(SearchTasksRealizationCommentsParams searchParams)
        {
            var result =
                await PostAsync<SearchTasksRealizationCommentsParams, ResponseModelList<TasksRealizationCommentDto>>($"api/TasksRealizationComments/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<TasksRealizationCommentDto>> GetTasksRealizationCommentByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<TasksRealizationCommentDto>>($"api/TasksRealizationComments/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteTasksRealizationCommentAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/TasksRealizationComments/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveTasksRealizationCommentAsync(SaveTasksRealizationCommentRequestModel requestModel)
        {
            var result = await PostAsync<SaveTasksRealizationCommentRequestModel, ResponseModel<int>>($"api/TasksRealizationComments", requestModel);
            return result;
        }
    }
}

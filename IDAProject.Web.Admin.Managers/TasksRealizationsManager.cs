using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.TasksRealizations;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.TasksRealizations;

namespace IDAProject.Web.Admin.Managers
{
    public class TasksRealizationsManager : BaseManager, ITasksRealizationsManager
    {
        public TasksRealizationsManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TasksRealizationsManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<TasksRealizationDto>> SearchTasksRealizationsAsync(SearchTasksRealizationsParams searchParams)
        {
            var result =
                await PostAsync<SearchTasksRealizationsParams, ResponseModelList<TasksRealizationDto>>($"api/TasksRealizations/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<TasksRealizationDto>> GetTasksRealizationByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<TasksRealizationDto>>($"api/TasksRealizations/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteTasksRealizationAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/TasksRealizations/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveTasksRealizationAsync(SaveTasksRealizationRequestModel requestModel)
        {
            var result = await PostAsync<SaveTasksRealizationRequestModel, ResponseModel<int>>($"api/TasksRealizations", requestModel);
            return result;
        }
    }
}

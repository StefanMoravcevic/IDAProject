using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.TasksPlannings;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.TasksPlannings;

namespace IDAProject.Web.Admin.Managers
{
    public class TasksPlanningsManager : BaseManager, ITasksPlanningsManager
    {
        public TasksPlanningsManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TasksPlanningsManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<TasksPlanningDto>> SearchTasksPlanningsAsync(SearchTasksPlanningsParams searchParams)
        {
            var result =
                await PostAsync<SearchTasksPlanningsParams, ResponseModelList<TasksPlanningDto>>($"api/TasksPlannings/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<TasksPlanningDto>> GetTasksPlanningByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<TasksPlanningDto>>($"api/TasksPlannings/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteTasksPlanningAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/TasksPlannings/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveTasksPlanningAsync(SaveTasksPlanningRequestModel requestModel)
        {
            var result = await PostAsync<SaveTasksPlanningRequestModel, ResponseModel<int>>($"api/TasksPlannings", requestModel);
            return result;
        }
    }
}

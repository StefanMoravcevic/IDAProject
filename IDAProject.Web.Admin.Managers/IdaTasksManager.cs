using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.IdaTasks;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.IdaTasks;

namespace IDAProject.Web.Admin.Managers
{
    public class IdaTasksManager : BaseManager, IIdaTasksManager
    {
        public IdaTasksManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<IdaTasksManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<IdaTaskDto>> SearchIdaTasksAsync(SearchIdaTasksParams searchParams)
        {
            var result =
                await PostAsync<SearchIdaTasksParams, ResponseModelList<IdaTaskDto>>($"api/IdaTasks/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<IdaTaskDto>> GetIdaTaskByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<IdaTaskDto>>($"api/IdaTasks/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteIdaTaskAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/IdaTasks/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveIdaTaskAsync(SaveIdaTaskRequestModel requestModel)
        {
            var result = await PostAsync<SaveIdaTaskRequestModel, ResponseModel<int>>($"api/IdaTasks", requestModel);
            return result;
        }
    }
}

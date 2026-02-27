using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Projects;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Projects;

namespace IDAProject.Web.Admin.Managers
{
    public class ProjectsManager : BaseManager, IProjectsManager
    {
        public ProjectsManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<ProjectsManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<ProjectDto>> SearchProjectsAsync(SearchProjectsParams searchParams)
        {
            var result =
                await PostAsync<SearchProjectsParams, ResponseModelList<ProjectDto>>($"api/Projects/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<ProjectDto>> GetProjectByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<ProjectDto>>($"api/Projects/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteProjectAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/Projects/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveProjectAsync(SaveProjectRequestModel requestModel)
        {
            var result = await PostAsync<SaveProjectRequestModel, ResponseModel<int>>($"api/Projects", requestModel);
            return result;
        }
    }
}

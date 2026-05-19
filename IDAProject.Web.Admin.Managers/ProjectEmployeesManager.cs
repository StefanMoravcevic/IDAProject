using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.ProjectEmployees;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.ProjectEmployees;

namespace IDAProject.Web.Admin.Managers
{
    public class ProjectEmployeesManager : BaseManager, IProjectEmployeesManager
    {
        public ProjectEmployeesManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<ProjectEmployeesManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<ProjectEmployeeDto>> SearchProjectEmployeesAsync(SearchProjectEmployeesParams searchParams)
        {
            var result =
                await PostAsync<SearchProjectEmployeesParams, ResponseModelList<ProjectEmployeeDto>>($"api/ProjectEmployees/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<ProjectEmployeeDto>> GetProjectEmployeeByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<ProjectEmployeeDto>>($"api/ProjectEmployees/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteProjectEmployeeAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/ProjectEmployees/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveProjectEmployeeAsync(SaveProjectEmployeeRequestModel requestModel)
        {
            var result = await PostAsync<SaveProjectEmployeeRequestModel, ResponseModel<int>>($"api/ProjectEmployees", requestModel);
            return result;
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.EmployeeJobTypeControls;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.EmployeeJobTypeControls;

namespace IDAProject.Web.Admin.Managers
{
    public class EmployeeJobTypeControlsManager : BaseManager, IEmployeeJobTypeControlsManager
    {
        public EmployeeJobTypeControlsManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<EmployeeJobTypeControlsManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<EmployeeJobTypeControlDto>> SearchEmployeeJobTypeControlsAsync(SearchEmployeeJobTypeControlsParams searchParams)
        {
            var result =
                await PostAsync<SearchEmployeeJobTypeControlsParams, ResponseModelList<EmployeeJobTypeControlDto>>($"api/EmployeeJobTypeControls/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<EmployeeJobTypeControlDto>> GetEmployeeJobTypeControlByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<EmployeeJobTypeControlDto>>($"api/EmployeeJobTypeControls/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteEmployeeJobTypeControlAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/EmployeeJobTypeControls/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveEmployeeJobTypeControlAsync(SaveEmployeeJobTypeControlRequestModel requestModel)
        {
            var result = await PostAsync<SaveEmployeeJobTypeControlRequestModel, ResponseModel<int>>($"api/EmployeeJobTypeControls", requestModel);
            return result;
        }
    }
}

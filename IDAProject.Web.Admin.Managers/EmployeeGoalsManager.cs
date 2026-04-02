using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.EmployeeGoals;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.EmployeeGoals;

namespace IDAProject.Web.Admin.Managers
{
    public class EmployeeGoalsManager : BaseManager, IEmployeeGoalsManager
    {
        public EmployeeGoalsManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<EmployeeGoalsManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<EmployeeGoalDto>> SearchEmployeeGoalsAsync(SearchEmployeeGoalsParams searchParams)
        {
            var result =
                await PostAsync<SearchEmployeeGoalsParams, ResponseModelList<EmployeeGoalDto>>($"api/EmployeeGoals/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<EmployeeGoalDto>> GetEmployeeGoalByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<EmployeeGoalDto>>($"api/EmployeeGoals/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteEmployeeGoalAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/EmployeeGoals/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveEmployeeGoalAsync(SaveEmployeeGoalRequestModel requestModel)
        {
            var result = await PostAsync<SaveEmployeeGoalRequestModel, ResponseModel<int>>($"api/EmployeeGoals", requestModel);
            return result;
        }
    }
}

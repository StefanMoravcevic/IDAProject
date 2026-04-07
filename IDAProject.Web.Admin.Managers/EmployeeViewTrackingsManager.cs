using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.EmployeeViewTrackings;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.EmployeeViewTrackings;

namespace IDAProject.Web.Admin.Managers
{
    public class EmployeeViewTrackingsManager : BaseManager, IEmployeeViewTrackingsManager
    {
        public EmployeeViewTrackingsManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<EmployeeViewTrackingsManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<EmployeeViewTrackingDto>> SearchEmployeeViewTrackingsAsync(SearchEmployeeViewTrackingsParams searchParams)
        {
            var result =
                await PostAsync<SearchEmployeeViewTrackingsParams, ResponseModelList<EmployeeViewTrackingDto>>($"api/EmployeeViewTrackings/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<EmployeeViewTrackingDto>> GetEmployeeViewTrackingByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<EmployeeViewTrackingDto>>($"api/EmployeeViewTrackings/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteEmployeeViewTrackingAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/EmployeeViewTrackings/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveEmployeeViewTrackingAsync(SaveEmployeeViewTrackingRequestModel requestModel)
        {
            var result = await PostAsync<SaveEmployeeViewTrackingRequestModel, ResponseModel<int>>($"api/EmployeeViewTrackings", requestModel);
            return result;
        }

        public async Task<ResponseModelList<EmployeeViewTrackingDto>> GetBookmarkedEmployeesWithoutPlanNextWorkingDayAsync(List<int> bookmarkedEmployeeIds)
        {
            var result =
                await PostAsync<List<int>, ResponseModelList <EmployeeViewTrackingDto>>($"api/EmployeeViewTrackings/getBookmarkedEmployeesWithoutPlanNextWorkingDay",
                    bookmarkedEmployeeIds);
            return result;
        }
    }
}

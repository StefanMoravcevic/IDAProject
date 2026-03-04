using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.EmployeeAbsences;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.EmployeeAbsences;

namespace IDAProject.Web.Admin.Managers
{
    public class EmployeeAbsencesManager : BaseManager, IEmployeeAbsencesManager
    {
        public EmployeeAbsencesManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<EmployeeAbsencesManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<EmployeeAbsenceDto>> SearchEmployeeAbsencesAsync(SearchEmployeeAbsencesParams searchParams)
        {
            var result =
                await PostAsync<SearchEmployeeAbsencesParams, ResponseModelList<EmployeeAbsenceDto>>($"api/EmployeeAbsences/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<EmployeeAbsenceDto>> GetEmployeeAbsenceByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<EmployeeAbsenceDto>>($"api/EmployeeAbsences/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteEmployeeAbsenceAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/EmployeeAbsences/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveEmployeeAbsenceAsync(SaveEmployeeAbsenceRequestModel requestModel)
        {
            var result = await PostAsync<SaveEmployeeAbsenceRequestModel, ResponseModel<int>>($"api/EmployeeAbsences", requestModel);
            return result;
        }
    }
}

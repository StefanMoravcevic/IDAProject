using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Periods;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Periods;

namespace IDAProject.Web.Admin.Managers
{
    public class PeriodsManager : BaseManager, IPeriodsManager
    {
        public PeriodsManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<PeriodsManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<PeriodDto>> SearchPeriodsAsync(SearchPeriodsParams searchParams)
        {
            var result =
                await PostAsync<SearchPeriodsParams, ResponseModelList<PeriodDto>>($"api/Periods/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<PeriodDto>> GetPeriodByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<PeriodDto>>($"api/Periods/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeletePeriodAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/Periods/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SavePeriodAsync(SavePeriodRequestModel requestModel)
        {
            var result = await PostAsync<SavePeriodRequestModel, ResponseModel<int>>($"api/Periods", requestModel);
            return result;
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.ScannedLines;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.ScannedLines;

namespace IDAProject.Web.Admin.Managers
{
    public class ScannedLinesManager : BaseManager, IScannedLinesManager
    {
        public ScannedLinesManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<ScannedLinesManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<ScannedLineDto>> SearchScannedLinesAsync(SearchScannedLinesParams searchParams)
        {
            var result =
                await PostAsync<SearchScannedLinesParams, ResponseModelList<ScannedLineDto>>($"api/ScannedLines/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<ScannedLineDto>> GetScannedLineByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<ScannedLineDto>>($"api/ScannedLines/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteScannedLineAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/ScannedLines/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveScannedLineAsync(SaveScannedLineRequestModel requestModel)
        {
            var result = await PostAsync<SaveScannedLineRequestModel, ResponseModel<int>>($"api/ScannedLines", requestModel);
            return result;
        }
    }
}

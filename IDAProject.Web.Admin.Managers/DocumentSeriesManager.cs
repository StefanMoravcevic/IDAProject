using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.DocumentSeries;
using IDAProject.Web.Models.RequestModels.DocumentSeries;

namespace IDAProject.Web.Admin.Managers
{
    public class DocumentSeriesManager : BaseManager, IDocumentSeriesManager
    {
        public DocumentSeriesManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<DocumentSeriesManager> logger,IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, configuration, logger)
        {
        }

        public async Task<ResponseModel<DocumentSerieDto>> GetDocumentSerieByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<DocumentSerieDto>>($"api/documentSeries/{id}");
            return result;
        }

        public async Task<ResponseModelList<DocumentSerieDto>> SearchDocumentSeriesAsync(SearchDocumentSeriesParams searchParams)
        {
            var result = await PostAsync<SearchDocumentSeriesParams, ResponseModelList<DocumentSerieDto>>($"api/documentSeries/search", searchParams);
            return result;
        }

        public async Task<ResponseModel<int>> SaveDocumentSerieAsync(SaveDocumentSerieRequestModel requestModel)
        {
            var result = await PostAsync<SaveDocumentSerieRequestModel, ResponseModel<int>>($"api/documentSeries", requestModel);
            return result;
        }
        public async Task<ResponseModel<string>> GetNewNumberAsync(int documentSerieTypeId)
        {
            var result = await GetAsync<ResponseModel<string>>($"api/documentSeries/getNewNumber/{documentSerieTypeId}");
            return result;
        }
        public async Task<ResponseModelBase> DeleteDocumentSerieAsync(int id, int userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/documentSeries/delete/{id}/{userId}");
            return result;
        }

    }
}

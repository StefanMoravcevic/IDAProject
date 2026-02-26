using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.DocumentSeries;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.DocumentSeries;

namespace IDAProject.Web.Api.Managers
{
    public class DocumentSeriesManager : IDocumentSeriesManager
    {
        private readonly IDocumentSeriesRepository _documentSeriesRepository;
        private readonly ILogger _logger;

        public DocumentSeriesManager(ILogger<DocumentSeriesManager> logger, IDocumentSeriesRepository documentSeriesRepository)
        {
            _logger = logger;
            _documentSeriesRepository = documentSeriesRepository;
        }

        public async Task<ResponseModelList<DocumentSerieDto>> SearchDocumentSeriesAsync(SearchDocumentSeriesParams searchParams)
        {
            var result = new ResponseModelList<DocumentSerieDto>();
            try
            {
                result.Payload = await _documentSeriesRepository.SearchDocumentSeriesAsync(searchParams);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(searchParams);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModel<DocumentSerieDto>> GetDocumentSerieByIdAsync(int id)
        {
            var result = new ResponseModel<DocumentSerieDto>();
            try
            {
                result.Payload = await _documentSeriesRepository.GetDocumentSerieByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The documentSerie with the specified id could not be found.";
                }
                else
                {
                    result.Valid = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }
        public async Task<ResponseModelBase> DeleteDocumentSerieAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _documentSeriesRepository.DeleteDocumentSerieAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveDocumentSerieAsync(SaveDocumentSerieRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _documentSeriesRepository.SaveDocumentSerieAsync(requestModel);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModel<string>> GetNewNumberAsync(int documentSerieTypeId)
        {
            var result = new ResponseModel<string>();
            try
            {
                result.Payload = await _documentSeriesRepository.GetNewNumberAsync(documentSerieTypeId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(documentSerieTypeId);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

    }
}
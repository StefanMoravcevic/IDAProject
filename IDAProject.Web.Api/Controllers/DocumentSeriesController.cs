using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.DocumentSeries;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.DocumentSeries;

namespace IDAProject.Web.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DocumentSeriesController : ControllerBase
    {
        private readonly IDocumentSeriesManager _documentSeriesManager;

        public DocumentSeriesController(IDocumentSeriesManager documentSeriesManager)
        {
            _documentSeriesManager = documentSeriesManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<DocumentSerieDto>> GetDocumentSeriesByIdAsync(int id)
        {
            var response = await _documentSeriesManager.GetDocumentSerieByIdAsync(id);
            return response;
        }
        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteDocumentSerieAsync(int id, int? userId)
        {
            var response = await _documentSeriesManager.DeleteDocumentSerieAsync(id, userId);
            return response;
        }
        [HttpPost("search")]
        public async Task<ResponseModelList<DocumentSerieDto>> SearchDocumentSeriesAsync(SearchDocumentSeriesParams searchParams)
        {
            var response = await _documentSeriesManager.SearchDocumentSeriesAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveDocumentSerieAsync(SaveDocumentSerieRequestModel requestModel)
        {
            var response = await _documentSeriesManager.SaveDocumentSerieAsync(requestModel);
            return response;
        }

        [HttpGet("getNewNumber/{documentSerieTypeId}")]
        public async Task<ResponseModel<string>> GetNewNumberAsync(int documentSerieTypeId)
        {
            var response = await _documentSeriesManager.GetNewNumberAsync(documentSerieTypeId);
            return response;
        }

    }
}
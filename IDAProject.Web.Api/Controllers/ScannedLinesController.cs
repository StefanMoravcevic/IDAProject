using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.ScannedLines;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.ScannedLines;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScannedLinesController : ControllerBase
    {
        private readonly IScannedLinesManager _ScannedLinesManager;

        public ScannedLinesController(IScannedLinesManager ScannedLinesManager)
        {
            _ScannedLinesManager = ScannedLinesManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<ScannedLineDto>> GetScannedLineByIdAsync(int id)
        {
            var response = await _ScannedLinesManager.GetScannedLineByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteScannedLineAsync(int id, int? userId)
        {
            var response = await _ScannedLinesManager.DeleteScannedLineAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<ScannedLineDto>> SearchScannedLinesAsync(SearchScannedLinesParams searchParams)
        {
            var response = await _ScannedLinesManager.SearchScannedLinesAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveScannedLineAsync(SaveScannedLineRequestModel requestModel)
        {
            var response = await _ScannedLinesManager.SaveScannedLineAsync(requestModel);
            return response;
        }
    }
}

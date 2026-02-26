using IDAProject.Web.Api.Managers;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Printers;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Printers;
using IDAProject.Web.Models.Dto.OrderLines;
using Microsoft.AspNetCore.Mvc;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintersController : ControllerBase
    {
        private readonly IPrintersManager _printersManager;

        public PrintersController(IPrintersManager PrintersManager)
        {
            _printersManager = PrintersManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<PrinterDto>> GetPrinterByIdAsync(int id)
        {
            var response = await _printersManager.GetPrinterByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeletePrinterAsync(int id, int? userId)
        {
            var response = await _printersManager.DeletePrinterAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<PrinterDto>> SearchPrintersAsync(SearchPrintersParams searchParams)
        {
            var response = await _printersManager.SearchPrintersAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SavePrinterAsync(SavePrinterRequestModel requestModel)
        {
            var response = await _printersManager.SavePrinterAsync(requestModel);
            return response;
        }

        [HttpPost("testPrintWithResponse")]
        public async Task<ResponseModelBase> PrintWithResponse(CustomPrint command)
        {

            return await _printersManager.SendCustomCommandWithResponse(command);
        }

        [HttpPost("checkPrinterStatus")]
        public async Task<ResponseModelBase> CheckPrinterStatus(CustomPrint command)
        {

            return await _printersManager.CheckPrinterStatusAsync(command);
        }

        [HttpPost("queuePrint")]

        public async Task<ResponseModelBase> QueuePrintAsync(PrinterModel model)
        {
            var response = await _printersManager.QueuePrintAsync(model.line, model.UserId.Value);
            return response;
        }
    }
}

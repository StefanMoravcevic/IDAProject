using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Printers;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Printers;
using IDAProject.Web.Models.Dto.OrderLines;

namespace IDAProject.Web.Admin.Managers
{
    public class PrintersManager : BaseManager, IPrintersManager
    {
        public PrintersManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<PrintersManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<PrinterDto>> SearchPrintersAsync(SearchPrintersParams searchParams)
        {
            var result =
                await PostAsync<SearchPrintersParams, ResponseModelList<PrinterDto>>($"api/Printers/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<PrinterDto>> GetPrinterByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<PrinterDto>>($"api/Printers/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeletePrinterAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/Printers/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SavePrinterAsync(SavePrinterRequestModel requestModel)
        {
            var result = await PostAsync<SavePrinterRequestModel, ResponseModel<int>>($"api/Printers", requestModel);
            return result;
        }

        public async Task<ResponseModelBase> QueuePrintAsync(PrinterModel model)
        {
            var result = await PostAsync<PrinterModel, ResponseModelBase>($"api/Printers/queuePrint", model);
            return result;
        }
    }
}

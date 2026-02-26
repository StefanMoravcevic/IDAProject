using IDAProject.Web.Models.Dto.OrderLines;
using IDAProject.Web.Models.Dto.Printers;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Printers;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IPrintersManager
    {
        Task<ResponseModelList<PrinterDto>> SearchPrintersAsync(SearchPrintersParams searchParams);
        Task<ResponseModel<PrinterDto>> GetPrinterByIdAsync(int id);
        Task<ResponseModelBase> DeletePrinterAsync(int id, int? userId);
        Task<ResponseModel<int>> SavePrinterAsync(SavePrinterRequestModel requestModel);
        Task<ResponseModelBase> QueuePrintAsync(OrderLineDto line,int userId);
        Task<ResponseModelBase> SendCustomCommandWithResponse(CustomPrint command);
        Task<ResponseModelBase> CheckPrinterStatusAsync(CustomPrint command);
    }
}

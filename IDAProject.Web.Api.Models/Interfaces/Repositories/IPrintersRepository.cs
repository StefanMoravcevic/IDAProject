
using IDAProject.Web.Models.Dto.Printers;
using IDAProject.Web.Models.RequestModels.Printers;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IPrintersRepository
    {
        Task<PrinterDto> GetPrinterByIdAsync(int id);
        Task<int> SavePrinterAsync(SavePrinterRequestModel requestModel);
        Task<List<PrinterDto>> SearchPrintersAsync(SearchPrintersParams searchParams);
        Task DeletePrinterAsync(int id, int? userId);
    }
}

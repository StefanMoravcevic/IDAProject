using IDAProject.Web.Models.Dto.Printers;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Printers
{
    public class PrinterViewModel : NavigationBaseViewModel
    {
        public PrinterViewModel()
        {
            Printer = new PrinterDto();
        }
        public PrinterDto Printer { get; set; }

        public int ReadOnly { get; set; }

    }
}

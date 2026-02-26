using IDAProject.Web.Admin.Models.Html.AjaxTable;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.Printers
{
    public class PrintersViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public PrintersViewModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id") { HeaderStyle = "width:40px;" },
                new ColumnDefinition("Name", _localizer["Name"]),
                new ColumnDefinition("BarCode", _localizer["BarCode"]),
                new ColumnDefinition("Ip4Address", _localizer["IP address"]),
                new ColumnDefinition("Port", _localizer["Port"])
            };
        }
    }
}

using IDAProject.Web.Admin.Models.Html.AjaxTable;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.ScannedLines
{
    public class ScannedLinesViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public ScannedLinesViewModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            Columns = new List<ColumnDefinition>()
            {
                new ColumnDefinition("Id", _localizer["Id"]) { HeaderStyle = "width:40px;" },
                new ColumnDefinition("CustomerOrderNumber", _localizer["Customer order number"]),
                new ColumnDefinition("FebiArticleNo", _localizer["Febi article no"]),
                new ColumnDefinition("ScannedQuantity", _localizer["Scanned quantity"]),
                new ColumnDefinition("RequestedQuantity", _localizer["Requested quantity"]),
                new ColumnDefinition("DateFormatted", _localizer["Date"]),
                new ColumnDefinition("Options", _localizer["Options"])
            };
        }

        //add view model properties here

    }
}

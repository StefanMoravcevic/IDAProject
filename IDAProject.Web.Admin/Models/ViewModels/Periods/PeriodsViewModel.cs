using IDAProject.Web.Admin.Models.Html.AjaxTable;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.Periods
{
    public class PeriodsViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public PeriodsViewModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id", _localizer["Id"]) { HeaderStyle = "width:40px;" },
                new ColumnDefinition("Code", _localizer["Code"]),
                new ColumnDefinition("Name", _localizer["Name"]),
                new ColumnDefinition("DateFromFormatted", _localizer["Time from"]),
                new ColumnDefinition("DateToFormatted", _localizer["Time to"])
            };
        }

    }
}

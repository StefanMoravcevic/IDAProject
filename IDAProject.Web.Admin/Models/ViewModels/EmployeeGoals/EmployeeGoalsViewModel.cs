using IDAProject.Web.Admin.Models.Html.AjaxTable;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.EmployeeGoals
{
    public class EmployeeGoalsViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public EmployeeGoalsViewModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id", _localizer["Id"]) { HeaderStyle = "width:40px;" },
                new ColumnDefinition("Year", _localizer["Year"]),
                new ColumnDefinition("Employee", _localizer["Employee"]),
                new ColumnDefinition("Goal", _localizer["Goal"])
            };
        }

        //add view model properties here

    }
}

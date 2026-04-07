using IDAProject.Web.Admin.Models.Html.AjaxTable;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.EmployeeGoals
{
    public class EmployeeGoalsViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public EmployeeGoalsViewModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            Employees = new List<GenericSelectOption>();
            Years = new List<GenericSelectOption>();
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id", _localizer["Id"]) { HeaderStyle = "width:40px;" },
                new ColumnDefinition("Year", _localizer["Year"]),
                new ColumnDefinition("Employee", _localizer["Employee"]),
                new ColumnDefinition("Goal", _localizer["Goal"])
            };
        }

        public IEnumerable<ISelectOption> Employees { get; set; }
        public IEnumerable<ISelectOption> Years { get; set; }

    }
}

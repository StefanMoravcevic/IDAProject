using IDAProject.Web.Admin.Models.Html.AjaxTable;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.EmployeeJobTypeControls
{
    public class EmployeeJobTypeControlsViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public EmployeeJobTypeControlsViewModel(IStringLocalizer<SharedResources> localizer)
        {

            _localizer = localizer;
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id", _localizer["Id"]) { HeaderStyle = "width:40px;" },
                new ColumnDefinition("Employee", _localizer["Employee"]),
                new ColumnDefinition("JobType", _localizer["Group"])
            };
        }
    }
}

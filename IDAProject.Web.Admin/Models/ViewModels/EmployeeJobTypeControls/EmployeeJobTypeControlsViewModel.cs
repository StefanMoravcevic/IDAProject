using IDAProject.Web.Admin.Models.Html.AjaxTable;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.EmployeeJobTypeControls
{
    public class EmployeeJobTypeControlsViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public EmployeeJobTypeControlsViewModel(IStringLocalizer<SharedResources> localizer)
        {
            Employees = new List<GenericSelectOption>();
            JobTypes = new List<GenericSelectOption>();
            _localizer = localizer;
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id", _localizer["Id"]) { HeaderStyle = "width:40px;" },
                new ColumnDefinition("Employee", _localizer["Employee"]),
                new ColumnDefinition("JobType", _localizer["Group"])
            };
        }

        public IEnumerable<ISelectOption> Employees { get; set; }
        public IEnumerable<ISelectOption> JobTypes { get; set; }
    }
}

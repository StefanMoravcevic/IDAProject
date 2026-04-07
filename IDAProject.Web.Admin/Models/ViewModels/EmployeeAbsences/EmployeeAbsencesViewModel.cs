using IDAProject.Web.Admin.Models.Html.AjaxTable;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.EmployeeAbsences
{
    public class EmployeeAbsencesViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public EmployeeAbsencesViewModel(IStringLocalizer<SharedResources> localizer)
        {

            _localizer = localizer;
            Employees = new List<GenericSelectOption>();
            JobTypes = new List<GenericSelectOption>();
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id", _localizer["Id"]) { HeaderStyle = "width:40px;" },
                new ColumnDefinition("Group", _localizer["Group"]),
                new ColumnDefinition("Employee", _localizer["Employee"]),
                new ColumnDefinition("AbsenceType", _localizer["Absence type"]),
                new ColumnDefinition("DateFromFormatted",_localizer["Date from"]),
                new ColumnDefinition("DateToFormatted",_localizer["Date to"]),
                new ColumnDefinition("Comment", _localizer["Note"])
            };
        }

        public IEnumerable<ISelectOption> Employees { get; set; }
        public IEnumerable<ISelectOption> JobTypes { get; set; }

    }
}

using IDAProject.Web.Admin.Models.Html.AjaxTable;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.Companies
{
    public class OrgUnitsViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public OrgUnitsViewModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id") { HeaderStyle = "width:40px;" },
                new ColumnDefinition("Code", _localizer["Code"]),
                new ColumnDefinition("Name", _localizer["Name"]),
                new ColumnDefinition("ParentOrgUnit", _localizer["Parent org unit"])
            };
            Company = String.Empty;
            //LoanRequests = new List<GenericSelectOption>();
            //Employees = new List<GenericSelectOption>();
        }
        //public IEnumerable<ISelectOption> LoanRequests { get; set; }
        //public IEnumerable<ISelectOption> Employees { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }

    }
}

using Microsoft.Extensions.Localization;
using IDAProject.Web.Admin.Models.Html.AjaxTable;

namespace IDAProject.Web.Admin.Models.ViewModels.Employees
{
    public class EmployeesViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public EmployeesViewModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            //JobTypes = new List<int>();
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id", _localizer["Id"]) { HeaderStyle = "width:40px;" },
                new ColumnDefinition("EmployeeNumber", _localizer["EmployeeNumber"]),
                new ColumnDefinition("Photo", _localizer["Photo"]),
                new ColumnDefinition("Name",_localizer["First name"]),
                new ColumnDefinition("Surname",_localizer["Surname"]),
                new ColumnDefinition("JobType", _localizer["JobType"]),
                //new ColumnDefinition("Dispatcher"),
                new ColumnDefinition("Email", _localizer["E-mail"]),
                //new ColumnDefinition("Company", "Company"),
                new ColumnDefinition("OrgUnit", _localizer["OrgUnit"]),
                new ColumnDefinition("BankAccount", _localizer["PersonalBankAccount"]),
                //new ColumnDefinition("Partner", "Subcontrator"),
                new ColumnDefinition("BirthDateFormatted", _localizer["Date of birth"]),
                new ColumnDefinition("BirthPlace", _localizer["BirthPlace"]),
                //new ColumnDefinition("Citizenship", _localizer["Citizenship"]),
                //new ColumnDefinition("PersonalId", _localizer["PersonalId"]),
                //new ColumnDefinition("PassportId", "Passport Id"),
                //new ColumnDefinition("InsuranceNumber", _localizer["InsuranceNumber"]) ,
                //new ColumnDefinition("FederalNumber", "Federal number"),
                new ColumnDefinition("CellPhoneNumber", _localizer["MobilePhone"]),
                new ColumnDefinition("Blocked", _localizer["Active"]) { HeaderStyle = "width:50px; text-align:center", CellStyle = "text-align:center;" }
            };
        }
        //public List<int> JobTypes { get; set; }
        public int JobTypeId { get; set; }

    }
}

using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Employees
{
    public class EmployeeViewModel : NavigationBaseViewModel
    {
        public EmployeeViewModel()
        {
            Employee = new EmployeeDto();
            JobTypes = new List<GenericSelectOption>();
            States = new List<GenericSelectOption>();
            Cities = new List<GenericSelectOption>();
            Partners = new List<GenericSelectOption>();
            NoticeTypes = new List<GenericSelectOption>();
            OrgUnits = new List<GenericSelectOption>();
            Companies = new List<GenericSelectOption>();
            Genders = new List<GenericSelectOption>();
            ZipCodes = new List<GenericSelectOption>();
            Sectors = new List<GenericSelectOption>();
        }

        public EmployeeDto Employee { get; set; }

        public IEnumerable<ISelectOption> JobTypes { get; set; }
        public IEnumerable<ISelectOption> ZipCodes { get; set; }
        public IEnumerable<ISelectOption> States { get; set; }
        public IEnumerable<ISelectOption> Cities { get; set; }
        public IEnumerable<ISelectOption> OrgUnits { get; set; }
        public IEnumerable<ISelectOption> Partners { get; set; }
        public IEnumerable<ISelectOption> Companies { get; set; }
        public IEnumerable<ISelectOption> NoticeTypes { get; set; }
        public IEnumerable<ISelectOption> Genders { get; set; }
        public IEnumerable<ISelectOption> Sectors { get; set; }
        public int ReadOnly { get; set; }
        public int JobTypeId { get; set; }
        public EmployeeDocumentsViewModel? EmployeeDocumentsViewModel { get; set; }
    }
}

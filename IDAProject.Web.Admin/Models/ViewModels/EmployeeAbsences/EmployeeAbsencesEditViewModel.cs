using IDAProject.Web.Models.Dto.EmployeeAbsences;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.EmployeeAbsences
{
    public class EmployeeAbsencesEditViewModel : NavigationBaseViewModel
    {
        public EmployeeAbsencesEditViewModel()
        {
            Absence = new EmployeeAbsenceDto();
            AbsenceTypes = new List<GenericSelectOption>();
            JobTypes = new List<GenericSelectOption>();
            Employees = new List<GenericSelectOption>();
        }


        public EmployeeAbsenceDto Absence { get; set; }
        public IEnumerable<ISelectOption> AbsenceTypes { get; set; }
        public IEnumerable<ISelectOption> JobTypes { get; set; }
        public IEnumerable<ISelectOption> Employees { get; set; }
    }
}

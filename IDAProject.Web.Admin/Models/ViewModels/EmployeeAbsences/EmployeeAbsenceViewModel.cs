using IDAProject.Web.Models.Dto.EmployeeAbsences;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.EmployeeAbsences
{
    public class EmployeeAbsenceViewModel : NavigationBaseViewModel
    {
        public EmployeeAbsenceViewModel()
        {
            EmployeeAbsences = new List<EmployeeAbsenceDto>();
            AbsenceTypes = new List<GenericSelectOption>();
        }
        public List<EmployeeAbsenceDto> EmployeeAbsences { get; set; }
        public IEnumerable<ISelectOption> AbsenceTypes { get; set; }
        public int? EmployeeId { get; set; }

    }
}

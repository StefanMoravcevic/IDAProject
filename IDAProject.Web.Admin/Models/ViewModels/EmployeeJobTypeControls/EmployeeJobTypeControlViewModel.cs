using IDAProject.Web.Models.Dto.EmployeeJobTypeControls;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.EmployeeJobTypeControls
{
    public class EmployeeJobTypeControlViewModel : NavigationBaseViewModel
    {
        public EmployeeJobTypeControlViewModel()
        {
            EmployeeJobTypeControl = new EmployeeJobTypeControlDto();
            Employees = new List<GenericSelectOption>();
            JobTypes = new List<GenericSelectOption>();
        }
        public EmployeeJobTypeControlDto EmployeeJobTypeControl { get; set; }
        public IEnumerable<ISelectOption> Employees { get; set; }
        public IEnumerable<ISelectOption> JobTypes { get; set; }
        public int ReadOnly { get; set; }

    }
}

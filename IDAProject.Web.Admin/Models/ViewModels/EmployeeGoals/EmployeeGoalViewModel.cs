using IDAProject.Web.Models.Dto.EmployeeGoals;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.EmployeeGoals
{
    public class EmployeeGoalViewModel : NavigationBaseViewModel
    {
        public EmployeeGoalViewModel()
        {
            EmployeeGoal = new EmployeeGoalDto();
            Years = new List<GenericSelectOption>();
            Employees = new List<GenericSelectOption>();
        }
        public EmployeeGoalDto EmployeeGoal { get; set; }
        public IEnumerable<ISelectOption> Years { get; set; }
        public IEnumerable<ISelectOption> Employees { get; set; }
        public int ReadOnly { get; set; }

    }
}

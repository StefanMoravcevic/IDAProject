using IDAProject.Web.Models.Dto.EmployeeAbsences;
using IDAProject.Web.Models.Dto.ProjectEmployees;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.ProjectEmployees
{
    public class ProjectEmployeeViewModel : NavigationBaseViewModel
    {
        public ProjectEmployeeViewModel()
        {
            ProjectEmployees = new List<ProjectEmployeeDto>();
            Projects = new List<GenericSelectOption>();
            Employees = new List<GenericSelectOption>();
        }
        public int ProjectId { get; set; }
        public List<ProjectEmployeeDto> ProjectEmployees { get; set; }
        public IEnumerable<ISelectOption> Projects { get; set; }
        public IEnumerable<ISelectOption> Employees { get; set; }

    }
}

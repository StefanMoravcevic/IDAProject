using IDAProject.Web.Models.Dto.Projects;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Projects
{
    public class ProjectViewModel : NavigationBaseViewModel
    {
        public ProjectViewModel()
        {
            Project = new ProjectDto();
        }
        public ProjectDto Project { get; set; }

    }
}

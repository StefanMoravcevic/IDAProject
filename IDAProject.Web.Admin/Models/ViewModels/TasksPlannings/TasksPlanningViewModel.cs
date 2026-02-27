using IDAProject.Web.Models.Dto.TasksPlannings;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.TasksPlannings
{
    public class TasksPlanningViewModel : NavigationBaseViewModel
    {
        public TasksPlanningViewModel()
        {
            TasksPlanning = new TasksPlanningDto();
        }
        public TasksPlanningDto TasksPlanning { get; set; }

    }
}

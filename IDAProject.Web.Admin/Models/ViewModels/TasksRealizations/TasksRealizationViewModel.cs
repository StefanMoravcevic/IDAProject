using IDAProject.Web.Models.Dto.TasksRealizations;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.TasksRealizations
{
    public class TasksRealizationViewModel : NavigationBaseViewModel
    {
        public TasksRealizationViewModel()
        {
            TasksRealization = new TasksRealizationDto();
        }
        public TasksRealizationDto TasksRealization { get; set; }

    }
}

using IDAProject.Web.Models.Dto.TasksRealizationComments;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.TasksRealizationComments
{
    public class TasksRealizationCommentViewModel : NavigationBaseViewModel
    {
        public TasksRealizationCommentViewModel()
        {
            TasksRealizationComment = new TasksRealizationCommentDto();
        }
        public TasksRealizationCommentDto TasksRealizationComment { get; set; }

    }
}

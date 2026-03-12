using IDAProject.Web.Models.Dto.TasksPlanningComments;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.TasksPlanningComments
{
    public class TasksPlanningCommentViewModel : NavigationBaseViewModel
    {
        public TasksPlanningCommentViewModel()
        {
            TasksPlanningComment = new TasksPlanningCommentDto();
        }
        public TasksPlanningCommentDto TasksPlanningComment { get; set; }

    }
}

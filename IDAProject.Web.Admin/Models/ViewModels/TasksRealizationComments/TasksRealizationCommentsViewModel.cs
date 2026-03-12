using IDAProject.Web.Admin.Models.Html.AjaxTable;

namespace IDAProject.Web.Admin.Models.ViewModels.TasksRealizationComments
{
    public class TasksRealizationCommentsViewModel : NavigationWithAjaxTableViewModel
    {
        public TasksRealizationCommentsViewModel()
        {
            
            Columns = new List<ColumnDefinition>()
            {
                //check before use
                new( "Id", "Id"), 
new( "UserId", "UserId"), 
new( "Comment", "Comment"), 
new( "TaskRealizationId", "TaskRealizationId"), 
new( "CreatedAt", "CreatedAt"), 
new( "ParentTaskRealizationCommentId", "ParentTaskRealizationCommentId"), 
            };
        }

        //add view model properties here

    }
}

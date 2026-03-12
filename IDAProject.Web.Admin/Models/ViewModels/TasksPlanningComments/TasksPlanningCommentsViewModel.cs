using IDAProject.Web.Admin.Models.Html.AjaxTable;

namespace IDAProject.Web.Admin.Models.ViewModels.TasksPlanningComments
{
    public class TasksPlanningCommentsViewModel : NavigationWithAjaxTableViewModel
    {
        public TasksPlanningCommentsViewModel()
        {
            
            Columns = new List<ColumnDefinition>()
            {
                //check before use
                new( "Id", "Id"), 
new( "UserId", "UserId"), 
new( "Comment", "Comment"), 
new( "TaskPlanningId", "TaskPlanningId"), 
new( "CreatedAt", "CreatedAt"), 
            };
        }

        //add view model properties here

    }
}

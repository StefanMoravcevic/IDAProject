using IDAProject.Web.Admin.Models.Html.AjaxTable;

namespace IDAProject.Web.Admin.Models.ViewModels.TasksPlannings
{
    public class TasksPlanningsViewModel : NavigationWithAjaxTableViewModel
    {
        public TasksPlanningsViewModel()
        {
            
            Columns = new List<ColumnDefinition>()
            {
                //check before use
                new( "Id", "Id"), 
new( "UserId", "UserId"), 
new( "ProjectId", "ProjectId"), 
new( "TaskId", "TaskId"), 
new( "RegularActivityId", "RegularActivityId"), 
new( "ActivityTypeId", "ActivityTypeId"), 
new( "ActivityName", "ActivityName"), 
new( "TimeFrom", "TimeFrom"), 
new( "TimeTo", "TimeTo"), 
new( "Duration", "Duration"), 
new( "PlanNo", "PlanNo"), 
new( "PlanStatusId", "PlanStatusId"), 
new( "CreatedAt", "CreatedAt"), 
            };
        }

        //add view model properties here

    }
}

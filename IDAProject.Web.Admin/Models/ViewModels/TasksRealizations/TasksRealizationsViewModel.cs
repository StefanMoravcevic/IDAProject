using IDAProject.Web.Admin.Models.Html.AjaxTable;

namespace IDAProject.Web.Admin.Models.ViewModels.TasksRealizations
{
    public class TasksRealizationsViewModel : NavigationWithAjaxTableViewModel
    {
        public TasksRealizationsViewModel()
        {
            
            Columns = new List<ColumnDefinition>()
            {
                //check before use
                new( "Id", "Id"), 
new( "TasksPlanningId", "TasksPlanningId"), 
new( "ActivityTypeId", "ActivityTypeId"), 
new( "ProjectId", "ProjectId"), 
new( "IdaTaskId", "IdaTaskId"), 
new( "RegularActivityId", "RegularActivityId"), 
new( "Activity", "Activity"), 
new( "Report", "Report"), 
new( "TimeFrom", "TimeFrom"), 
new( "TimeTo", "TimeTo"), 
new( "Duration", "Duration"), 
new( "CreatedDate", "CreatedDate"), 
new( "Finished", "Finished"), 
            };
        }

        //add view model properties here

    }
}

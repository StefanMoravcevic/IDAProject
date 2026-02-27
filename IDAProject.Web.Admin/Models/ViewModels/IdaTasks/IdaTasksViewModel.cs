using IDAProject.Web.Admin.Models.Html.AjaxTable;

namespace IDAProject.Web.Admin.Models.ViewModels.IdaTasks
{
    public class IdaTasksViewModel : NavigationWithAjaxTableViewModel
    {
        public IdaTasksViewModel()
        {
            
            Columns = new List<ColumnDefinition>()
            {
                //check before use
                new( "Id", "Id"), 
new( "Name", "Name"), 
new( "Description", "Description"), 
new( "DueDate", "DueDate"), 
new( "ProjectId", "ProjectId"), 
new( "IsCompleted", "IsCompleted"), 
            };
        }

        //add view model properties here

    }
}

using IDAProject.Web.Admin.Models.Html.AjaxTable;

namespace IDAProject.Web.Admin.Models.ViewModels.Projects
{
    public class ProjectsViewModel : NavigationWithAjaxTableViewModel
    {
        public ProjectsViewModel()
        {
            
            Columns = new List<ColumnDefinition>()
            {
                //check before use
                new( "Id", "Id"), 
new( "Description", "Description"), 
new( "IsCompleted", "IsCompleted"), 
            };
        }

        //add view model properties here

    }
}

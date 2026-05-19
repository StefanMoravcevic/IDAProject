using IDAProject.Web.Admin.Models.Html.AjaxTable;

namespace IDAProject.Web.Admin.Models.ViewModels.ProjectEmployees
{
    public class ProjectEmployeesViewModel : NavigationWithAjaxTableViewModel
    {
        public ProjectEmployeesViewModel()
        {
            
            Columns = new List<ColumnDefinition>()
            {
                //check before use
                new( "Id", "Id"), 
new( "ProjectId", "ProjectId"), 
new( "EmployeeId", "EmployeeId"), 
            };
        }

        //add view model properties here

    }
}

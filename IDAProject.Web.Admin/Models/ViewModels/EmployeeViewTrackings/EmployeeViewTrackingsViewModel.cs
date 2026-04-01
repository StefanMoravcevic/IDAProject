using DeclarationFactory.Web.Admin.Models.Html.AjaxTable;

namespace DeclarationFactory.Web.Admin.Models.ViewModels.EmployeeViewTrackings
{
    public class EmployeeViewTrackingsViewModel : NavigationWithAjaxTableViewModel
    {
        public EmployeeViewTrackingsViewModel()
        {
            
            Columns = new List<ColumnDefinition>()
            {
                //check before use
                new( "Id", "Id"), 
new( "ViewerEmployeeId", "ViewerEmployeeId"), 
new( "ViewedEmployeeId", "ViewedEmployeeId"), 
new( "ViewedFrom", "ViewedFrom"), 
new( "ViewedUntil", "ViewedUntil"), 
            };
        }

        //add view model properties here

    }
}

using IDAProject.Web.Admin.Models.Html.AjaxTable;

namespace IDAProject.Web.Admin.Models.ViewModels.RegularActivities
{
    public class RegularActivitiesViewModel : NavigationWithAjaxTableViewModel
    {
        public RegularActivitiesViewModel()
        {
            
            Columns = new List<ColumnDefinition>()
            {
                //check before use
                new( "Id", "Id"), 
new( "Name", "Name"), 
new( "Description", "Description"), 
            };
        }

        //add view model properties here

    }
}

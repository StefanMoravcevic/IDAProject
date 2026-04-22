using DeclarationFactory.Web.Admin.Models.Html.AjaxTable;

namespace DeclarationFactory.Web.Admin.Models.ViewModels.Shifts
{
    public class ShiftsViewModel : NavigationWithAjaxTableViewModel
    {
        public ShiftsViewModel()
        {
            
            Columns = new List<ColumnDefinition>()
            {
                //check before use
                new( "Id", "Id"), 
new( "Shift1", "Shift1"), 
new( "TimeFrom", "TimeFrom"), 
new( "TimeTo", "TimeTo"), 
            };
        }

        //add view model properties here

    }
}

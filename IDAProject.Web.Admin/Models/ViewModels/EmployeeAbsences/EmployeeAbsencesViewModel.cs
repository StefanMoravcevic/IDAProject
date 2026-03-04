using IDAProject.Web.Admin.Models.Html.AjaxTable;

namespace IDAProject.Web.Admin.Models.ViewModels.EmployeeAbsences
{
    public class EmployeeAbsencesViewModel : NavigationWithAjaxTableViewModel
    {
        public EmployeeAbsencesViewModel()
        {
            
            Columns = new List<ColumnDefinition>()
            {
                //check before use
                new( "Id", "Id"), 
new( "EmployeeId", "EmployeeId"), 
new( "AbsenceTypeId", "AbsenceTypeId"), 
new( "DateFrom", "DateFrom"), 
new( "DateTo", "DateTo"), 
new( "AllDay", "AllDay"), 
new( "Comment", "Comment"), 
new( "TimeFrom", "TimeFrom"), 
new( "TimeTo", "TimeTo"), 
            };
        }

        //add view model properties here

    }
}

using IDAProject.Web.Admin.Models.Html.AjaxTable;

namespace IDAProject.Web.Admin.Models.ViewModels.Partners
{
    public class SubcontractorFeesViewModel : NavigationWithAjaxTableViewModel
    {
        public SubcontractorFeesViewModel()
        {
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id") { HeaderStyle = "width:50px;" },
                new ColumnDefinition("RecordDateFormatted", "Record date"),
                new ColumnDefinition("DateFromFormatted", "Start date"),
                new ColumnDefinition("DateToFormatted", "End date") { HeaderStyle = "text-align:right;", CellStyle = "text-align:right;" },
                new ColumnDefinition("FeeFormatted", "Fee") { HeaderStyle = "text-align:right;", CellStyle = "text-align:right;" }
            };
        }
        public int PartnerId { get; set; }

        public string? Name { get; set; }

    }
}

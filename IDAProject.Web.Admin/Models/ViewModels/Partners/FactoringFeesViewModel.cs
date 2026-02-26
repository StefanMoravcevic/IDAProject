using IDAProject.Web.Admin.Models.Html.AjaxTable;

namespace IDAProject.Web.Admin.Models.ViewModels.Partners
{
    public class FactoringFeesViewModel : NavigationWithAjaxTableViewModel
    {
        public FactoringFeesViewModel()
        {
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id") { HeaderStyle = "width:40px;" },
                new ColumnDefinition("CompanyName","Company"),
                new ColumnDefinition("PartnerName","Partner"),
                new ColumnDefinition("DateFromFormatted","From"),
                new ColumnDefinition("DateToFormatted","To"),
                new ColumnDefinition("FeeFormatted", "Fee"),
                new ColumnDefinition("RecordDateFormatted","Record date")
            };
        }
        public int PartnerId { get; set; }
        public string? PartnerName { get; set; }
    }
}

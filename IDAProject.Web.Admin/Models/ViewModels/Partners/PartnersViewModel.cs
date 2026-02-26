using IDAProject.Web.Admin.Models.Html.AjaxTable;

namespace IDAProject.Web.Admin.Models.ViewModels.Partners
{
    public class PartnersViewModel : NavigationWithAjaxTableViewModel
    {
        public PartnersViewModel()
        {
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id") { HeaderStyle = "width:40px;" },
                new ColumnDefinition("Code","Code"),
                new ColumnDefinition("AccountingCode","Accounting code"),
                new ColumnDefinition("Name","Name"),
                new ColumnDefinition("Address", "Address"),
                new ColumnDefinition("ZipCode", "ZipCode"),
                new ColumnDefinition("State", "State"),
                new ColumnDefinition("City","City"),
                new ColumnDefinition("PartnerType", "Partner type"),
                new ColumnDefinition("Ein", "EIN"),
                new ColumnDefinition("Mc", "MC"),
                new ColumnDefinition("Dot", "DOT") { HeaderStyle = "text-align:center", CellStyle = "text-align:center;" },
                new ColumnDefinition("Phone", "Phone #"),
                new ColumnDefinition("Fax", "Fax #"),
                new ColumnDefinition("Email", "E-mail"),
                new ColumnDefinition("ContactPerson", "Contact person"),
                new ColumnDefinition("PaymentCondition", "Payment condition"),
                new ColumnDefinition("PrimaryContact","Primary contact"),
                new ColumnDefinition("IncomeType","Income type"),
                new ColumnDefinition("Blocked") { HeaderStyle = "width:50px; text-align:center", CellStyle = "text-align:center;" },
                new ColumnDefinition("BlockedComment","Blocked comment")
            };
        }
        public int PartnerCategoryId { get; set; }
    }
}

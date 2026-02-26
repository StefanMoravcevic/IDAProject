using IDAProject.Web.Admin.Models.Html.AjaxTable;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Contacts
{
    public class ContactsViewModel : NavigationWithAjaxTableViewModel
    {
        public ContactsViewModel()
        {
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id") { HeaderStyle = "width:40px;" },
                new ColumnDefinition("IsCompany", "Company") { HeaderStyle = "width:80px; text-align:center", CellStyle = "text-align:center;\" Class = \"IsCompany" },
                new ColumnDefinition("Name","Name"),
                //new ColumnDefinition("PartnerName","Partner name"),
                new ColumnDefinition("ContactCompany","Contact company name"),
                new ColumnDefinition("State", "State"),
                new ColumnDefinition("City", "City"),
                new ColumnDefinition("Address", "Address"),
                new ColumnDefinition("ZipCode", "ZipCode"),
                new ColumnDefinition("MobileNo", "Phone #"),
                new ColumnDefinition("Fax", "Fax #"),
                new ColumnDefinition("Email", "E-mail"),
                new ColumnDefinition("InvoiceFlag", "Accept invoices") { HeaderStyle = "width:120px; text-align:center", CellStyle = "text-align:center;" },
                new ColumnDefinition("PreferredMethodOfCommunication", "Communication"),
                new ColumnDefinition("Ein", "EIN"),
                new ColumnDefinition("Mc", "MC"),
                new ColumnDefinition("Dot", "DOT")
            };
            PartnerCategories = new List<GenericSelectOption>();
        }
        public IEnumerable<ISelectOption> PartnerCategories { get; set; }
    }
}

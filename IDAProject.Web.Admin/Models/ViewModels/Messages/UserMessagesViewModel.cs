using IDAProject.Web.Admin.Models.Html.AjaxTable;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Messages
{
    public class UserMessagesViewModel : NavigationWithAjaxTableViewModel
    {
        public UserMessagesViewModel()
        {
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id") { HeaderStyle = "width:40px;" },
                new ColumnDefinition("Sender","Sender name") { HeaderStyle = "width:120px;" },
                new ColumnDefinition("SenderUser","Sender user") { HeaderStyle = "width:100px;" },
                new ColumnDefinition("Receiver","Receiver name") { HeaderStyle = "width:120px;" },
                new ColumnDefinition("ReceiverUser","Receiver user") { HeaderStyle = "width:100px;" },
                new ColumnDefinition("DateFormatted", "Sent date") { HeaderStyle = "width:120px; text-align:center", CellStyle = "text-align:center;" },
                new ColumnDefinition("Message"),
                new ColumnDefinition("File") { HeaderStyle = "width:160px;" },
                //new ColumnDefinition("Active") { HeaderStyle = "width:50px; text-align:center", CellStyle = "text-align:center;" }
            };
        }
    }
}
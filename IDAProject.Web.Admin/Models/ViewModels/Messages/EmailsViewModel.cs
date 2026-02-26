using IDAProject.Web.Admin.Models.Html.AjaxTable;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Messages
{
    public class EmailsViewModel : NavigationWithAjaxTableViewModel
    {
        public EmailsViewModel()
        {
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id") { HeaderStyle = "width:40px;" },
                new ColumnDefinition("Sender","Sender") { HeaderStyle = "width:100px;" },
                new ColumnDefinition("Receiver","Receiver name") { HeaderStyle = "width:120px;" },
                new ColumnDefinition("ReceiverEmail","Receiver e-mail") { HeaderStyle = "width:120px;" },
                new ColumnDefinition("ReceiverUser","Receiver user") { HeaderStyle = "width:100px;" },
                new ColumnDefinition("SentDateFormatted", "Sent date") { HeaderStyle = "width:120px; text-align:center", CellStyle = "text-align:center;" },
                new ColumnDefinition("Subject"),
                new ColumnDefinition("Message"),
                new ColumnDefinition("Attachment") { HeaderStyle = "width:160px;" },
            };
        }
    }
}
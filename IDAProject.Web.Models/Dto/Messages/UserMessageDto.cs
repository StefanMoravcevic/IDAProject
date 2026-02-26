using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.Messages
{
    public class UserMessageDto : SaveUserMessageRequestModel
    {
        public int Id { get; set; }
        public int? ReferenceDocumentId { get; set; }
        public string? Sender { get; set; }
        public string? SenderUser { get; set; }
        public string? Receiver { get; set; }
        public string? ReceiverUser { get; set; }
        public string? File { get; set; }
        public string DateFormatted
        {
            get { return DisplayFormatHelpers.FormatDateTime(CreatedDate); }
        }
    }
}

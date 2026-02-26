using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.Messages
{
    public class EmailDto
    {
        public int Id { get; set; }
        public string? Sender { get; set; }
        public string? ReceiverEmail { get; set; }
        public string? Receiver { get; set; }
        public string? ReceiverUser { get; set; }
        public string? Message { get; set; }
        public string? Subject { get; set; }
        public string? Attachment { get; set; }
        public DateTime? SentDate { get; set; }
        public string SentDateFormatted
        {
            get { return DisplayFormatHelpers.FormatDateTime(SentDate); }
        }
    }
}

namespace IDAProject.Web.Models.Dto.Messages
{
    public class Conversation : ConversationBaseModel
    {
        public Conversation()
        {
            Title = string.Empty;
            SenderImage = string.Empty;
            LastMessage = string.Empty;
            ReceiptImage = string.Empty;
            TimeInfo = string.Empty;
            LastMessageDate = DateTime.Now;
        }

        public string Title { get; set; }

        public string SenderImage { get; set; }

        public string ReceiptImage { get; set; }

        public string LastMessage { get; set; }

        public string TimeInfo { get; set; }

        public DateTime LastMessageDate { get; set; }
    }
}
namespace IDAProject.Web.Models.Dto.Documents
{
    public class EmailQueueDto
    {
        public EmailQueueDto()
        {
            EmailTo = string.Empty;
            Subject = string.Empty;
            Body = string.Empty;
        }

        public int Id { get; set; }
        public string EmailTo { get; set; }
        public string? EmailCc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public DateTime DateQueued { get; set; }
        public DateTime? DateSent { get; set; }
    }
}

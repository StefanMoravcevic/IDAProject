namespace IDAProject.Web.Models.RequestModels.Messages
{
    public class SearchEmailsParams
    {

        public int? Id { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? LastXDays { get; set; }
        public string? Keyword { get; set; }
        public int? DispatcherId { get; set; }
        public string? DispatcherEmail { get; set; }
    }
}

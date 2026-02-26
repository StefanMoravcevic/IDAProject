namespace IDAProject.Web.Models.RequestModels.Contacts
{
    public class SearchContactsParams
    {
        public int? Id { get; set; }
        public bool? IsCompany { get; set; }
        public string? Keyword { get; set; }
        public int? PartnerId { get; set; }
        public int? CompanyId { get; set; }
        public int? ContactCompanyId { get; set; }
    }
}
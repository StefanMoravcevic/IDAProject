namespace IDAProject.Web.Models.RequestModels.Partners
{
    public class SearchPartnersParams
    {
        public int? Id { get; set; }
        public string? Keyword { get; set; }
        //public int? PartnerTypeId { get; set; }
        public int? PartnerCategoryId { get; set; }
        public bool? Blocked { get; set; }
    }
}

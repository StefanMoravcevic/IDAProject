namespace IDAProject.Web.Models.RequestModels.Companies
{
    public class SearchCompaniesParams
    {
        public int? Id { get; set; } 
        public int? ParentCompanyId { get; set; } 
        public int? FactoringHouseId { get; set; }
        public string? Keyword { get; set; }

    }
}

namespace IDAProject.Web.Models.RequestModels.Companies
{
    public class SearchOrgUnitsParams
    {
        public int? Id { get; set; } 
        public int? ParentOrgUnitId { get; set; } 
        public int? CompanyId { get; set; } 

    }
}

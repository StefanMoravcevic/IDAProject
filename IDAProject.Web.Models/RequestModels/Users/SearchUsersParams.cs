namespace IDAProject.Web.Models.RequestModels.Users
{
    public class SearchUsersParams
    {
        public int? Active { get; set; }
        public string? Keyword { get; set; }
        public int? EmployeeId { get; set; }
        public int? OrgUnitId { get; set; }
        public int? OrgUnitIdUser { get; set; }
    }
}

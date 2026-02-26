namespace IDAProject.Web.Models.Dto.Roles
{
    public class SaveRoleRequestModel
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string? Name { get; set; }
    }
}

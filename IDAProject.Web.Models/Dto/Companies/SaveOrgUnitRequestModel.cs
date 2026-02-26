namespace IDAProject.Web.Models.Dto.Companies
{
    public class SaveOrgUnitRequestModel
    {
        public SaveOrgUnitRequestModel()
        {
            Code = string.Empty;
            Name = string.Empty;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int? ParentOrgUnitId { get; set; }
    }
}

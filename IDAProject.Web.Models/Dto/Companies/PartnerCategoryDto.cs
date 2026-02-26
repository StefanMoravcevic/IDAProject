namespace IDAProject.Web.Models.Dto.Companies
{
    public class PartnerCategoryDto
    {
        public PartnerCategoryDto()
        {
            Code = string.Empty;
            Name = string.Empty;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
namespace IDAProject.Web.Models.Dto.Companies
{
    public class PartnerTypeDto
    {
        public PartnerTypeDto()
        {
            PartnerCategory = new PartnerCategoryDto();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; } = null!;
        public PartnerCategoryDto PartnerCategory { get; set; }
    }
}

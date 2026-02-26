namespace IDAProject.Web.Models.Dto.Common
{
    public class MeasureUnitDto
    {
        public MeasureUnitDto()
        {
        }

        public int Id { get; set; }
        public string Sign { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}

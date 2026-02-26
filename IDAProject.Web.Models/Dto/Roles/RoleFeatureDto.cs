using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.Roles
{
    public class RoleFeatureDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FeatureName { get; set; } = null!;

    }
}

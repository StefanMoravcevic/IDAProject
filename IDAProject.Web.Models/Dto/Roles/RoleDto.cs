using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.Roles
{
    public class RoleDto : SaveRoleRequestModel
    {
        public RoleDto()
        {
        }
        public string? Company { get; set; }
    }
}

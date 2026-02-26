
namespace IDAProject.Web.Models.Dto.Users
{
    public class UserRoleDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string UserUsername { get; set; } = null!;
        public string RoleName { get; set; } = null!;
    }
}


namespace IDAProject.Web.Models.Dto.Users
{
    public class UserTableItemDto
    {
        public int Id { get; set; }
        public int NumberOfRoles { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool IsActive { get; set; }
        public string Employee { get; set; } = null!;
        public string? UserCulture { get; set; } = null!;
        public string? PartnerName { get; set; } = null!;
        public string? Roles { get; set; } = null!;
        public string? Printer { get; set; }
    }
}

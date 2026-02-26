namespace IDAProject.Web.Models.Dto.Users
{
    public class SaveUserRequestModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NormalizedEmail { get; set; } = null!;
        public bool EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool IsActive { get; set; }
        public int? EmployeeId { get; set; }
        public int? PrinterId { get; set; }
        public int? PartnerId { get; set; }
        public int? OrgId { get; set; }
        public string? UserCulture { get; set; }
    }
}

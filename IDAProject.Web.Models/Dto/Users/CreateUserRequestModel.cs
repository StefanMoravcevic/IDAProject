namespace IDAProject.Web.Models.Dto.Users
{
    public class CreateUserRequestModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NormalizedEmail { get; set; } = null!;
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string UserCulture { get; set; } = null!;
        public bool PhoneNumberConfirmed { get; set; }
        public bool IsActive { get; set; }
        public int? EmployeeId { get; set; }
        public int? PartnerId { get; set; }
        public int? OrgId { get; set; }
    }
}

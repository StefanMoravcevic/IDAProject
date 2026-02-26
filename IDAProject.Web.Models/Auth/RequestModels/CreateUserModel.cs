namespace IDAProject.Web.Models.Auth.RequestModels
{
    public class CreateUserModel
    {
        public CreateUserModel()
        {
            UserName = string.Empty;
            Email = string.Empty;
            //PhoneNumber = string.Empty;
            Password = string.Empty;
            //UserCulture = string.Empty;
            Roles = new List<string>();
        }

        public int? EmployeeId { get; set; }
        public int Id { get; set; }
        public int? PartnerId { get; set; }
        public int? OrgId { get; set; }
        public int? PrinterId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? UserCulture { get; set; }
        public bool Active { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool EmailConfirmed { get; set; }

        public List<string> Roles { get; set; }
    }
}

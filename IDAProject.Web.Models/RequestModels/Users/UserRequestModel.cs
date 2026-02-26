namespace IDAProject.Web.Models.RequestModels.Users
{
    public class UserRequestModel
    {
        public UserRequestModel()
        {
            Password = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            NormalizedEmail = string.Empty;
            PhoneNumberConfirmed = false;
            EmailConfirmed = false;
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool IsActive { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}

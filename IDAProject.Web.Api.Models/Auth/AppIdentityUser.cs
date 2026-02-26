using Microsoft.AspNetCore.Identity;

namespace IDAProject.Web.Api.Models.Auth
{
    public class AppIdentityUser : IdentityUser<int>
    {

        public AppIdentityUser() : base()
        {
            PartnerName = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            MiddleName = string.Empty;
            FcmToken = string.Empty;
            UserCulture = string.Empty;
        }

        public int? OrgId { get; set; }
        public int? PartnerId { get; set; }
        public int? EmployeeId { get; set; }
        public int? PrinterId { get; set; }
        public string PartnerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string? UserCulture { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// FCM registration token
        /// </summary>
        public string FcmToken { get; set; }
    }
}

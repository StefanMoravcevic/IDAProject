namespace IDAProject.Web.Models.Dto.Users
{
    public class UserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string NormalizedUserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string NormalizedEmail { get; set; } = null!;

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; } = null!;

        public string SecurityStamp { get; set; } = null!;

        public string? ConcurrencyStamp { get; set; }

        public string? PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public bool IsActive { get; set; }

        public int? EmployeeId { get; set; }
        public int? PrinterId { get; set; }

        /// <summary>
        /// FCM registration token
        /// </summary>
        public string? FcmToken { get; set; }

        public string? UserCulture { get; set; }

        public int? PartnerId { get; set; }

        public int? OrgId { get; set; }

    }
}
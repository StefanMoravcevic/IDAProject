using System.ComponentModel.DataAnnotations;

namespace IDAProject.Web.Models.Auth.RequestModels
{
    public class ChangePasswordModel
    {
        public int? UserId { get; set; }
        public string? Username { get; set; }
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
    }
}

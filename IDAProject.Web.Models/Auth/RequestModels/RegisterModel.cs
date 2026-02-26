using System.ComponentModel.DataAnnotations;

namespace IDAProject.Web.Models.Auth.RequestModels
{
    public class RegisterModel
    {
        public string? Username { get; set; }

        public string? Email { get; set; }
    }
}

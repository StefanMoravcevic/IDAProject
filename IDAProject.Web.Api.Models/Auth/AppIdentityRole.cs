using Microsoft.AspNetCore.Identity;

namespace IDAProject.Web.Api.Models.Auth
{
    public class AppIdentityRole : IdentityRole<int>
    {
        public AppIdentityRole() : base()
        {

        }
    }
}
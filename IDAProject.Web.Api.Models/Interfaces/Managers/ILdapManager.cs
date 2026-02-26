using IDAProject.Web.Api.Models.Auth;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface ILdapManager
    {
        bool CheckLdapPasswordAsync(AppIdentityUser? user, string? password);


    }
}

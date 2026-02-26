using IDAProject.Web.Api.Models.Auth;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface ISecurityManager
    {
        Task<ResponseModelBase> ValidateApiKeyAsync(string apiKey);

        Task<string> GenerateTokenAsync(AppIdentityUser userAccount, IList<string> roles, IList<string> features);

        bool CheckLdapPasswordAsync(AppIdentityUser? user, string? password);


    }
}

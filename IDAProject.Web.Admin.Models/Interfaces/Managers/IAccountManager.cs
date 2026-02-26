using IDAProject.Web.Admin.Models.Accounts;
using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IAccountManager
    {
        /// <summary>
        /// Validates whether a given token is valid or not, and returns true in case the token is valid otherwise it will return false;
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool IsTokenValid(string token);

        Task<ResponseModel<string>> GenerateTokenAsync(LoginModel model);

        UserAccount GetUserFromJwt(string token);

        Task<ResponseModel<bool>> GetLdapSetting();

        Task<ResponseModelBase> ResetPasswordAsync(RegisterModel model);

        Task<ResponseModelBase> RegisterAccountAsync(RegisterModel model);

        Task<ResponseModelBase> ChangePasswordAsync(ChangePasswordModel model);

        Task<ResponseModelBase> AdminResetPasswordAsync(int userId, int adminUserId);
    }
}

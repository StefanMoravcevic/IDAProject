using Microsoft.AspNetCore.Identity;
using IDAProject.Web.Api.Models.Auth;
using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Users;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Users;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IUsersManager
    {
        Task<ResponseModelList<UserTableItemDto>> SearchUsersAsync(SearchUsersParams searchParams);

        Task<ResponseModel<UserDto>> GetUserByIdAsync(int id);
        Task<ResponseModelList<UserRoleDto>> GetRolesByUserIdAsync(int id);
        Task<ResponseModelBase> DeleteUserRoleAsync(int userRoleId);
        Task<ResponseModelBase> UpdateUserAsync(SaveUserRequestModel requestModel);
        Task<ResponseModelBase> CreateUserRoleAsync(CreateUserRoleModel requestModel);
        Task<ResponseModelList<UserDto>> GetUsersByRoleIdAsync(int roleId);
        Task<ResponseModelBase> RegisterAccountAsync(RegisterModel requestModel);
        Task<ResponseModelBase> ResetPasswordAsync(RegisterModel requestModel);
        Task<ResponseModelBase> UpdateUsersPasswordAsync(int userId, string newPasswordHash);
        Task<IdentityResult> ValidatePasswordAsync(UserManager<AppIdentityUser> userManager, AppIdentityUser user, string password);
        string GenerateRandomPassword(PasswordOptions? opts = null);

        Task<ResponseModel<int>> SaveUserLogAsync(SaveUserLogRequestModel requestModel);
        Task<ResponseModelList<UserLogDto>> SearchUserLogsAsync(SearchUserLogsParams searchParams);
    }
}

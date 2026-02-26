using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Users;
using IDAProject.Web.Models.RequestModels.Users;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task<List<UserTableItemDto>> SearchUsersAsync(SearchUsersParams searchParams);

        Task<UserDto?> GetUserByIdAsync(int id);

        Task<List<UserRoleDto>> GetRolesByUserIdAsync(int id);

        Task UpdateUserAsync(SaveUserRequestModel requestModel);

        Task<int> CreateUserRoleAsync(CreateUserRoleModel requestModel);

        Task<bool> SearchUserRoleAsync(CreateUserRoleModel requestModel);

        Task<int> DeleteUserRoleAsync(int userRoleId);

        Task<int> GetUserCompanyIdAsync(int idUser);

        Task<string> GetUserInitialsAsync(int idUser);

        Task<string> GetUserFullNameAsync(int idUser);
        Task<List<UserDto>> GetUsersByRoleIdAsync(int roleId);


        Task UpdateUsersPasswordAsync(int userId, string newPasswordHash);
        Task<int> SaveUserLogAsync(SaveUserLogRequestModel requestModel);
        Task<List<UserLogDto>> SearchUserLogsAsync(SearchUserLogsParams searchParams);
    }
}

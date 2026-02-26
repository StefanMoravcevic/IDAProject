using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Users;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.Users;
using IDAProject.Web.Models.Dto.AspNetUserOrgUnits;
using IDAProject.Web.Models.RequestModels.AspNetUserOrgUnits;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IUsersManager
    {
        Task<ResponseModelList<UserTableItemDto>> SearchUsersAsync(SearchUsersParams searchParams);
        Task<ResponseModelBase> CreateUserAsync(CreateUserModel requestModel);
        Task<ResponseModelBase> CreateUserRoleAsync(CreateUserRoleModel requestModel);
        Task<ResponseModelBase> DeleteUserRoleAsync(DeleteUserRoleModel requestModel);
        Task<ResponseModel<UserDto>> GetUserByIdAsync(int id);
        Task<ResponseModelList<UserRoleDto>> GetRolesByUserIdAsync(int id);
        Task<ResponseModelBase> SaveUserAsync(SaveUserRequestModel requestModel);
        Task<IEnumerable<ISelectOption>> GetUsersAsSelectOptionsAsync();
        Task<ResponseModelList<AspNetUserOrgUnitDto>> GetOrgUnitsByUserIdAsync(int id);

        Task<ResponseModel<int>> SaveUserLogAsync(SaveUserLogRequestModel requestModel);
        Task<ResponseModelList<UserLogDto>> SearchUserLogsAsync(SearchUserLogsParams searchParams);
        Task<ResponseModelList<AspNetUserOrgUnitDto>> SearchAspNetUserOrgUnitsAsync(SearchAspNetUserOrgUnitsParams searchParams);
        Task<ResponseModel<AspNetUserOrgUnitDto>> GetAspNetUserOrgUnitByIdAsync(int id);
        Task<ResponseModelBase> DeleteAspNetUserOrgUnitAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveAspNetUserOrgUnitAsync(SaveAspNetUserOrgUnitRequestModel requestModel);
    }
}

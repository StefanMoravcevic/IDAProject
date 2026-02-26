using IDAProject.Web.Admin.Managers;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Users;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Models.Dto.AspNetUserOrgUnits;
using IDAProject.Web.Models.RequestModels.AspNetUserOrgUnits;

namespace IDAProject.Web.Admin.Users
{
    public class UsersManager : BaseManager, IUsersManager
    {

        public UsersManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<UsersManager> logger)
            : base(httpClientFactory, configuration, logger)
        {
        }

        public async Task<ResponseModel<UserDto>> GetUserByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<UserDto>>($"api/accounts/{id}");
            return result;
        }
        public async Task<ResponseModelList<UserRoleDto>> GetRolesByUserIdAsync(int id)
        {
            var result = await GetAsync<ResponseModelList<UserRoleDto>>($"api/accounts/roles/{id}");
            return result;
        }

        public async Task<ResponseModelList<UserTableItemDto>> SearchUsersAsync(SearchUsersParams searchParams)
        {
            var result = await PostAsync<SearchUsersParams, ResponseModelList<UserTableItemDto>>($"api/accounts/search", searchParams);
            return result;
        }
        public async Task<ResponseModelBase> SaveUserAsync(SaveUserRequestModel requestModel)
        {
            var result = await PostAsync<SaveUserRequestModel, ResponseModelBase>($"api/accounts/save", requestModel);
            return result;
        }
        public async Task<ResponseModelBase> CreateUserAsync(CreateUserModel requestModel)
        {
            var result = await PostAsync<CreateUserModel, ResponseModelBase>($"api/accounts/createUser", requestModel);
            return result;
        }
        public async Task<ResponseModelBase> CreateUserRoleAsync(CreateUserRoleModel requestModel)
        {
            var result = await PostAsync<CreateUserRoleModel, ResponseModelBase>($"api/accounts/saveUserRole", requestModel);
            return result;
        }
        public async Task<ResponseModelBase> DeleteUserRoleAsync(DeleteUserRoleModel requestModel)
        {
            var result = await PostAsync<DeleteUserRoleModel, ResponseModelBase>($"api/accounts/deleteUserRole", requestModel);
            return result;
        }


        public async Task<IEnumerable<ISelectOption>> GetUsersAsSelectOptionsAsync()
        {
            var searchParams = new SearchUsersParams();
            var usersResponse = await SearchUsersAsync(searchParams);
            var usersList = usersResponse.Payload.OrderBy(x => x.UserName);
            var result = usersList.Select(x => new GenericSelectOption
            {
                Value = x.Id,
                Description = x.UserName
            });
            return result;
        }

        #region UserLog

        public async Task<ResponseModel<int>> SaveUserLogAsync(SaveUserLogRequestModel requestModel)
        {
            var result = await PostAsync<SaveUserLogRequestModel, ResponseModel<int>>($"api/accounts/saveUserLog", requestModel);
            return result;
        }

        public async Task<ResponseModelList<UserLogDto>> SearchUserLogsAsync(SearchUserLogsParams searchParams)
        {
            var result = await PostAsync<SearchUserLogsParams, ResponseModelList<UserLogDto>>($"api/accounts/searchUserLogs", searchParams);
            return result;
        }



        #endregion

        #region User org units 

        public async Task<ResponseModelList<AspNetUserOrgUnitDto>> GetOrgUnitsByUserIdAsync(int id)
        {
            var result = await GetAsync<ResponseModelList<AspNetUserOrgUnitDto>>($"api/AspNetUserOrgUnits/orgUnits/{id}");
            return result;
        }

        public async Task<ResponseModelList<AspNetUserOrgUnitDto>> SearchAspNetUserOrgUnitsAsync(SearchAspNetUserOrgUnitsParams searchParams)
        {
            var result = await PostAsync<SearchAspNetUserOrgUnitsParams, ResponseModelList<AspNetUserOrgUnitDto>>($"api/AspNetUserOrgUnits/search", searchParams);
            return result;
        }

        public async Task<ResponseModel<AspNetUserOrgUnitDto>> GetAspNetUserOrgUnitByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<AspNetUserOrgUnitDto>>($"api/AspNetUserOrgUnits/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteAspNetUserOrgUnitAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/AspNetUserOrgUnits/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveAspNetUserOrgUnitAsync(SaveAspNetUserOrgUnitRequestModel requestModel)
        {
            var result = await PostAsync<SaveAspNetUserOrgUnitRequestModel, ResponseModel<int>>($"api/AspNetUserOrgUnits", requestModel);
            return result;
        }

        #endregion
    }
}

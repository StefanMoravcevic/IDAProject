using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Roles;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Roles;

namespace IDAProject.Web.Admin.Managers
{

    public class RolesManager : BaseManager, IRolesManager
    {
        public RolesManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<RolesManager> logger, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, configuration, logger)
        {
        }

        public async Task<ResponseModel<RoleDto>> GetRoleByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<RoleDto>>($"api/roles/{id}");
            return result;
        }

        public async Task<ResponseModelList<RoleDto>> SearchRolesAsync(SearchRolesParams searchParams)
        {
            var result = await PostAsync<SearchRolesParams, ResponseModelList<RoleDto>>($"api/roles/search", searchParams);
            return result;
        }

        public async Task<ResponseModel<int>> SaveRoleAsync(SaveRoleRequestModel requestModel)
        {
            var result = await PostAsync<SaveRoleRequestModel, ResponseModel<int>>($"api/roles", requestModel);
            return result;
        }
        public async Task<ResponseModelBase> CreateRoleFeatureAsync(CreateRoleFeatureModel requestModel)
        {
            var result = await PostAsync<CreateRoleFeatureModel, ResponseModelBase>($"api/roles/saveRoleFeature", requestModel);
            return result;
        }
        public async Task<ResponseModelBase> DeleteRoleFeatureAsync(DeleteRoleFeatureModel requestModel)
        {
            var result = await PostAsync<DeleteRoleFeatureModel, ResponseModelBase>($"api/roles/deleteRoleFeature", requestModel);
            return result;
        }
        public async Task<ResponseModelList<RoleFeatureDto>> SearchRoleFeaturesAsync(int id)
        {
            var result = await GetAsync<ResponseModelList<RoleFeatureDto>>($"api/roles/searchRoleFeatures/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteRoleAsync(int id, int userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/roles/delete/{id}/{userId}");
            return result;
        }

    }
}

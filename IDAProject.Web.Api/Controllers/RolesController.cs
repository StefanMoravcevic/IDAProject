using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Roles;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Roles;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesManager _rolesManager;

        public RolesController(IRolesManager rolesManager)
        {
            _rolesManager = rolesManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<RoleDto>> GetRoleByIdAsync(int id)
        {
            var response = await _rolesManager.GetRoleByIdAsync(id);
            return response;
        }


        [HttpPost("search")]
        public async Task<ResponseModelList<RoleDto>> SearchRolesAsync(SearchRolesParams searchParams)
        {
            var response = await _rolesManager.SearchRolesAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveRoleAsync(SaveRoleRequestModel requestModel)
        {
            var response = await _rolesManager.SaveRoleAsync(requestModel);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteRoleAsync(int id, int? userId)
        {
            var response = await _rolesManager.DeleteRoleAsync(id, userId);
            return response;
        }
        [HttpPost("saveRoleFeature")]
        public async Task<ResponseModelBase> CreateRoleFeatureAsync(CreateRoleFeatureModel requestModel)
        {
            var response = await _rolesManager.CreateRoleFeatureAsync(requestModel);
            return response;
        }
        [HttpPost("deleteRoleFeature")]
        public async Task<ResponseModelBase> DeleteRoleFeatureAsync(DeleteRoleFeatureModel requestModel)
        {
            var response = await _rolesManager.DeleteRoleFeatureAsync(requestModel.RoleFeatureId);
            return response;
        }
        [HttpGet("searchRoleFeatures/{id}/")]
        public async Task<ResponseModelList<RoleFeatureDto>> GetRoleFeaturesByRoleIdAsync(int id)
        {
            var response = await _rolesManager.GetRoleFeaturesByRoleIdAsync(id);
            return response;
        }

    }
}
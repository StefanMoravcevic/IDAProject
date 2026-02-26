using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Roles;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Roles;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IRolesRepository
    {
        Task<List<RoleDto>> SearchRolesAsync(SearchRolesParams searchParams);

        Task<RoleDto?> GetRoleByIdAsync(int id);
        Task<List<RoleFeatureDto>> GetRoleFeaturesByRoleIdAsync(int id);
        Task<int> SaveRoleAsync(SaveRoleRequestModel requestModel);

        Task DeleteRoleAsync(int id, int? userId);
        Task<int> CreateRoleFeatureAsync(CreateRoleFeatureModel requestModel);
        Task<bool> SearchRoleFeatureAsync(CreateRoleFeatureModel requestModel);
        Task<int> DeleteRoleFeatureAsync(int roleFeatureId);

    }
}

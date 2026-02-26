using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Partners;
using IDAProject.Web.Models.Dto.Roles;
using IDAProject.Web.Models.Dto.Users;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Roles;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IRolesManager
    {
        Task<ResponseModelList<RoleDto>> SearchRolesAsync(SearchRolesParams searchParams);

        Task<ResponseModel<RoleDto>> GetRoleByIdAsync(int id);

        Task<ResponseModel<int>> SaveRoleAsync(SaveRoleRequestModel requestModel);

        Task<ResponseModelBase> DeleteRoleAsync(int id, int? userId);
        Task<ResponseModelBase> DeleteRoleFeatureAsync(int roleFeatureId);
        Task<ResponseModelBase> CreateRoleFeatureAsync(CreateRoleFeatureModel requestModel);
        Task<ResponseModelList<RoleFeatureDto>> GetRoleFeaturesByRoleIdAsync(int id);

    }
}

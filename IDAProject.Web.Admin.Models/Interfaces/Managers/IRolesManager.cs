using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Roles;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Roles;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IRolesManager
    {
        Task<ResponseModelList<RoleDto>> SearchRolesAsync(SearchRolesParams searchParams);
        Task<ResponseModelList<RoleFeatureDto>> SearchRoleFeaturesAsync(int id);
        Task<ResponseModel<RoleDto>> GetRoleByIdAsync(int id);
        Task<ResponseModel<int>> SaveRoleAsync(SaveRoleRequestModel requestModel);
        Task<ResponseModelBase> CreateRoleFeatureAsync(CreateRoleFeatureModel requestModel);
        Task<ResponseModelBase> DeleteRoleFeatureAsync(DeleteRoleFeatureModel requestModel);
        Task<ResponseModelBase> DeleteRoleAsync(int id, int userId);

    }
}

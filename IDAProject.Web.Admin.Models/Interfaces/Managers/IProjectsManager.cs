using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.Projects;
using IDAProject.Web.Models.RequestModels.Projects;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IProjectsManager
    {
        Task<ResponseModelList<ProjectDto>> SearchProjectsAsync(SearchProjectsParams searchParams);
        Task<ResponseModel<ProjectDto>> GetProjectByIdAsync(int id);
        Task<ResponseModelBase> DeleteProjectAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveProjectAsync(SaveProjectRequestModel requestModel);
    }
}


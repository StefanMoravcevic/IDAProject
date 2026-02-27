
using IDAProject.Web.Models.Dto.Projects;
using IDAProject.Web.Models.RequestModels.Projects;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IProjectsRepository
    {
        Task<ProjectDto> GetProjectByIdAsync(int id);
        Task<int> SaveProjectAsync(SaveProjectRequestModel requestModel);
        Task<List<ProjectDto>> SearchProjectsAsync(SearchProjectsParams searchParams);
        Task DeleteProjectAsync(int id, int? userId);
    }
}

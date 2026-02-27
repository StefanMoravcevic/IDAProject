using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Projects;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Projects;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsManager _ProjectsManager;

        public ProjectsController(IProjectsManager ProjectsManager)
        {
            _ProjectsManager = ProjectsManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<ProjectDto>> GetProjectByIdAsync(int id)
        {
            var response = await _ProjectsManager.GetProjectByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteProjectAsync(int id, int? userId)
        {
            var response = await _ProjectsManager.DeleteProjectAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<ProjectDto>> SearchProjectsAsync(SearchProjectsParams searchParams)
        {
            var response = await _ProjectsManager.SearchProjectsAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveProjectAsync(SaveProjectRequestModel requestModel)
        {
            var response = await _ProjectsManager.SaveProjectAsync(requestModel);
            return response;
        }
    }
}

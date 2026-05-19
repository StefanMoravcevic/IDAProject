using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.ProjectEmployees;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.ProjectEmployees;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectEmployeesController : ControllerBase
    {
        private readonly IProjectEmployeesManager _ProjectEmployeesManager;

        public ProjectEmployeesController(IProjectEmployeesManager ProjectEmployeesManager)
        {
            _ProjectEmployeesManager = ProjectEmployeesManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<ProjectEmployeeDto>> GetProjectEmployeeByIdAsync(int id)
        {
            var response = await _ProjectEmployeesManager.GetProjectEmployeeByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteProjectEmployeeAsync(int id, int? userId)
        {
            var response = await _ProjectEmployeesManager.DeleteProjectEmployeeAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<ProjectEmployeeDto>> SearchProjectEmployeesAsync(SearchProjectEmployeesParams searchParams)
        {
            var response = await _ProjectEmployeesManager.SearchProjectEmployeesAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveProjectEmployeeAsync(SaveProjectEmployeeRequestModel requestModel)
        {
            var response = await _ProjectEmployeesManager.SaveProjectEmployeeAsync(requestModel);
            return response;
        }
    }
}

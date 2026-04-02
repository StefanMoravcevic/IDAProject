using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.EmployeeJobTypeControls;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.EmployeeJobTypeControls;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeJobTypeControlsController : ControllerBase
    {
        private readonly IEmployeeJobTypeControlsManager _EmployeeJobTypeControlsManager;

        public EmployeeJobTypeControlsController(IEmployeeJobTypeControlsManager EmployeeJobTypeControlsManager)
        {
            _EmployeeJobTypeControlsManager = EmployeeJobTypeControlsManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<EmployeeJobTypeControlDto>> GetEmployeeJobTypeControlByIdAsync(int id)
        {
            var response = await _EmployeeJobTypeControlsManager.GetEmployeeJobTypeControlByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteEmployeeJobTypeControlAsync(int id, int? userId)
        {
            var response = await _EmployeeJobTypeControlsManager.DeleteEmployeeJobTypeControlAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<EmployeeJobTypeControlDto>> SearchEmployeeJobTypeControlsAsync(SearchEmployeeJobTypeControlsParams searchParams)
        {
            var response = await _EmployeeJobTypeControlsManager.SearchEmployeeJobTypeControlsAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveEmployeeJobTypeControlAsync(SaveEmployeeJobTypeControlRequestModel requestModel)
        {
            var response = await _EmployeeJobTypeControlsManager.SaveEmployeeJobTypeControlAsync(requestModel);
            return response;
        }
    }
}

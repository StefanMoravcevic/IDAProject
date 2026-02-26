using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Managers;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Contacts;
using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.Dto.Users;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Employees;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesManager _employeesManager;

        public EmployeesController(IEmployeesManager employeesManager)
        {
            _employeesManager = employeesManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<EmployeeDto>> GetEmployeesByIdAsync(int id)
        {
            var response = await _employeesManager.GetEmployeeByIdAsync(id);
            return response;
        }
        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteEmployeeAsync(int id, int? userId)
        {
            var response = await _employeesManager.DeleteEmployeeAsync(id, userId);
            return response;
        }
        [HttpPost("search")]
        public async Task<ResponseModelList<EmployeeDto>> SearchEmployeesAsync(SearchEmployeesParams searchParams)
        {
            var response = await _employeesManager.SearchEmployeesAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveEmployeeAsync(SaveEmployeeRequestModel requestModel)
        {
            var response = await _employeesManager.SaveEmployeeAsync(requestModel);
            return response;
        }

        [HttpGet("email_phonebyemployee/{id}")]
        public async Task<ResponseModel<EmployeeSearchResponseModel>> GetEmailPhoneByEmployeeIdAsync(int id)
        {
            var response = await _employeesManager.GetEmailPhoneByEmployeeIdAsync(id);
            return response;
        }

		[HttpGet("nextEmployee/{currentId}")]
		public async Task<ResponseModel<EmployeeDto>> GetNextRow(int currentId)
		{
			var response = await _employeesManager.GetNextRowAsync(currentId);
			return response;

		}
		[HttpGet("previousEmployee/{currentId}")]
		public async Task<ResponseModel<EmployeeDto>> GetPreviousRow(int currentId)
		{
			var response = await _employeesManager.GetPreviousRowAsync(currentId);
			return response;

		}

	}
}
using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.EmployeeGoals;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.EmployeeGoals;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeGoalsController : ControllerBase
    {
        private readonly IEmployeeGoalsManager _EmployeeGoalsManager;

        public EmployeeGoalsController(IEmployeeGoalsManager EmployeeGoalsManager)
        {
            _EmployeeGoalsManager = EmployeeGoalsManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<EmployeeGoalDto>> GetEmployeeGoalByIdAsync(int id)
        {
            var response = await _EmployeeGoalsManager.GetEmployeeGoalByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteEmployeeGoalAsync(int id, int? userId)
        {
            var response = await _EmployeeGoalsManager.DeleteEmployeeGoalAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<EmployeeGoalDto>> SearchEmployeeGoalsAsync(SearchEmployeeGoalsParams searchParams)
        {
            var response = await _EmployeeGoalsManager.SearchEmployeeGoalsAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveEmployeeGoalAsync(SaveEmployeeGoalRequestModel requestModel)
        {
            var response = await _EmployeeGoalsManager.SaveEmployeeGoalAsync(requestModel);
            return response;
        }
    }
}

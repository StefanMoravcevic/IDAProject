using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.EmployeeViewTrackings;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.EmployeeViewTrackings;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeViewTrackingsController : ControllerBase
    {
        private readonly IEmployeeViewTrackingsManager _EmployeeViewTrackingsManager;

        public EmployeeViewTrackingsController(IEmployeeViewTrackingsManager EmployeeViewTrackingsManager)
        {
            _EmployeeViewTrackingsManager = EmployeeViewTrackingsManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<EmployeeViewTrackingDto>> GetEmployeeViewTrackingByIdAsync(int id)
        {
            var response = await _EmployeeViewTrackingsManager.GetEmployeeViewTrackingByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteEmployeeViewTrackingAsync(int id, int? userId)
        {
            var response = await _EmployeeViewTrackingsManager.DeleteEmployeeViewTrackingAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<EmployeeViewTrackingDto>> SearchEmployeeViewTrackingsAsync(SearchEmployeeViewTrackingsParams searchParams)
        {
            var response = await _EmployeeViewTrackingsManager.SearchEmployeeViewTrackingsAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveEmployeeViewTrackingAsync(SaveEmployeeViewTrackingRequestModel requestModel)
        {
            var response = await _EmployeeViewTrackingsManager.SaveEmployeeViewTrackingAsync(requestModel);
            return response;
        }
    }
}

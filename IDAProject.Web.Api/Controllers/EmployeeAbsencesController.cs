using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.EmployeeAbsences;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.EmployeeAbsences;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAbsencesController : ControllerBase
    {
        private readonly IEmployeeAbsencesManager _EmployeeAbsencesManager;

        public EmployeeAbsencesController(IEmployeeAbsencesManager EmployeeAbsencesManager)
        {
            _EmployeeAbsencesManager = EmployeeAbsencesManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<EmployeeAbsenceDto>> GetEmployeeAbsenceByIdAsync(int id)
        {
            var response = await _EmployeeAbsencesManager.GetEmployeeAbsenceByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteEmployeeAbsenceAsync(int id, int? userId)
        {
            var response = await _EmployeeAbsencesManager.DeleteEmployeeAbsenceAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<EmployeeAbsenceDto>> SearchEmployeeAbsencesAsync(SearchEmployeeAbsencesParams searchParams)
        {
            var response = await _EmployeeAbsencesManager.SearchEmployeeAbsencesAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveEmployeeAbsenceAsync(SaveEmployeeAbsenceRequestModel requestModel)
        {
            if (TimeOnly.TryParse(requestModel.TimeFromFormatted, out var tf))
                requestModel.TimeFrom = tf;

            if (TimeOnly.TryParse(requestModel.TimeToFormatted, out var tt))
                requestModel.TimeTo = tt;
            var response = await _EmployeeAbsencesManager.SaveEmployeeAbsenceAsync(requestModel);
            return response;
        }
    }
}

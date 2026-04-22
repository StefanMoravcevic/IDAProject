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
        private readonly IEmployeesManager _employeesManager;

        public EmployeeAbsencesController(IEmployeeAbsencesManager EmployeeAbsencesManager, IEmployeesManager employeesManager)
        {
            _EmployeeAbsencesManager = EmployeeAbsencesManager;
            _employeesManager = employeesManager;
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

        [HttpPost("save")]
        public async Task<ResponseModel<int>> SaveEmployeeAbsenceAsync(SaveEmployeeAbsenceRequestModel requestModel)
        {
            if (TimeOnly.TryParse(requestModel.TimeFromFormatted, out var tf))
                requestModel.TimeFrom = tf;

            if (TimeOnly.TryParse(requestModel.TimeToFormatted, out var tt))
                requestModel.TimeTo = tt;

            if (requestModel.IsFromAdmin == true && requestModel.JobTypeId.HasValue)
            {
                var employees = await _employeesManager.SearchEmployeesAsync(new Web.Models.RequestModels.Employees.SearchEmployeesParams { JobTypeId = requestModel.JobTypeId });

                foreach (var employee in employees.Payload)
                {
                    var newRequest = new SaveEmployeeAbsenceRequestModel
                    {
                        EmployeeId = employee.Id,
                        AbsenceTypeId = requestModel.AbsenceTypeId,
                        DateFrom = requestModel.DateFrom,
                        DateTo = requestModel.DateTo,
                        TimeFrom = requestModel.TimeFrom,
                        TimeTo = requestModel.TimeTo,
                        AllDay = requestModel.AllDay,
                        JobTypeId = requestModel.JobTypeId,
                        Comment = requestModel.Comment
                    };

                    await _EmployeeAbsencesManager.SaveEmployeeAbsenceAsync(newRequest);
                }

                return new ResponseModel<int>
                {
                    Valid = true,
                    Message = "Absence saved for all employees in selected job type."
                };
            }

            // 🟢 NORMAL MODE
            var response = await _EmployeeAbsencesManager.SaveEmployeeAbsenceAsync(requestModel);
            return response;
        }
    }
}

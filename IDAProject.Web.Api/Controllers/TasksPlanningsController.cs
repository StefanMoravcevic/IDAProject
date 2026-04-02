using System.Globalization;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.TasksPlannings;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.TasksPlannings;
using Microsoft.AspNetCore.Mvc;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksPlanningsController : ControllerBase
    {
        private readonly ITasksPlanningsManager _TasksPlanningsManager;

        public TasksPlanningsController(ITasksPlanningsManager TasksPlanningsManager)
        {
            _TasksPlanningsManager = TasksPlanningsManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<TasksPlanningDto>> GetTasksPlanningByIdAsync(int id)
        {
            var response = await _TasksPlanningsManager.GetTasksPlanningByIdAsync(id);
            return response;
        }
        [HttpGet("stats/{employeeId}")]
        public async Task<ResponseModel<EmployeePlanningStatsDto>> GetEmployeePlanningStatsAsync(int employeeId)
        {
            var response = await _TasksPlanningsManager.GetLast30DaysStats(employeeId);
            return response;
        }
        [HttpGet("statsGeneric/{employeeId}/{from}/{to}")]
        public async Task<ResponseModel<EmployeePlanningStatsDto>> GetEmployeePlanningStatsGenericAsync(int employeeId, string? from, string? to)
        {
            DateTime fromDate = DateTime.ParseExact(from, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(to, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var response = await _TasksPlanningsManager.GetStatsGeneric(employeeId, fromDate, toDate);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteTasksPlanningAsync(int id, int? userId)
        {
            var response = await _TasksPlanningsManager.DeleteTasksPlanningAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<TasksPlanningDto>> SearchTasksPlanningsAsync(SearchTasksPlanningsParams searchParams)
        {
            var response = await _TasksPlanningsManager.SearchTasksPlanningsAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveTasksPlanningAsync(SaveTasksPlanningRequestModel requestModel)
        {
            if (TimeOnly.TryParse(requestModel.TimeFromFormatted, out var tf))
                requestModel.TimeFrom = tf;

            if (TimeOnly.TryParse(requestModel.TimeToFormatted, out var tt))
                requestModel.TimeTo = tt;

            if (TimeOnly.TryParse(requestModel.DurationFormatted, out var td))
                requestModel.Duration = td;

            var response = await _TasksPlanningsManager.SaveTasksPlanningAsync(requestModel);
            return response;
        }
    }
}

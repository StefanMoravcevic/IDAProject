using IDAProject.Web.Api.Managers;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.TasksRealizations;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.TasksRealizations;
using Microsoft.AspNetCore.Mvc;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksRealizationsController : ControllerBase
    {
        private readonly ITasksRealizationsManager _TasksRealizationsManager;
        private readonly ITasksPlanningsManager _tasksPlanningsManager;

        public TasksRealizationsController(ITasksRealizationsManager TasksRealizationsManager, ITasksPlanningsManager tasksPlanningsManager)
        {
            _TasksRealizationsManager = TasksRealizationsManager;
            _tasksPlanningsManager = tasksPlanningsManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<TasksRealizationDto>> GetTasksRealizationByIdAsync(int id)
        {
            var response = await _TasksRealizationsManager.GetTasksRealizationByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteTasksRealizationAsync(int id, int? userId)
        {
            var response = await _TasksRealizationsManager.DeleteTasksRealizationAsync(id, userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<TasksRealizationDto>> SearchTasksRealizationsAsync(SearchTasksRealizationsParams searchParams)
        {
            var response = await _TasksRealizationsManager.SearchTasksRealizationsAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveTasksRealizationAsync(SaveTasksRealizationRequestModel requestModel)
        {
            if (requestModel.Finished)
            {
                var taskPlanningId = await _tasksPlanningsManager.GetTasksPlanningByIdAsync(requestModel.TasksPlanningId.Value);
                taskPlanningId.Payload.PlanStatusId = 2;
                await _tasksPlanningsManager.SaveTasksPlanningAsync(taskPlanningId.Payload);


            }
            if (TimeOnly.TryParse(requestModel.TimeFromFormatted, out var tf))
                requestModel.TimeFrom = tf;

            if (TimeOnly.TryParse(requestModel.TimeToFormatted, out var tt))
                requestModel.TimeTo = tt;

            if (TimeOnly.TryParse(requestModel.DurationFormatted, out var td))
                requestModel.Duration = td;
            var response = await _TasksRealizationsManager.SaveTasksRealizationAsync(requestModel);
            return response;
        }
    }
}

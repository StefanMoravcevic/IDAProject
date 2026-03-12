using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.TasksRealizationComments;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.TasksRealizationComments;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksRealizationCommentsController : ControllerBase
    {
        private readonly ITasksRealizationCommentsManager _TasksRealizationCommentsManager;

        public TasksRealizationCommentsController(ITasksRealizationCommentsManager TasksRealizationCommentsManager)
        {
            _TasksRealizationCommentsManager = TasksRealizationCommentsManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<TasksRealizationCommentDto>> GetTasksRealizationCommentByIdAsync(int id)
        {
            var response = await _TasksRealizationCommentsManager.GetTasksRealizationCommentByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteTasksRealizationCommentAsync(int id, int? userId)
        {
            var response = await _TasksRealizationCommentsManager.DeleteTasksRealizationCommentAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<TasksRealizationCommentDto>> SearchTasksRealizationCommentsAsync(SearchTasksRealizationCommentsParams searchParams)
        {
            var response = await _TasksRealizationCommentsManager.SearchTasksRealizationCommentsAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveTasksRealizationCommentAsync(SaveTasksRealizationCommentRequestModel requestModel)
        {
            var response = await _TasksRealizationCommentsManager.SaveTasksRealizationCommentAsync(requestModel);
            return response;
        }
    }
}

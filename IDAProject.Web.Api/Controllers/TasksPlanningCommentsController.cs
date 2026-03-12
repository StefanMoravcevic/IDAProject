using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.TasksPlanningComments;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.TasksPlanningComments;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksPlanningCommentsController : ControllerBase
    {
        private readonly ITasksPlanningCommentsManager _TasksPlanningCommentsManager;

        public TasksPlanningCommentsController(ITasksPlanningCommentsManager TasksPlanningCommentsManager)
        {
            _TasksPlanningCommentsManager = TasksPlanningCommentsManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<TasksPlanningCommentDto>> GetTasksPlanningCommentByIdAsync(int id)
        {
            var response = await _TasksPlanningCommentsManager.GetTasksPlanningCommentByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteTasksPlanningCommentAsync(int id, int? userId)
        {
            var response = await _TasksPlanningCommentsManager.DeleteTasksPlanningCommentAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<TasksPlanningCommentDto>> SearchTasksPlanningCommentsAsync(SearchTasksPlanningCommentsParams searchParams)
        {
            var response = await _TasksPlanningCommentsManager.SearchTasksPlanningCommentsAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveTasksPlanningCommentAsync(SaveTasksPlanningCommentRequestModel requestModel)
        {
            var response = await _TasksPlanningCommentsManager.SaveTasksPlanningCommentAsync(requestModel);
            return response;
        }
    }
}

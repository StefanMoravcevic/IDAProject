using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.TasksPlanningComments;
using IDAProject.Web.Models.Dto.TasksPlanningComments;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.TasksPlanningComments;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class TasksPlanningCommentsController : BaseController
    {
        private readonly ITasksPlanningCommentsManager _TasksPlanningCommentsManager;
        private readonly IMasterDataManager _masterDataManager;

        public TasksPlanningCommentsController(
            ILogger<TasksPlanningCommentsController> logger,
            IAccountManager accountManager,
            ITasksPlanningCommentsManager TasksPlanningCommentsManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _TasksPlanningCommentsManager = TasksPlanningCommentsManager;
            _masterDataManager = masterDataManager;
        }
        [HttpGet("TasksPlanningCommentsList", Name = RouteNames.TasksPlanningComments_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new TasksPlanningCommentsViewModel();
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "TasksPlanningComments");
            //var responseModel = await _TasksPlanningCommentsManager.SearchTasksPlanningCommentsAsync();
            //viewModel = TasksPlanningComments.Payload;
            //return Json(responseModel.Payload);
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.TasksPlanningComments_Search)]
        public async Task<IActionResult> SearchTasksPlanningComments(SearchTasksPlanningCommentsParams searchParams)
        {
            var responseModel = await _TasksPlanningCommentsManager.SearchTasksPlanningCommentsAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new/{Id}", Name = RouteNames.TasksPlanningComments_New)]
        public async Task<IActionResult> NewTasksPlanningCommentAsync(int Id)
        {
            var viewModel = new TasksPlanningCommentViewModel();

            viewModel.User = GetCurrentUser();
            return View("EditTasksPlanningComment", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.TasksPlanningComments_Edit)]
        public async Task<IActionResult> EditTasksPlanningCommentAsync(int id)
        {
            var viewModel = new TasksPlanningCommentViewModel();

            var TasksPlanningCommentResponse = await _TasksPlanningCommentsManager.GetTasksPlanningCommentByIdAsync(id);

            viewModel.TasksPlanningComment = TasksPlanningCommentResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditTasksPlanningComment", viewModel);
        }

        //controller method for saving TasksPlanningComment
        [HttpPost("save", Name = RouteNames.TasksPlanningComments_Save)]
        public async Task<IActionResult> SaveTasksPlanningCommentAsync(SaveTasksPlanningCommentRequestModel requestModel)
        {
            var user = GetCurrentUser();
            requestModel.UserId = user.Id;
            requestModel.CreatedAt = DateTime.Now;
            var responseModel = await _TasksPlanningCommentsManager.SaveTasksPlanningCommentAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.TasksPlanningComments_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.TasksPlanningComments_Delete)]
        public async Task<IActionResult> DeleteTasksPlanningCommentAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _TasksPlanningCommentsManager.DeleteTasksPlanningCommentAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.TasksPlanningComments_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
    }
}

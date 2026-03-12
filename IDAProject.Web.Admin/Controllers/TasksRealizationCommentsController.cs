using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.TasksRealizationComments;
using IDAProject.Web.Models.Dto.TasksRealizationComments;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.TasksRealizationComments;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class TasksRealizationCommentsController : BaseController
    {
        private readonly ITasksRealizationCommentsManager _TasksRealizationCommentsManager;
        private readonly IMasterDataManager _masterDataManager;

        public TasksRealizationCommentsController(
            ILogger<TasksRealizationCommentsController> logger,
            IAccountManager accountManager,
            ITasksRealizationCommentsManager TasksRealizationCommentsManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _TasksRealizationCommentsManager = TasksRealizationCommentsManager;
            _masterDataManager = masterDataManager;
        }
        [HttpGet("TasksRealizationCommentsList", Name = RouteNames.TasksRealizationComments_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new TasksRealizationCommentsViewModel();
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "TasksRealizationComments");
            //var responseModel = await _TasksRealizationCommentsManager.SearchTasksRealizationCommentsAsync();
            //viewModel = TasksRealizationComments.Payload;
            //return Json(responseModel.Payload);
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.TasksRealizationComments_Search)]
        public async Task<IActionResult> SearchTasksRealizationComments(SearchTasksRealizationCommentsParams searchParams)
        {
            var responseModel = await _TasksRealizationCommentsManager.SearchTasksRealizationCommentsAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new/{Id}", Name = RouteNames.TasksRealizationComments_New)]
        public async Task<IActionResult> NewTasksRealizationCommentAsync(int Id)
        {
            var viewModel = new TasksRealizationCommentViewModel();

            viewModel.User = GetCurrentUser();
            return View("EditTasksRealizationComment", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.TasksRealizationComments_Edit)]
        public async Task<IActionResult> EditTasksRealizationCommentAsync(int id)
        {
            var viewModel = new TasksRealizationCommentViewModel();

            var TasksRealizationCommentResponse = await _TasksRealizationCommentsManager.GetTasksRealizationCommentByIdAsync(id);

            viewModel.TasksRealizationComment = TasksRealizationCommentResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditTasksRealizationComment", viewModel);
        }

        //controller method for saving TasksRealizationComment
        [HttpPost("save", Name = RouteNames.TasksRealizationComments_Save)]
        public async Task<IActionResult> SaveTasksRealizationCommentAsync(SaveTasksRealizationCommentRequestModel requestModel)
        {
            var responseModel = await _TasksRealizationCommentsManager.SaveTasksRealizationCommentAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.TasksRealizationComments_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.TasksRealizationComments_Delete)]
        public async Task<IActionResult> DeleteTasksRealizationCommentAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _TasksRealizationCommentsManager.DeleteTasksRealizationCommentAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.TasksRealizationComments_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
    }
}

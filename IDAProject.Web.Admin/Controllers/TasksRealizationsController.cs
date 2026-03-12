using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.TasksRealizations;
using IDAProject.Web.Models.Dto.TasksRealizations;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.TasksRealizations;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class TasksRealizationsController : BaseController
    {
        private readonly ITasksRealizationsManager _TasksRealizationsManager;
        private readonly IIdaTasksManager _idaTasksManager;
        private readonly ITasksPlanningsManager _tasksPlanningsManager;
        private readonly IMasterDataManager _masterDataManager;

        public TasksRealizationsController(
            ILogger<TasksRealizationsController> logger,
            IAccountManager accountManager,
            IIdaTasksManager idaTasksManager,
            ITasksPlanningsManager tasksPlanningsManager,
            ITasksRealizationsManager TasksRealizationsManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _TasksRealizationsManager = TasksRealizationsManager;
            _tasksPlanningsManager = tasksPlanningsManager;
            _masterDataManager = masterDataManager;
            _idaTasksManager = idaTasksManager;
        }
        [HttpGet("TasksRealizationsList", Name = RouteNames.TasksRealizations_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new TasksRealizationsViewModel();
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "TasksRealizations");
            //var responseModel = await _TasksRealizationsManager.SearchTasksRealizationsAsync();
            //viewModel = TasksRealizations.Payload;
            //return Json(responseModel.Payload);
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.TasksRealizations_Search)]
        public async Task<IActionResult> SearchTasksRealizations(SearchTasksRealizationsParams searchParams)
        {
            var responseModel = await _TasksRealizationsManager.SearchTasksRealizationsAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new/{Id}", Name = RouteNames.TasksRealizations_New)]
        public async Task<IActionResult> NewTasksRealizationAsync(int Id)
        {
            var viewModel = new TasksRealizationViewModel();

            viewModel.User = GetCurrentUser();
            return View("EditTasksRealization", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.TasksRealizations_Edit)]
        public async Task<IActionResult> EditTasksRealizationAsync(int id)
        {
            var viewModel = new TasksRealizationViewModel();

            var TasksRealizationResponse = await _TasksRealizationsManager.GetTasksRealizationByIdAsync(id);

            viewModel.TasksRealization = TasksRealizationResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditTasksRealization", viewModel);
        }

        //controller method for saving TasksRealization
        [HttpPost("save", Name = RouteNames.TasksRealizations_Save)]
        public async Task<IActionResult> SaveTasksRealizationAsync(SaveTasksRealizationRequestModel requestModel)
        {
            var user = GetCurrentUser();
            requestModel.UserId = user.Id;
            requestModel.CreatedDate = DateTime.Now;
            var responseModel = await _TasksRealizationsManager.SaveTasksRealizationAsync(requestModel);
            if (responseModel.Valid)
            {
                if (requestModel.Finished)
                {
                    if(requestModel.ActivityTypeId == (int)ActivityTypes.Projekat || requestModel.ActivityTypeId == (int)ActivityTypes.Zadatak) 
                    {
                        var taskId = await _idaTasksManager.GetIdaTaskByIdAsync(requestModel.IdaTaskId.Value);
                        taskId.Payload.IsCompleted = true;
                        await _idaTasksManager.SaveIdaTaskAsync(taskId.Payload);
                    }
                }
                responseModel.Message = Url.RouteUrl(RouteNames.TasksRealizations_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.TasksRealizations_Delete)]
        public async Task<IActionResult> DeleteTasksRealizationAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _TasksRealizationsManager.DeleteTasksRealizationAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.TasksRealizations_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
    }
}

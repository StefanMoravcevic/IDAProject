using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.TasksPlannings;
using IDAProject.Web.Models.Dto.TasksPlannings;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.TasksPlannings;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class TasksPlanningsController : BaseController
    {
        private readonly ITasksPlanningsManager _TasksPlanningsManager;
        private readonly IMasterDataManager _masterDataManager;

        public TasksPlanningsController(
            ILogger<TasksPlanningsController> logger,
            IAccountManager accountManager,
            ITasksPlanningsManager TasksPlanningsManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _TasksPlanningsManager = TasksPlanningsManager;
            _masterDataManager = masterDataManager;
        }
        [HttpGet("TasksPlanningsList", Name = RouteNames.TasksPlannings_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new TasksPlanningsViewModel();
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "TasksPlannings");
            return View(viewModel);
        }
        [HttpGet("getById/{id}", Name = RouteNames.TasksPlannings_GetById)]
        public async Task<IActionResult> GetById(int id)
        {
            var responseModel = await _TasksPlanningsManager.GetTasksPlanningByIdAsync(id);
            return Json(responseModel.Payload);
        }

        [HttpPost("search", Name = RouteNames.TasksPlannings_Search)]
        public async Task<IActionResult> SearchTasksPlannings(SearchTasksPlanningsParams searchParams)
        {
            var responseModel = await _TasksPlanningsManager.SearchTasksPlanningsAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new", Name = RouteNames.TasksPlannings_New)]
        public async Task<IActionResult> NewTasksPlanningAsync()
        {
            var viewModel = new TasksPlanningViewModel();

            viewModel.User = GetCurrentUser();
            return View("EditTasksPlanning", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.TasksPlannings_Edit)]
        public async Task<IActionResult> EditTasksPlanningAsync(int id)
        {
            var viewModel = new TasksPlanningViewModel();

            var TasksPlanningResponse = await _TasksPlanningsManager.GetTasksPlanningByIdAsync(id);

            viewModel.TasksPlanning = TasksPlanningResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditTasksPlanning", viewModel);
        }

        [HttpPost("save", Name = RouteNames.TasksPlannings_Save)]
        public async Task<IActionResult> SaveTasksPlanningAsync(SaveTasksPlanningRequestModel requestModel)
        {
            var user = GetCurrentUser();
            if (!string.IsNullOrEmpty(requestModel.PlanDateForSave))
            {
                string[] formats = { "dd.MM.yyyy.", "d.M.yyyy" };
                if (DateTime.TryParseExact(requestModel.PlanDateForSave, formats,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime planDate))
                {
                    requestModel.PlanDate = planDate;
                }
                else
                {
                    throw new ArgumentException($"Neispravan format datuma: {requestModel.PlanDateForSave}");
                }
            }
            requestModel.UserId = user.Id;
            if (requestModel.Id == 0)
            {
                requestModel.CreatedAt = DateTime.Now;
            }
            else
            {
                var taskplanning = await _TasksPlanningsManager.GetTasksPlanningByIdAsync(requestModel.Id);
                requestModel.CreatedAt = taskplanning.Payload.CreatedAt;
            }
                requestModel.EmployeeId = user.EmployeeId;
            var responseModel = await _TasksPlanningsManager.SaveTasksPlanningAsync(requestModel);
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.TasksPlannings_Delete)]
        public async Task<IActionResult> DeleteTasksPlanningAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _TasksPlanningsManager.DeleteTasksPlanningAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.TasksPlannings_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
    }
}

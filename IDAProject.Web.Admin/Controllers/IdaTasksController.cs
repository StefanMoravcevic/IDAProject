using IDAProject.Web.Admin.Managers;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.EmployeeAbsences;
using IDAProject.Web.Admin.Models.ViewModels.IdaTasks;
using IDAProject.Web.Models.Dto.IdaTasks;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.EmployeeAbsences;
using IDAProject.Web.Models.RequestModels.IdaTasks;
using Microsoft.AspNetCore.Mvc;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class IdaTasksController : BaseController
    {
        private readonly IIdaTasksManager _IdaTasksManager;
        private readonly IMasterDataManager _masterDataManager;

        public IdaTasksController(
            ILogger<IdaTasksController> logger,
            IAccountManager accountManager,
            IIdaTasksManager IdaTasksManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _IdaTasksManager = IdaTasksManager;
            _masterDataManager = masterDataManager;
        }
        [HttpGet("IdaTasksList", Name = RouteNames.IdaTasks_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new IdaTasksViewModel();
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "IdaTasks");
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.IdaTasks_Search)]
        public async Task<IActionResult> SearchIdaTasks(SearchIdaTasksParams searchParams)
        {
            var responseModel = await _IdaTasksManager.SearchIdaTasksAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new", Name = RouteNames.IdaTasks_New)]
        public async Task<IActionResult> NewIdaTaskAsync()
        {
            var viewModel = new IdaTaskViewModel();

            viewModel.User = GetCurrentUser();
            return View("EditIdaTask", viewModel);
        }

        //[HttpGet("edit/{id}", Name = RouteNames.IdaTasks_Edit)]
        //public async Task<IActionResult> EditIdaTaskAsync(int id)
        //{
        //    var viewModel = new IdaTaskViewModel();

        //    var IdaTaskResponse = await _IdaTasksManager.GetIdaTaskByIdAsync(id);

        //    viewModel.IdaTask = IdaTaskResponse.Payload!;
        //    viewModel.User = GetCurrentUser();

        //    return View("EditIdaTask", viewModel);
        //}

        [HttpPost("save", Name = RouteNames.IdaTasks_Save)]
        public async Task<IActionResult> SaveIdaTaskAsync(SaveIdaTaskRequestModel requestModel)
        {
            var responseModel = await _IdaTasksManager.SaveIdaTaskAsync(requestModel);
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.IdaTasks_Delete)]
        public async Task<IActionResult> DeleteIdaTaskAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _IdaTasksManager.DeleteIdaTaskAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.IdaTasks_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpGet("idaTasks/{userId}", Name = RouteNames.IdaTasks_GetByEmployeeId)]
        public async Task<IActionResult> IdaTasksByEmployeeId(int userId)
        {
            var idaTasks = await _IdaTasksManager.SearchIdaTasksAsync(new SearchIdaTasksParams { UserId = userId, HasProject = false });

            var viewModel = new IdaTaskViewModel
            {
                UserId = userId,
                IdaTask = idaTasks.Payload!,
            };
            return PartialView("EditIdaTasksModel", viewModel);
        }

        [HttpGet("idaTasks/records/{userId}", Name = RouteNames.IdaTasks_RecordsByEmployeeId)]
        public async Task<IActionResult> IdaTasksRecordsByEmployeeId(int userId)
        {
            var responseModel = await _IdaTasksManager.SearchIdaTasksAsync(new SearchIdaTasksParams { UserId = userId, HasProject = false });
            return PartialView("EditIdaTasksRecords", responseModel.Payload);
        }
        [HttpGet("idaProjectTasks/{userId}", Name = RouteNames.IdaProjectTasks_GetByEmployeeId)]
        public async Task<IActionResult> IdaProjectTasksByEmployeeId(int userId)
        {
            var idaTasks = await _IdaTasksManager.SearchIdaTasksAsync(new SearchIdaTasksParams { UserId = userId, HasProject = true });

            var viewModel = new IdaTaskViewModel
            {
                UserId = userId,
                IdaTask = idaTasks.Payload!,
            };
            viewModel.Projects = await _masterDataManager.GetSelectOptionsByTableAsync("Projects", "Description");
            return PartialView("EditIdaProjectTasksModel", viewModel);
        }

        [HttpGet("idaProjectTasks/records/{userId}", Name = RouteNames.IdaProjectTasks_RecordsByEmployeeId)]
        public async Task<IActionResult> IdaProjectTasksRecordsByEmployeeId(int userId)
        {
            var responseModel = await _IdaTasksManager.SearchIdaTasksAsync(new SearchIdaTasksParams { UserId = userId, HasProject = true });
            return PartialView("EditIdaProjectTasksRecords", responseModel.Payload);
        }
    }
}

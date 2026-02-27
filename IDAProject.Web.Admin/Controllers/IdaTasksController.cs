using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.IdaTasks;
using IDAProject.Web.Models.Dto.IdaTasks;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.IdaTasks;

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

        [HttpGet("edit/{id}", Name = RouteNames.IdaTasks_Edit)]
        public async Task<IActionResult> EditIdaTaskAsync(int id)
        {
            var viewModel = new IdaTaskViewModel();

            var IdaTaskResponse = await _IdaTasksManager.GetIdaTaskByIdAsync(id);

            viewModel.IdaTask = IdaTaskResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditIdaTask", viewModel);
        }

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
    }
}

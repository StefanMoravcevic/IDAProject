using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.EmployeeJobTypeControls;
using IDAProject.Web.Models.Dto.EmployeeJobTypeControls;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.EmployeeJobTypeControls;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class EmployeeJobTypeControlsController : BaseController
    {
        private readonly IEmployeeJobTypeControlsManager _EmployeeJobTypeControlsManager;
        private readonly IEmployeesManager _employeesManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EmployeeJobTypeControlsController(
            ILogger<EmployeeJobTypeControlsController> logger,
            IAccountManager accountManager,
            IEmployeesManager  employeesManager,
            IStringLocalizer<SharedResources> localizer,
            IEmployeeJobTypeControlsManager EmployeeJobTypeControlsManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _EmployeeJobTypeControlsManager = EmployeeJobTypeControlsManager;
            _masterDataManager = masterDataManager;
            _localizer = localizer;
            _employeesManager = employeesManager;
        }
        [HttpGet("EmployeeJobTypeControlsList", Name = RouteNames.EmployeeJobTypeControls_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new EmployeeJobTypeControlsViewModel(_localizer);
            viewModel.JobTypes = await _masterDataManager.GetSelectOptionsByTableAsync("JobTypes", "Name");
            viewModel.Employees = await _employeesManager.GetEmployeesAsSelectOptionsAsync();
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "EmployeeJobTypeControls");
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.EmployeeJobTypeControls_Search)]
        public async Task<IActionResult> SearchEmployeeJobTypeControls(SearchEmployeeJobTypeControlsParams searchParams)
        {
            var responseModel = await _EmployeeJobTypeControlsManager.SearchEmployeeJobTypeControlsAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new", Name = RouteNames.EmployeeJobTypeControls_New)]
        public async Task<IActionResult> NewEmployeeJobTypeControlAsync()
        {
            var viewModel = new EmployeeJobTypeControlViewModel();

            viewModel.User = GetCurrentUser();
            viewModel.Employees = await _employeesManager.GetEmployeesAsSelectOptionsAsync();
            viewModel.JobTypes = await _masterDataManager.GetSelectOptionsByTableAsync("JobTypes", "Name");
            return View("EditEmployeeJobTypeControl", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.EmployeeJobTypeControls_Edit)]
        public async Task<IActionResult> EditEmployeeJobTypeControlAsync(int id)
        {
            var viewModel = new EmployeeJobTypeControlViewModel();

            var EmployeeJobTypeControlResponse = await _EmployeeJobTypeControlsManager.GetEmployeeJobTypeControlByIdAsync(id);
            viewModel.Employees = await _employeesManager.GetEmployeesAsSelectOptionsAsync();
            viewModel.JobTypes = await _masterDataManager.GetSelectOptionsByTableAsync("JobTypes", "Name");
            viewModel.EmployeeJobTypeControl = EmployeeJobTypeControlResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditEmployeeJobTypeControl", viewModel);
        }
        [HttpGet("view/{id}", Name = RouteNames.EmployeeJobTypeControls_View)]
        public async Task<IActionResult> ViewEmployeeJobTypeControlAsync(int id)
        {
            var viewModel = new EmployeeJobTypeControlViewModel();

            var EmployeeJobTypeControlResponse = await _EmployeeJobTypeControlsManager.GetEmployeeJobTypeControlByIdAsync(id);
            viewModel.Employees = await _employeesManager.GetEmployeesAsSelectOptionsAsync();
            viewModel.JobTypes = await _masterDataManager.GetSelectOptionsByTableAsync("JobTypes", "Name");
            viewModel.EmployeeJobTypeControl = EmployeeJobTypeControlResponse.Payload!;
            viewModel.ReadOnly = 1;
            viewModel.User = GetCurrentUser();

            return View("EditEmployeeJobTypeControl", viewModel);
        }

        [HttpPost("save", Name = RouteNames.EmployeeJobTypeControls_Save)]
        public async Task<IActionResult> SaveEmployeeJobTypeControlAsync(SaveEmployeeJobTypeControlRequestModel requestModel)
        {
            var responseModel = await _EmployeeJobTypeControlsManager.SaveEmployeeJobTypeControlAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.EmployeeJobTypeControls_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.EmployeeJobTypeControls_Delete)]
        public async Task<IActionResult> DeleteEmployeeJobTypeControlAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _EmployeeJobTypeControlsManager.DeleteEmployeeJobTypeControlAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.EmployeeJobTypeControls_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
    }
}

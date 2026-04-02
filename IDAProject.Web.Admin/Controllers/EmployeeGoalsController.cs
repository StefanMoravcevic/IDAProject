using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.EmployeeGoals;
using IDAProject.Web.Models.Dto.EmployeeGoals;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.EmployeeGoals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class EmployeeGoalsController : BaseController
    {
        private readonly IEmployeeGoalsManager _EmployeeGoalsManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IEmployeesManager _employeesManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EmployeeGoalsController(
            ILogger<EmployeeGoalsController> logger,
            IAccountManager accountManager,
            IEmployeesManager employeesManager,
            IStringLocalizer<SharedResources> localizer,
            IEmployeeGoalsManager EmployeeGoalsManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _EmployeeGoalsManager = EmployeeGoalsManager;
            _masterDataManager = masterDataManager;
            _employeesManager = employeesManager;
            _localizer = localizer;
        }
        [HttpGet("EmployeeGoalsList", Name = RouteNames.EmployeeGoals_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new EmployeeGoalsViewModel(_localizer);
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "EmployeeGoals");
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.EmployeeGoals_Search)]
        public async Task<IActionResult> SearchEmployeeGoals(SearchEmployeeGoalsParams searchParams)
        {
            var responseModel = await _EmployeeGoalsManager.SearchEmployeeGoalsAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new", Name = RouteNames.EmployeeGoals_New)]
        public async Task<IActionResult> NewEmployeeGoalAsync()
        {
            var viewModel = new EmployeeGoalViewModel();

            viewModel.User = GetCurrentUser();
            viewModel.Years = await _masterDataManager.GetSelectOptionsByTableAsync("Years", "Year1");
            viewModel.Employees = await _employeesManager.GetDriversAsSelectOptionsAsync();
            return View("EditEmployeeGoal", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.EmployeeGoals_Edit)]
        public async Task<IActionResult> EditEmployeeGoalAsync(int id)
        {
            var viewModel = new EmployeeGoalViewModel();

            var EmployeeGoalResponse = await _EmployeeGoalsManager.GetEmployeeGoalByIdAsync(id);

            viewModel.EmployeeGoal = EmployeeGoalResponse.Payload!;
            viewModel.Years = await _masterDataManager.GetSelectOptionsByTableAsync("Years", "Year1");
            viewModel.Employees = await _employeesManager.GetDriversAsSelectOptionsAsync();
            viewModel.User = GetCurrentUser();

            return View("EditEmployeeGoal", viewModel);
        }

        [HttpPost("save", Name = RouteNames.EmployeeGoals_Save)]
        public async Task<IActionResult> SaveEmployeeGoalAsync(SaveEmployeeGoalRequestModel requestModel)
        {
            var responseModel = await _EmployeeGoalsManager.SaveEmployeeGoalAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.EmployeeGoals_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.EmployeeGoals_Delete)]
        public async Task<IActionResult> DeleteEmployeeGoalAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _EmployeeGoalsManager.DeleteEmployeeGoalAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.EmployeeGoals_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
    }
}

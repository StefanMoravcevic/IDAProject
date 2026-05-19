using IDAProject.Web.Admin.Managers;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.EmployeeAbsences;
using IDAProject.Web.Admin.Models.ViewModels.ProjectEmployees;
using IDAProject.Web.Models.Dto.ProjectEmployees;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.EmployeeAbsences;
using IDAProject.Web.Models.RequestModels.ProjectEmployees;
using Microsoft.AspNetCore.Mvc;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class ProjectEmployeesController : BaseController
    {
        private readonly IProjectEmployeesManager _ProjectEmployeesManager;
        private readonly IEmployeesManager _employeesManager;
        private readonly IMasterDataManager _masterDataManager;

        public ProjectEmployeesController(
            ILogger<ProjectEmployeesController> logger,
            IAccountManager accountManager,
            IEmployeesManager employeesManager,
            IProjectEmployeesManager ProjectEmployeesManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _ProjectEmployeesManager = ProjectEmployeesManager;
            _masterDataManager = masterDataManager;
            _employeesManager = employeesManager;
        }
        [HttpGet("ProjectEmployeesList", Name = RouteNames.ProjectEmployees_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ProjectEmployeesViewModel();
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "ProjectEmployees");
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.ProjectEmployees_Search)]
        public async Task<IActionResult> SearchProjectEmployees(SearchProjectEmployeesParams searchParams)
        {
            var responseModel = await _ProjectEmployeesManager.SearchProjectEmployeesAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new/{Id}", Name = RouteNames.ProjectEmployees_New)]
        public async Task<IActionResult> NewProjectEmployeeAsync(int Id)
        {
            var viewModel = new ProjectEmployeeViewModel();

            viewModel.User = GetCurrentUser();
            return View("EditProjectEmployee", viewModel);
        }

        [HttpPost("save", Name = RouteNames.ProjectEmployees_Save)]
        public async Task<IActionResult> SaveProjectEmployeeAsync(SaveProjectEmployeeRequestModel requestModel)
        {
            var responseModel = await _ProjectEmployeesManager.SaveProjectEmployeeAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.ProjectEmployees_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpDelete("delete/{id}", Name = RouteNames.ProjectEmployees_Delete)]
        public async Task<IActionResult> DeleteProjectEmployeeAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _ProjectEmployeesManager.DeleteProjectEmployeeAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.ProjectEmployees_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpGet("projectEmployees/{projectId}", Name = RouteNames.ProjectEmployees_ListByProjectId)]
        public async Task<IActionResult> ProjectEmployeesByProjectId(int projectId)
        {
            var projectEmployees = await _ProjectEmployeesManager.SearchProjectEmployeesAsync(new SearchProjectEmployeesParams { ProjectId = projectId });
            var viewModel = new ProjectEmployeeViewModel
            {
                ProjectEmployees = projectEmployees.Payload,
                ProjectId = projectId
            };
            viewModel.Projects = await _masterDataManager.GetSelectOptionsByTableAsync("Projects", "Description");
            viewModel.Employees = await _employeesManager.GetEmployeesAsSelectOptionsAsync();
            return PartialView("EditProjectEmployeeModal", viewModel);
        }
        [HttpGet("projectEmployees/records/{projectId}", Name = RouteNames.ProjectEmployees_RecordsByProjectId)]
        public async Task<IActionResult> GetProjectEmployeesAsync(int projectId)
        {
            var responseModel = await _ProjectEmployeesManager.SearchProjectEmployeesAsync(new SearchProjectEmployeesParams { ProjectId = projectId });
            return PartialView("EditProjectEmployeeRecords", responseModel.Payload);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.Projects;
using IDAProject.Web.Models.Dto.Projects;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.Projects;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class ProjectsController : BaseController
    {
        private readonly IProjectsManager _ProjectsManager;
        private readonly IMasterDataManager _masterDataManager;

        public ProjectsController(
            ILogger<ProjectsController> logger,
            IAccountManager accountManager,
            IProjectsManager ProjectsManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _ProjectsManager = ProjectsManager;
            _masterDataManager = masterDataManager;
        }
        [HttpGet("ProjectsList", Name = RouteNames.Projects_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ProjectsViewModel();
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "Projects");
            //var responseModel = await _ProjectsManager.SearchProjectsAsync();
            //viewModel = Projects.Payload;
            //return Json(responseModel.Payload);
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.Projects_Search)]
        public async Task<IActionResult> SearchProjects(SearchProjectsParams searchParams)
        {
            var responseModel = await _ProjectsManager.SearchProjectsAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new/{Id}", Name = RouteNames.Projects_New)]
        public async Task<IActionResult> NewProjectAsync(int Id)
        {
            var viewModel = new ProjectViewModel();

            viewModel.User = GetCurrentUser();
            return View("EditProject", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.Projects_Edit)]
        public async Task<IActionResult> EditProjectAsync(int id)
        {
            var viewModel = new ProjectViewModel();

            var ProjectResponse = await _ProjectsManager.GetProjectByIdAsync(id);

            viewModel.Project = ProjectResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditProject", viewModel);
        }

        //controller method for saving Project
        [HttpPost("save", Name = RouteNames.Projects_Save)]
        public async Task<IActionResult> SaveProjectAsync(SaveProjectRequestModel requestModel)
        {
            var responseModel = await _ProjectsManager.SaveProjectAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Projects_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.Projects_Delete)]
        public async Task<IActionResult> DeleteProjectAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _ProjectsManager.DeleteProjectAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Projects_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.RegularActivities;
using IDAProject.Web.Models.Dto.RegularActivities;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.RegularActivities;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class RegularActivitiesController : BaseController
    {
        private readonly IRegularActivitiesManager _RegularActivitiesManager;
        private readonly IMasterDataManager _masterDataManager;

        public RegularActivitiesController(
            ILogger<RegularActivitiesController> logger,
            IAccountManager accountManager,
            IRegularActivitiesManager RegularActivitiesManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _RegularActivitiesManager = RegularActivitiesManager;
            _masterDataManager = masterDataManager;
        }
        [HttpGet("RegularActivitiesList", Name = RouteNames.RegularActivities_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new RegularActivitiesViewModel();
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "RegularActivities");
            //var responseModel = await _RegularActivitiesManager.SearchRegularActivitiesAsync();
            //viewModel = RegularActivities.Payload;
            //return Json(responseModel.Payload);
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.RegularActivities_Search)]
        public async Task<IActionResult> SearchRegularActivities(SearchRegularActivitiesParams searchParams)
        {
            var responseModel = await _RegularActivitiesManager.SearchRegularActivitiesAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new/{Id}", Name = RouteNames.RegularActivities_New)]
        public async Task<IActionResult> NewRegularActivityAsync(int Id)
        {
            var viewModel = new RegularActivityViewModel();

            viewModel.User = GetCurrentUser();
            return View("EditRegularActivity", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.RegularActivities_Edit)]
        public async Task<IActionResult> EditRegularActivityAsync(int id)
        {
            var viewModel = new RegularActivityViewModel();

            var RegularActivityResponse = await _RegularActivitiesManager.GetRegularActivityByIdAsync(id);

            viewModel.RegularActivity = RegularActivityResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditRegularActivity", viewModel);
        }

        //controller method for saving RegularActivity
        [HttpPost("save", Name = RouteNames.RegularActivities_Save)]
        public async Task<IActionResult> SaveRegularActivityAsync(SaveRegularActivityRequestModel requestModel)
        {
            var responseModel = await _RegularActivitiesManager.SaveRegularActivityAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.RegularActivities_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.RegularActivities_Delete)]
        public async Task<IActionResult> DeleteRegularActivityAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _RegularActivitiesManager.DeleteRegularActivityAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.RegularActivities_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
    }
}

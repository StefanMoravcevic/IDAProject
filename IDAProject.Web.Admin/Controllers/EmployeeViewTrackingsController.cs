using IDAProject.Web.Admin.Managers;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.EmployeeViewTrackings;
using IDAProject.Web.Models.Dto.TasksPlanningComments;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.EmployeeViewTrackings;
using Microsoft.AspNetCore.Mvc;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class EmployeeViewTrackingsController : BaseController
    {
        private readonly IEmployeeViewTrackingsManager _EmployeeViewTrackingsManager;
        private readonly IMasterDataManager _masterDataManager;

        public EmployeeViewTrackingsController(
            ILogger<EmployeeViewTrackingsController> logger,
            IAccountManager accountManager,
            IEmployeeViewTrackingsManager EmployeeViewTrackingsManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _EmployeeViewTrackingsManager = EmployeeViewTrackingsManager;
            _masterDataManager = masterDataManager;
        }
        //[HttpGet("EmployeeViewTrackingsList", Name = RouteNames.EmployeeViewTrackings_List)]
        //public async Task<IActionResult> Index()
        //{
        //    var viewModel = new EmployeeViewTrackingsViewModel();
        //    await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "EmployeeViewTrackings");
        //    //var responseModel = await _EmployeeViewTrackingsManager.SearchEmployeeViewTrackingsAsync();
        //    //viewModel = EmployeeViewTrackings.Payload;
        //    //return Json(responseModel.Payload);
        //    return View(viewModel);
        //}

        [HttpPost("search", Name = RouteNames.EmployeeViewTrackings_Search)]
        public async Task<IActionResult> SearchEmployeeViewTrackings(SearchEmployeeViewTrackingsParams searchParams)
        {
            var responseModel = await _EmployeeViewTrackingsManager.SearchEmployeeViewTrackingsAsync(searchParams);
            return Json(responseModel.Payload);
        }
        [HttpPost("getBookmarkedEmployeesWithoutPlanNextWorkingDay", Name = RouteNames.EmployeeViewTrackings_GetBookmarkedEmployeesWithoutPlanNextWorkingDay)]
        public async Task<IActionResult> GetBookmarkedEmployeesWithoutPlanNextWorkingDay([FromBody] List<int> bookmarkedEmployeeIds)
        {
            var responseModel = await _EmployeeViewTrackingsManager.GetBookmarkedEmployeesWithoutPlanNextWorkingDayAsync(bookmarkedEmployeeIds);
            return Json(responseModel.Payload);
        }

        //[HttpGet("new/{Id}", Name = RouteNames.EmployeeViewTrackings_New)]
        //public async Task<IActionResult> NewEmployeeViewTrackingAsync(int Id)
        //{
        //    var viewModel = new EmployeeViewTrackingViewModel();

        //    viewModel.User = GetCurrentUser();
        //    return View("EditEmployeeViewTracking", viewModel);
        //}

        //[HttpGet("edit/{id}", Name = RouteNames.EmployeeViewTrackings_Edit)]
        //public async Task<IActionResult> EditEmployeeViewTrackingAsync(int id)
        //{
        //    var viewModel = new EmployeeViewTrackingViewModel();

        //    var EmployeeViewTrackingResponse = await _EmployeeViewTrackingsManager.GetEmployeeViewTrackingByIdAsync(id);

        //    viewModel.EmployeeViewTracking = EmployeeViewTrackingResponse.Payload!;
        //    viewModel.User = GetCurrentUser();

        //    return View("EditEmployeeViewTracking", viewModel);
        //}

        //controller method for saving EmployeeViewTracking
        [HttpPost("save", Name = RouteNames.EmployeeViewTrackings_Save)]
        public async Task<IActionResult> SaveEmployeeViewTrackingAsync([FromBody] SaveEmployeeViewTrackingRequestModel requestModel)
        {
            var user = GetCurrentUser();
            requestModel.ViewedFrom = DateTime.Now;
            requestModel.ViewerEmployeeId = user.EmployeeId;
            var responseModel = await _EmployeeViewTrackingsManager.SaveEmployeeViewTrackingAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.EmployeeViewTrackings_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
        [HttpPost("saveChecked", Name = RouteNames.EmployeeViewTrackings_SaveChecked)]
        public async Task<IActionResult> SaveCheckedEmployeeViewTrackingAsync([FromBody] SaveEmployeeViewTrackingRequestModel requestModel)
        {
            var user = GetCurrentUser();
            requestModel.ViewerEmployeeId = user.EmployeeId;
            var responseModel = await _EmployeeViewTrackingsManager.SaveEmployeeViewTrackingCheckedAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.EmployeeViewTrackings_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.EmployeeViewTrackings_Delete)]
        public async Task<IActionResult> DeleteEmployeeViewTrackingAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _EmployeeViewTrackingsManager.DeleteEmployeeViewTrackingAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.EmployeeViewTrackings_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("hide", Name = RouteNames.EmployeeViewTrackings_Hide)]
        public async Task<IActionResult> HideEmployeeViewTrackingFromHomePageAsync(SaveEmployeeViewTrackingRequestModel requestModel)
        {
            var user = GetCurrentUser();
            var view = await _EmployeeViewTrackingsManager.GetEmployeeViewTrackingByIdAsync(requestModel.Id);
            view.Payload.HideFromHomePage = true;
            view.Payload.ViewedUntil = DateTime.Now;
            var responseModel = await _EmployeeViewTrackingsManager.SaveEmployeeViewTrackingAsync(view.Payload);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.EmployeeViewTrackings_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpGet("update/{id}", Name = RouteNames.EmployeeViewTrackings_Update)]
        public async Task<IActionResult> UpdateEmployeeViewTrackingAsync(int id)
        {
            var user = GetCurrentUser();
            var existing = await _EmployeeViewTrackingsManager.GetEmployeeViewTrackingByIdAsync(id);
            if(existing.Payload != null)
            {
                existing.Payload.ViewedFrom = DateTime.Now;
            }
            var responseModel = await _EmployeeViewTrackingsManager.SaveEmployeeViewTrackingAsync(existing.Payload);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.EmployeeViewTrackings_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
    }
}

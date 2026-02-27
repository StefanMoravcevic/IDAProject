using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.UserNotifications;
using IDAProject.Web.Models.Dto.UserNotifications;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.UserNotifications;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class UserNotificationsController : BaseController
    {
        private readonly IUserNotificationsManager _UserNotificationsManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public UserNotificationsController(
            ILogger<UserNotificationsController> logger,
            IAccountManager accountManager,
            IStringLocalizer<SharedResources> localizer,
            IUserNotificationsManager UserNotificationsManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _UserNotificationsManager = UserNotificationsManager;
            _masterDataManager = masterDataManager;
            _localizer = localizer;
        }
        [HttpGet("UserNotificationsList", Name = RouteNames.UserNotifications_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new UserNotificationsViewModel(_localizer);
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "UserNotifications");
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.UserNotifications_Search)]
        public async Task<IActionResult> SearchUserNotifications(SearchUserNotificationsParams searchParams)
        {
            var responseModel = await _UserNotificationsManager.SearchUserNotificationsAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new", Name = RouteNames.UserNotifications_New)]
        public async Task<IActionResult> NewUserNotificationAsync(int Id)
        {
            var viewModel = new UserNotificationViewModel();

            viewModel.User = GetCurrentUser();
            viewModel.Sectors = await _masterDataManager.GetSelectOptionsByTableAsync("Sectors", "Name");
            return View("EditUserNotification", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.UserNotifications_Edit)]
        public async Task<IActionResult> EditUserNotificationAsync(int id)
        {
            var viewModel = new UserNotificationViewModel();

            var UserNotificationResponse = await _UserNotificationsManager.GetUserNotificationByIdAsync(id);

            viewModel.UserNotification = UserNotificationResponse.Payload!;
            viewModel.Sectors = await _masterDataManager.GetSelectOptionsByTableAsync("Sectors", "Name");
            viewModel.User = GetCurrentUser();

            return View("EditUserNotification", viewModel);
        }

        [HttpPost("save", Name = RouteNames.UserNotifications_Save)]
        public async Task<IActionResult> SaveUserNotificationAsync(SaveUserNotificationRequestModel requestModel)
        {
            var responseModel = await _UserNotificationsManager.SaveUserNotificationAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.UserNotifications_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.UserNotifications_Delete)]
        public async Task<IActionResult> DeleteUserNotificationAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _UserNotificationsManager.DeleteUserNotificationAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.UserNotifications_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
    }
}

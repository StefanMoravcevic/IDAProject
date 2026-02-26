using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Managers;
using IDAProject.Web.Admin.Models.Accounts;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.TagHelpers;
using IDAProject.Web.Admin.Models.ViewModels;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IAccountManager _accountManager;
        protected readonly ILogger _logger;

        public BaseController(IAccountManager accountManager, ILogger logger)
        {
            _accountManager = accountManager;
            _logger = logger;
        }

        protected UserAccount GetCurrentUser()
        {
            var token = Request.Cookies[Constants.AdminCookieToken];

            var result = _accountManager.GetUserFromJwt(token!);
            return result!;
        }

        protected async Task UpdateNavigationWithAjaxTableViewModel(NavigationWithAjaxTableViewModel viewModel, IMasterDataManager masterDataManager, string tableName)
        {
            viewModel.User = GetCurrentUser();

            try
            {
                var hiddenColumnsResponse = await masterDataManager.GetTableSettingsAsync(viewModel.User.Id, tableName);
                if (hiddenColumnsResponse.Valid)
                {
                    viewModel.TableSettings = hiddenColumnsResponse.Payload!;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Route: Update navigation with ajax failed");
                viewModel.Notification = new NotificationViewModel
                {
                    Message = e.Message,
                    Type = NotificationType.Warning
                };
            }
        }

        public string GetApiDocumentDownloadEnpointUrl()
        {
            var baseManager = _accountManager as BaseManager;
            return baseManager!.GetEndpointUrl("api/documents/download");
        }
    }
}
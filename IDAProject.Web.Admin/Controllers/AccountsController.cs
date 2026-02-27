using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using IDAProject.Web.Admin.Models.Accounts;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.TagHelpers;
using IDAProject.Web.Admin.Models.ViewModels;
using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.General.Enums;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;
using IDAProject.Web.Models.Dto.Users;
using IDAProject.Web.Admin.Managers;
using IDAProject.Web.Models.RequestModels.Users;
using Microsoft.AspNetCore.Authorization;
using IDAProject.Web.Admin.Managers.Attributes;

namespace IDAProject.Web.Admin.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly IAccountManager _accountManager;
        private readonly IEmployeesManager _employeesManager;
        private readonly IUsersManager _userManager;

        public AccountsController(ILogger<AccountsController> logger,
            IUsersManager userManager, IAccountManager accountManager, IEmployeesManager employeesManager)
            : base(accountManager, logger)
        {
            _accountManager = accountManager;
            _userManager = userManager;
            _employeesManager = employeesManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var useLdap = await _accountManager.GetLdapSetting();
            var viewModel = new NavigationBaseViewModel();
            ViewBag.DomainAuthentication = useLdap.Payload;
            return View(viewModel);
        }

        private async Task<SaveUserLogRequestModel> LogLoginDetails(HttpContext context, string loginUserName)
        {
            var windowsUserName = context.User.Identity?.Name;
            //excluded for PIO b/c no external web access
            //var publicIpAddress = await GetPublicIpAddress();
            var saveModel = new SaveUserLogRequestModel
            {
                WindowsUserName = windowsUserName,
                UserName = loginUserName,
                RemoteIp = context.Connection.RemoteIpAddress?.ToString(),
                LocalIp = context.Connection.LocalIpAddress?.ToString(),
                RemotePort = context.Connection.RemotePort,
                LocalPort = context.Connection.LocalPort,
                LoginDateTime = DateTime.Now,
                //PublicIp = publicIpAddress
            };
            return saveModel;
        }

        private async Task<string> GetPublicIpAddress()
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync("https://api.ipify.org");
            }
        }
        [AllowAnonymous]
        [HttpPost("accounts/login", Name = RouteNames.Accounts_DoLogin)]
        public async Task<IActionResult> DoLoginAsync(LoginModel model)
        {
            SaveUserLogRequestModel saveModel;
            var viewModel = new NavigationBaseViewModel();
            if (ModelState.IsValid)
            {
                var tokenResponse = await _accountManager.GenerateTokenAsync(model);
                if (tokenResponse.Valid)
                {
                    var tokenCookie = Request.Cookies[Constants.AdminCookieToken];

                    var cookieOptions = new CookieOptions()
                    {
                        IsEssential = true,
                        //Expires = DateTime.UtcNow.AddMinutes(Constants.ShortLivedTokenExpirationInMinutes),
                        Expires = DateTime.UtcNow.AddDays(Constants.LongLivedTokenExpirationInDays),
                        SameSite = SameSiteMode.Strict
                    };

                    if (tokenCookie != null)
                    {
                        Response.Cookies.Delete(Constants.AdminCookieToken);
                    }

                    Response.Cookies.Append(Constants.AdminCookieToken, tokenResponse.Payload!, cookieOptions);
                    saveModel = await LogLoginDetails(HttpContext, model.Username);
                    var user = _accountManager.GetUserFromJwt(tokenResponse.Payload!);
                    saveModel.Note = "Successfull login";
                    saveModel.AspNetUserId = user.Id;
                    await _userManager.SaveUserLogAsync(saveModel);
                    return RedirectToAction("Index", "Home");
                }

                viewModel.Notification = new NotificationViewModel
                {
                    Type = NotificationType.Error,
                    Message = tokenResponse.Message
                };
                saveModel = await LogLoginDetails(HttpContext, model.Username);
                saveModel.Note = "Unsuccessfull login! - " + NotificationType.Error + "/" + tokenResponse.Message;
                await _userManager.SaveUserLogAsync(saveModel);
            }
            return RedirectToAction("Login");
            //return View("Login", viewModel);
        }
        [AllowAnonymous]
        [HttpGet("accounts/logout", Name = RouteNames.Accounts_Logout)]
        public async Task<IActionResult> Logout()
        {
            var user = GetCurrentUser();
            var loginSessions = await _userManager.SearchUserLogsAsync(new SearchUserLogsParams { UserId = user.Id });
            var lastLoginSession = loginSessions.Payload.OrderByDescending(x => x.LoginDateTime).Where(y => y.LogoutDateTime == null).FirstOrDefault();
            if (lastLoginSession != null)
            {
                await _userManager.SaveUserLogAsync(new SaveUserLogRequestModel
                {
                    LocalIp = lastLoginSession.LocalIp,
                    LogoutDateTime = DateTime.Now,
                    LoginDateTime = lastLoginSession.LoginDateTime,
                    AspNetUserId = lastLoginSession.AspNetUserId,
                    Id = lastLoginSession.Id,
                    LocalPort = lastLoginSession.LocalPort,
                    PublicIp = lastLoginSession.PublicIp,
                    Note = lastLoginSession.Note + " and logout",
                    RemoteIp = lastLoginSession.RemoteIp,
                    RemotePort = lastLoginSession.RemotePort,
                    UserName = lastLoginSession.UserName,
                    WindowsUserName = lastLoginSession.WindowsUserName
                });
            }
            Response.Cookies.Delete(Constants.AdminCookieToken, new CookieOptions
            {
                Path = "/",
                HttpOnly = true,
                Secure = true, // Match the original cookie settings
                SameSite = SameSiteMode.Strict
            });
            SignOut(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        [HttpGet("accounts/registerAccount", Name = RouteNames.Accounts_RegisterAccount)]
        public IActionResult RegisterAccountIndex()
        {
            var viewModel = new NavigationBaseViewModel();
            return PartialView("RegisterAccountModal", viewModel);
        }
        [AllowAnonymous]
        [HttpGet("accounts/resetPassword", Name = RouteNames.Accounts_ResetPassword)]
        public IActionResult ResetPasswordIndex()
        {
            var viewModel = new NavigationBaseViewModel();
            return PartialView("ResetPasswordModal", viewModel);
        }
        [CustomAuthorization((int)AspNetRoles.Any, (int)AspNetFeatures.Any)]
        [HttpGet("accounts/changePassword", Name = RouteNames.Accounts_ChangePassword)]
        public IActionResult ChangePasswordIndex()
        {
            var viewModel = new NavigationBaseViewModel();
            return PartialView("ChangePasswordModal", viewModel);
        }
        [AllowAnonymous]
        [HttpPost("accounts/registerAccountConfirm", Name = RouteNames.Accounts_RegisterAccountConfirm)]
        public async Task<IActionResult> RegisterAccountAsync(RegisterModel model)
        {
            var response = await _accountManager.RegisterAccountAsync(model);
            return Json(response);
        }
        [AllowAnonymous]
        [HttpPost("accounts/resetPasswordConfirm", Name = RouteNames.Accounts_ResetPasswordConfirm)]
        public async Task<IActionResult> ResetPasswordAsync(RegisterModel model)
        {
            var response = await _accountManager.ResetPasswordAsync(model);
            return Json(response);
        }
        [CustomAuthorization((int)AspNetRoles.Any, (int)AspNetFeatures.Any)]
        [HttpPost("accounts/changePasswordConfirm", Name = RouteNames.Accounts_ChangePasswordConfirm)]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var user = GetCurrentUser();
            model.Username = user.UserName;
            model.UserId = user.Id;
            var response = await _accountManager.ChangePasswordAsync(model);
            return Json(response);
        }
        [CustomAuthorization((int)AspNetRoles.Administrator, (int)AspNetFeatures.Any)]
        [HttpGet("accounts/adminResetPassword/{userId}", Name = RouteNames.Accounts_AdminResetPassword)]
        public async Task<IActionResult> AdminResetPasswordAsync(int userId)
        {
            var adminUser = GetCurrentUser();
            var response = await _accountManager.AdminResetPasswordAsync(userId, adminUser.Id);
            return Json(response);
        }
    }
}
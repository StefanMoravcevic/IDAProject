using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using IDAProject.Web.Admin.Models;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.Home;
using IDAProject.Web.Models.Common;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.General;
using Newtonsoft.Json;
using IDAProject.Web.Models.RequestModels.ExchangeRates;
using IDAProject.Web.Admin.Models.ViewModels;
using IDAProject.Web.Admin.Managers.Attributes;
using IDAProject.Web.Admin.Managers;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    [CustomAuthorization((int)AspNetRoles.Any, (int)AspNetFeatures.Any)]
    public class HomeController : BaseController
    {
        private readonly IMasterDataManager _masterDataManager;
        private readonly IConfiguration _configuration;
        private readonly IReportsManager _reportsManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public HomeController(ILogger<HomeController> logger, IAccountManager accountManager, IReportsManager reportsManager, IStringLocalizer<SharedResources> localizer,

            IMasterDataManager masterDataManager, IConfiguration configuration) : base(accountManager, logger)
        {
            _reportsManager = reportsManager;
            _masterDataManager = masterDataManager;
            _configuration = configuration;
            _localizer = localizer;
        }

        [HttpGet("index", Name = RouteNames.Home_Dashboard)]
        public async Task<IActionResult> Index()
        {
            var cookieOptions = new CookieOptions()
            {
                IsEssential = true,
                Expires = DateTime.Now.AddDays(Constants.LongLivedTokenExpirationInDays),
                SameSite = SameSiteMode.Strict
            };

            Response.Cookies.Append("SelectedApplication", "IDAProject", cookieOptions);
            var viewModel = new HomeViewModel();
            var user = GetCurrentUser();
            viewModel.User = user;
            if (user.Roles.Contains(AspNetRoles.Administrator.ToString()))
            {
                return View(viewModel);
            }
            else if (user.Roles.Contains(AspNetRoles.Skeniranje.ToString()))
            {
                return RedirectToAction("IndexWithoutHeader", "OrderLines");
            }

            return View(viewModel);
        }

        [HttpGet("chooseApp", Name = RouteNames.Home_ChooseApp)]
        public async Task<IActionResult> ChooseApp()
        {
            var cookieSelectedApp = new CookieOptions()
            {
                IsEssential = true,
                Expires = DateTime.Now.AddDays(Constants.LongLivedTokenExpirationInDays),
                SameSite = SameSiteMode.Strict
            };

            var user = GetCurrentUser();

            return View();
        }

        [HttpGet("privacy", Name = RouteNames.Home_Privacy)]
        public IActionResult Privacy()
        {
            var viewModel = new NavigationBaseViewModel();
            var user = GetCurrentUser();
            viewModel.User = user;
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
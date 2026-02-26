using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class ErrorController : BaseController
    {
        public ErrorController(ILogger<ErrorController> logger, IAccountManager accountManager) : base(accountManager, logger)
        {
        }

        [HttpGet/*(Name = RouteNames.Home_Dashboard)*/]
        public IActionResult Index(string? userMessage)
        {
            ViewBag.UserMessage = userMessage;
            var viewModel = new NavigationBaseViewModel();
            viewModel.User = GetCurrentUser();

            return View("ErrorModal", viewModel);
        }

    }
}
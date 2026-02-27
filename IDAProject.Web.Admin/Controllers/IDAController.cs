using System.Threading.Tasks;
using IDAProject.Web.Admin.Managers.Attributes;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels;
using IDAProject.Web.Admin.Models.ViewModels.IDA;
using IDAProject.Web.Models.General.Enums;
using Microsoft.AspNetCore.Mvc;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    [CustomAuthorization((int)AspNetRoles.Any, (int)AspNetFeatures.Any)]
    public class IDAController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMasterDataManager _masterDataManager;
        public IDAController(ILogger<IDAController> logger, IAccountManager accountManager, IConfiguration configuration, IMasterDataManager masterDataManager) : base(accountManager, logger)
        {
            _configuration = configuration;
            _masterDataManager = masterDataManager;
        }


        [HttpGet("index", Name = RouteNames.IDA_Index)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new IDAViewModel();
            viewModel.Projects = await _masterDataManager.GetSelectOptionsByTableAsync("Projects", "Description");
            viewModel.Tasks = await _masterDataManager.GetSelectOptionsByTableAsync("IdaTasks", "Name");
            viewModel.ActivityTypes = await _masterDataManager.GetSelectOptionsByTableAsync("ActivityTypes", "Name");
            viewModel.PlanStatuses = await _masterDataManager.GetSelectOptionsByTableAsync("PlanStatuses", "Name");
            viewModel.RegularActivities = await _masterDataManager.GetSelectOptionsByTableAsync("RegularActivities", "Name");
            viewModel.User = GetCurrentUser();
            viewModel.Today = DateTime.Now.Date.ToString("dd.MM.yyyy");
            return View(viewModel);
        }
    }
}

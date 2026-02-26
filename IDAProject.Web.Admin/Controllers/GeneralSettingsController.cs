using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.GeneralSettings;
using IDAProject.Web.Models.Dto.Common;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class GeneralSettingsController : BaseController
    {
        private readonly IMasterDataManager _masterDataManager;

        public GeneralSettingsController(
            ILogger<GeneralSettingsController> logger,
            IMasterDataManager masterDataManager,
            IUsersManager usersManager, IAccountManager accountManager)
            : base(accountManager, logger)
        {
            _masterDataManager = masterDataManager;
        }

        [HttpGet(Name = RouteNames.GeneralSettings_Edit)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new GeneralSettingViewModel();

            viewModel.GeneralSetting = await _masterDataManager.GetGeneralSettingsAsync();
            viewModel.MeasureUnits = await _masterDataManager.GetSelectOptionsByTableAsync("MeasureUnits", "Name");
            viewModel.Currencies = await _masterDataManager.GetSelectOptionsByTableAsync("Currencies", "Name");
            viewModel.User = GetCurrentUser();

            return View(viewModel);
        }

        [HttpPost("saveGeneralSetting", Name = RouteNames.GeneralSettings_Save)]
        public async Task<IActionResult> SaveGeneralSettingAsync(SaveGeneralSettingRequestModel requestModel)
        {
            var responseModel = await _masterDataManager.SaveGeneralSettingAsync(requestModel);
            return Json(responseModel);
        }
    }
}
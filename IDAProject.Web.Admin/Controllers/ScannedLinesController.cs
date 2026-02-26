using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.ScannedLines;
using IDAProject.Web.Models.Dto.ScannedLines;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.ScannedLines;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class ScannedLinesController : BaseController
    {
        private readonly IScannedLinesManager _ScannedLinesManager;
        private readonly IOrderLinesManager _orderLinesManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ScannedLinesController(
            ILogger<ScannedLinesController> logger,
            IAccountManager accountManager,
            IStringLocalizer<SharedResources> localizer,
            IScannedLinesManager ScannedLinesManager,
            IOrderLinesManager orderLinesManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _ScannedLinesManager = ScannedLinesManager;
            _masterDataManager = masterDataManager;
            _orderLinesManager = orderLinesManager;
            _localizer = localizer;
        }

        [HttpGet("ScannedLinesList", Name = RouteNames.ScannedLines_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ScannedLinesViewModel(_localizer);
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "ScannedLines");
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.ScannedLine_Search)]
        public async Task<IActionResult> SearchScannedLinesAsync(SearchScannedLinesParams searchParams)
        {
            var response = await _ScannedLinesManager.SearchScannedLinesAsync(searchParams);
            return Json(response.Payload);
        }



    }
}

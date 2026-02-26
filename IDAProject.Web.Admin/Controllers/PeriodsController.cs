using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.Periods;
using IDAProject.Web.Models.Dto.Periods;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.Periods;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class PeriodsController : BaseController
    {
        private readonly IPeriodsManager _PeriodsManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public PeriodsController(
            ILogger<PeriodsController> logger,
            IAccountManager accountManager,
            IStringLocalizer<SharedResources> localizer,
            IPeriodsManager PeriodsManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _PeriodsManager = PeriodsManager;
            _masterDataManager = masterDataManager;
            _localizer = localizer;
        }
        [HttpGet("PeriodsList", Name = RouteNames.Periods_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new PeriodsViewModel(_localizer);
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "Periods");
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.Periods_Search)]
        public async Task<IActionResult> SearchPeriods(SearchPeriodsParams searchParams)
        {
            var responseModel = await _PeriodsManager.SearchPeriodsAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new", Name = RouteNames.Periods_New)]
        public async Task<IActionResult> NewPeriodAsync(int Id)
        {
            var viewModel = new PeriodViewModel();

            viewModel.User = GetCurrentUser();
            return View("EditPeriod", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.Periods_Edit)]
        public async Task<IActionResult> EditPeriodAsync(int id)
        {
            var viewModel = new PeriodViewModel();

            var PeriodResponse = await _PeriodsManager.GetPeriodByIdAsync(id);

            viewModel.Period = PeriodResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditPeriod", viewModel);
        }

        [HttpPost("save", Name = RouteNames.Periods_Save)]
        public async Task<IActionResult> SavePeriodAsync(SavePeriodRequestModel requestModel)
        {
            DateTime dateFrom;
            DateTime dateTo;
            var user = GetCurrentUser();
            if (DateTime.TryParseExact(requestModel.DateFromForFormat, "dd.MM.yyyy HH:mm",
                               CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFrom))
            {
                requestModel.DateFrom = dateFrom;
            }

            if (DateTime.TryParseExact(requestModel.DateToForFormat, "dd.MM.yyyy HH:mm",
                               CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTo))
            {
                requestModel.DateTo = dateTo;
            }
            var responseModel = await _PeriodsManager.SavePeriodAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Periods_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpDelete("delete/{id}", Name = RouteNames.Periods_Delete)]
        public async Task<IActionResult> DeletePeriodAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _PeriodsManager.DeletePeriodAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Periods_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
    }
}

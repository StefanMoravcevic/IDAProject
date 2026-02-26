using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.OrderHeaderArchives;
using IDAProject.Web.Models.Dto.OrderHeaderArchives;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.OrderHeaderArchives;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class OrderHeaderArchivesController : BaseController
    {
        private readonly IOrderHeaderArchivesManager _OrderHeaderArchivesManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public OrderHeaderArchivesController(
            ILogger<OrderHeaderArchivesController> logger,
            IAccountManager accountManager,
            IStringLocalizer<SharedResources> localizer,
            IOrderHeaderArchivesManager OrderHeaderArchivesManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _OrderHeaderArchivesManager = OrderHeaderArchivesManager;
            _masterDataManager = masterDataManager;
            _localizer = localizer;
        }
        [HttpGet("OrderHeaderArchivesList", Name = RouteNames.OrderHeaderArchives_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new OrderHeaderArchivesViewModel(_localizer);
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "OrderHeaderArchives");
            //var responseModel = await _OrderHeaderArchivesManager.SearchOrderHeaderArchivesAsync();
            //viewModel = OrderHeaderArchives.Payload;
            //return Json(responseModel.Payload);
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.OrderHeaderArchives_Search)]
        public async Task<IActionResult> SearchOrderHeaderArchives(SearchOrderHeaderArchivesParams searchParams)
        {
            var responseModel = await _OrderHeaderArchivesManager.SearchOrderHeaderArchivesAsync(searchParams);
            return Json(responseModel.Payload);
        }
    }
}

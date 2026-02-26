using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.OrderLineArchives;
using IDAProject.Web.Models.Dto.OrderLineArchives;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.OrderLineArchives;
using Microsoft.Extensions.Localization;
using IDAProject.Web.Admin.Managers;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class OrderLineArchivesController : BaseController
    {
        private readonly IOrderLineArchivesManager _OrderLineArchivesManager;
        private readonly IOrderHeadersManager _orderHeadersManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public OrderLineArchivesController(
            ILogger<OrderLineArchivesController> logger,
            IAccountManager accountManager,
            IStringLocalizer<SharedResources> localizer,
            IOrderLineArchivesManager OrderLineArchivesManager,
            IOrderHeadersManager orderHeadersManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _OrderLineArchivesManager = OrderLineArchivesManager;
            _masterDataManager = masterDataManager;
            _localizer = localizer;
            _orderHeadersManager = orderHeadersManager;
        }
        [HttpGet("OrderLineArchivesList/{orderHeaderArchiveId}", Name = RouteNames.OrderLineArchives_List)]
        public async Task<IActionResult> Index(int? orderHeaderArchiveId)
        {
            var viewModel = new OrderLineArchivesViewModel(_localizer);
            viewModel.OrderHeaderArchiveId = orderHeaderArchiveId;
            var orderHeaders = await _orderHeadersManager.SearchOrderHeadersAsync(new Web.Models.RequestModels.OrderHeaders.SearchOrderHeadersParams { IsArchived = true });
            var orderHeaderOptions = orderHeaders.Payload
            .Select(o => new GenericSelectOption
            {
                Value = o.Id,
                Description = o.CustomerOrderNumber
            })
            .ToList();
            viewModel.OrderHeaders = orderHeaderOptions;
            viewModel.FebiItems = await _masterDataManager.GetSelectOptionsByTableAsync("FebiItems", "FebiArticleNo");
            viewModel.PartnerCodes = new List<GenericSelectOption>
            {
                new GenericSelectOption { Value = 1, Description = "BEOGRADSKI" },
                new GenericSelectOption { Value = 2, Description = "NEBEOGRADSKI" }
            };
            viewModel.Segments = new List<GenericSelectOption>
            {
                new GenericSelectOption { Value = 1, Description = "I" },
                new GenericSelectOption { Value = 2, Description = "II" },
                new GenericSelectOption { Value = 3, Description = "III" }
            };
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "OrderLineArchives");
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.OrderLineArchives_Search)]
        public async Task<IActionResult> SearchOrderLineArchives(SearchOrderLineArchivesParams searchParams)
        {
            var responseModel = await _OrderLineArchivesManager.SearchOrderLineArchivesAsync(searchParams);
            return Json(responseModel.Payload);
        }
    }
}

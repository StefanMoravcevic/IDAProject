using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.OrderHeaders;
using IDAProject.Web.Models.Dto.OrderHeaders;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.OrderHeaders;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class OrderHeadersController : BaseController
    {
        private readonly IOrderHeadersManager _OrderHeadersManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public OrderHeadersController(
            ILogger<OrderHeadersController> logger,
            IAccountManager accountManager,
            IOrderHeadersManager OrderHeadersManager,
            IStringLocalizer<SharedResources> localizer,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _OrderHeadersManager = OrderHeadersManager;
            _masterDataManager = masterDataManager;
            _localizer = localizer;
        }
        [HttpGet("OrderHeadersList", Name = RouteNames.OrderHeaders_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new OrderHeadersViewModel(_localizer);
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "OrderHeaders");
            //var responseModel = await _OrderHeadersManager.SearchOrderHeadersAsync();
            //viewModel = OrderHeaders.Payload;
            //return Json(responseModel.Payload);
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.OrderHeaders_Search)]
        public async Task<IActionResult> SearchOrderHeaders(SearchOrderHeadersParams searchParams)
        {
            var responseModel = await _OrderHeadersManager.SearchOrderHeadersAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new/{Id}", Name = RouteNames.OrderHeaders_New)]
        public async Task<IActionResult> NewOrderHeaderAsync(int Id)
        {
            var viewModel = new OrderHeaderViewModel();

            viewModel.User = GetCurrentUser();
            return View("EditOrderHeader", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.OrderHeaders_Edit)]
        public async Task<IActionResult> EditOrderHeaderAsync(int id)
        {
            var viewModel = new OrderHeaderViewModel();

            var OrderHeaderResponse = await _OrderHeadersManager.GetOrderHeaderByIdAsync(id);

            viewModel.OrderHeader = OrderHeaderResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditOrderHeader", viewModel);
        }

        //controller method for saving OrderHeader
        [HttpPost("save", Name = RouteNames.OrderHeaders_Save)]
        public async Task<IActionResult> SaveOrderHeaderAsync(SaveOrderHeaderRequestModel requestModel)
        {
            var responseModel = await _OrderHeadersManager.SaveOrderHeaderAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.OrderHeaders_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.OrderHeaders_Delete)]
        public async Task<IActionResult> DeleteOrderHeaderAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _OrderHeadersManager.DeleteOrderHeaderAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.OrderHeaders_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
    }
}

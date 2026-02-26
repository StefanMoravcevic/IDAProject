using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.OrderLines;
using IDAProject.Web.Models.Dto.OrderLines;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.OrderLines;
using Microsoft.Extensions.Localization;
using IDAProject.Web.Admin.Managers;
using IDAProject.Web.Admin.Models.ViewModels.ScannedLines;
using IDAProject.Web.Models.RequestModels.ScannedLines;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.Printers;
using IDAProject.Web.Models.Auth.RequestModels;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class OrderLinesController : BaseController
    {
        private readonly IOrderLinesManager _OrderLinesManager;
        private readonly IOrderHeadersManager _orderHeadersManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IScannedLinesManager _ScannedLinesManager;
        private readonly IConfiguration _configuration;
        private readonly IPrintersManager _printersManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public OrderLinesController(
            ILogger<OrderLinesController> logger,
            IAccountManager accountManager,
            IOrderHeadersManager  orderHeadersManager,
            IStringLocalizer<SharedResources> localizer,
            IScannedLinesManager  scannedLinesManager,
            IPrintersManager printersManager,
            IOrderLinesManager OrderLinesManager,
            IConfiguration configuration,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _OrderLinesManager = OrderLinesManager;
            _masterDataManager = masterDataManager;
            _ScannedLinesManager = scannedLinesManager;
            _orderHeadersManager = orderHeadersManager;
            _printersManager = printersManager;
            _configuration = configuration;
            _localizer = localizer;
        }
        [HttpGet("OrderLinesList/{orderHeaderId}", Name = RouteNames.OrderLines_List)]
        public async Task<IActionResult> Index(int? orderHeaderId)
        {
            var viewModel = new OrderLinesViewModel(_localizer);
            var apiUrl = _configuration.GetSection("WebApi:Url").Value;
            viewModel.OrderHeaderId = orderHeaderId;
            var orderHeaders = await _orderHeadersManager.SearchOrderHeadersAsync(new Web.Models.RequestModels.OrderHeaders.SearchOrderHeadersParams { });
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
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "OrderLines");
            viewModel.ApiUrl = apiUrl;
            return View(viewModel);
        }

        [HttpGet("OrderLinesListNoHeader", Name = RouteNames.OrderLines_ListNoHeader)]
        public async Task<IActionResult> IndexWithoutHeader(int? orderHeaderId)
        {
            var viewModel = new OrderLinesViewModel(_localizer);
            var apiUrl = _configuration.GetSection("WebApi:Url").Value;
            var orderHeaders = await _orderHeadersManager.SearchOrderHeadersAsync(new Web.Models.RequestModels.OrderHeaders.SearchOrderHeadersParams { });
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
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "OrderLines");
            viewModel.ApiUrl = apiUrl;
            return View("Index",viewModel);
        }

        [HttpGet("run", Name = RouteNames.OrderLines_RunSignalR)]
        public async Task<IActionResult> RunSignalr()
        {
            var result = await _OrderLinesManager.SearchOrderLines();
            return Json(result);
        }

        [HttpPost("search", Name = RouteNames.OrderLines_Search)]
        public async Task<IActionResult> SearchOrderLines(SearchOrderLinesParams searchParams)
        {
            var responseModel = await _OrderLinesManager.SearchOrderLinesAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new/{Id}", Name = RouteNames.OrderLines_New)]
        public async Task<IActionResult> NewOrderLineAsync(int Id)
        {
            var viewModel = new OrderLineViewModel();

            viewModel.User = GetCurrentUser();
            return View("EditOrderLine", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.OrderLines_Edit)]
        public async Task<IActionResult> EditOrderLineAsync(int id)
        {
            var viewModel = new OrderLineViewModel();

            var OrderLineResponse = await _OrderLinesManager.GetOrderLineByIdAsync(id);

            viewModel.OrderLine = OrderLineResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditOrderLine", viewModel);
        }

        //controller method for saving OrderLine
        [HttpPost("save", Name = RouteNames.OrderLines_Save)]
        public async Task<IActionResult> SaveOrderLineAsync(SaveOrderLineRequestModel requestModel)
        {
            var responseModel = await _OrderLinesManager.SaveOrderLineAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.OrderLines_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.OrderLines_Delete)]
        public async Task<IActionResult> DeleteOrderLineAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _OrderLinesManager.DeleteOrderLineAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.OrderLines_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("incrementScannedQuantity", Name = RouteNames.OrderLines_IncrementQuantity)]
        public async Task<IActionResult> IncrementScannedQuantityAsync(SearchOrderLinesParams searchParams)
        {
            var user = GetCurrentUser();
            searchParams.UserId = user.Id;
            var responseModel = await _OrderLinesManager.IncrementOrderLineCheckedQuantityAsync(searchParams);
            return Json(responseModel);
        }

        [HttpGet("GetByOrderLine", Name = RouteNames.ScannedLine_ListByOrderLine)]
        public async Task<IActionResult> GetByOrderLine(int orderLineId)
        {
            var orderLine = await _OrderLinesManager.GetOrderLineByIdAsync(orderLineId);

            if (orderLine == null)
                return NotFound("Order line not found.");

            var scannedLines = await _ScannedLinesManager.SearchScannedLinesAsync(
                new SearchScannedLinesParams { OrderLineId = orderLineId });

            var viewModel = new ScannedLinesListViewModel(_localizer)
            {
                ScannedLines = scannedLines.Payload
            };

            return PartialView("ScannedLinesListPartial", viewModel);
        }

        [HttpGet("printLineHistoryItem", Name = RouteNames.ScannedLine_Print)]
        public async Task<IActionResult> PrintQueueAsync(int scannedLineId)
        {
            var user = GetCurrentUser();
            var orderLineHistory = await _ScannedLinesManager.GetScannedLineByIdAsync(scannedLineId);
            var orderLineId = await _OrderLinesManager.SearchArchivedOrderLinesAsync(new SearchOrderLinesParams { Id = orderLineHistory.Payload.OrderLineId });
            var dtoForPrint = new OrderLineDto
            {
                CustomerOrderNumber = orderLineId.Payload.FirstOrDefault().CustomerOrderNumber,
                RequestedQuantity = orderLineHistory.Payload.RequestedQuantity,
                CheckedQuantity = orderLineHistory.Payload.ScannedQuantity,
                TourName = orderLineId.Payload.FirstOrDefault().TourName,
                FebiArticleNo = orderLineId.Payload.FirstOrDefault().WintArticleNo
            };
            var printerModel = new PrinterModel
            {
                line = dtoForPrint,
                UserId = user.Id,
            };

            var result = await _printersManager.QueuePrintAsync(printerModel);
            return Json(result);
        }

        [HttpPost("changeQuantityManually", Name = RouteNames.OrderLines_ChangeQuantityManually)]
        public async Task<IActionResult> ChangeQuantityManually(ChangeQuantityRequestModel requestModel)
        {
            var user = GetCurrentUser();
            var result = new ResponseModelBase();
            var getLine = await _OrderLinesManager.GetOrderLineByIdAsync(requestModel.LineId.Value);
            if(getLine.Payload != null)
            {
                var saveLine = new SaveOrderLineRequestModel
                {
                    Id = getLine.Payload.Id,
                    Segment = getLine.Payload.Segment,
                    CheckedQuantity = requestModel.Quantity,
                    CustomerOrderId = getLine.Payload.CustomerOrderId,
                    DayOfWeek = getLine.Payload.DayOfWeek,
                    FebiItemId = getLine.Payload.FebiItemId,
                    LineNo = getLine.Payload.LineNo,
                    OrderDate = getLine.Payload.OrderDate,
                    PartnerCode = getLine.Payload.PartnerCode,
                    RequestedQuantity = getLine.Payload.RequestedQuantity,
                };

                result = await _OrderLinesManager.SaveOrderLineAsync(saveLine);
            }

            return Json(result);
        }

    }
}

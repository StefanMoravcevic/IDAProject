using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.OrderHeaders;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.OrderHeaders;
using IDAProject.Web.Models.Dto.OrderLines;
using IDAProject.Web.Api.Managers;
using Microsoft.AspNetCore.Authorization;
using IDAProject.Web.Models.Dto.FebiItems;
using System.Globalization;
using IDAProject.Web.Api.Hubs;
using IDAProject.Web.Api.Models.Interfaces.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHeadersController : ControllerBase
    {
        private readonly IOrderHeadersManager _OrderHeadersManager;
        private readonly IOrderLinesManager _orderLinesManager;
        private readonly IHubContext<OrderLineHub, IOrderLinesClient> _hubContext;
        private readonly IFebiItemsManager _febiItemsManager;
        private readonly UhuraAuthService _uhuraAuthService;
        private readonly UhuraMasterDataService _uhuraMasterDataService;
        private string ClientId = "clktr_3922ce9831919ef53cf98a4bab349007";
        private string ClientSecret = "YynLpmqLhWD94NfyM5IoxJjwWwdX17hW";
        private string baseUrl = "https://bis1.prod.apimanagement.eu20.hana.ondemand.com/p/v1/masterdata-api/api/v1";

        public OrderHeadersController(IOrderHeadersManager OrderHeadersManager, IOrderLinesManager orderLinesManager, IFebiItemsManager febiItemsManager, UhuraAuthService uhuraAuthService, UhuraMasterDataService uhuraMasterDataService, IHubContext<OrderLineHub, IOrderLinesClient> hubContext)
        {
            _OrderHeadersManager = OrderHeadersManager;
            _orderLinesManager = orderLinesManager;
            _febiItemsManager = febiItemsManager;
            _uhuraAuthService = uhuraAuthService;
            _uhuraMasterDataService = uhuraMasterDataService;
            _hubContext = hubContext;

        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<OrderHeaderDto>> GetOrderHeaderByIdAsync(int id)
        {
            var response = await _OrderHeadersManager.GetOrderHeaderByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteOrderHeaderAsync(int id, int? userId)
        {
            var response = await _OrderHeadersManager.DeleteOrderHeaderAsync(id, userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<OrderHeaderDto>> SearchOrderHeadersAsync(SearchOrderHeadersParams searchParams)
        {
            var response = await _OrderHeadersManager.SearchOrderHeadersAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveOrderHeaderAsync(SaveOrderHeaderRequestModel requestModel)
        {
            var response = await _OrderHeadersManager.SaveOrderHeaderAsync(requestModel);
            return response;
        }

        [HttpPost("CreateFromNavision")]
        public async Task<ResponseModel<int>> CreateOrderFromNavision([FromBody] CustomerOrderResponse navResponse)
        {
            var token = await _uhuraAuthService.GetTokenAsync(ClientId, ClientSecret);

            if (navResponse == null || navResponse.CustomerOrder == null)
                return new ResponseModel<int> { Valid = false, Message = "Invalid request" };

            var requestModel = new SaveOrderHeaderRequestModel
            {
                CustomerOrderNumber = navResponse.CustomerOrder.Header.CustomerOrderNumber,
                CreatedDate = DateTime.Now,
                DeliveryRouteCode = navResponse.CustomerOrder.Header.TransportRoute,
                PartnerCode = navResponse.CustomerOrder.Header.PartnerCode == "1002091"
                    ? "BEOGRADSKI"
                    : navResponse.CustomerOrder.Header.PartnerCode == "1002170"
                        ? "NEBEOGRADSKI"
                        : ""
            };

            var response = await _OrderHeadersManager.SaveOrderHeaderAsync(requestModel);

            var articleNumbers = navResponse.CustomerOrder.Positions
                .Select(p => p.ArticleNumber)
                .Distinct()
                .ToList();

            var masterDataArticles = await _uhuraMasterDataService.GetArticlesAsync(token, articleNumbers);
            var articleDict = masterDataArticles.ToDictionary(a => a.ArticleNumber, a => a);

            foreach (var pos in navResponse.CustomerOrder.Positions)
            {
                string? ean = null;
                string? articleName = null;
                int? packingUnit = null;
                if (articleDict.TryGetValue(pos.ArticleNumber, out var mdArticle))
                {
                    ean = mdArticle.Ean;
                    articleName = mdArticle.ArticleName;
                    packingUnit = mdArticle.DeliveryUnit;
                }


                var existingItems = await _febiItemsManager.SearchFebiItemsAsync(
                    new Web.Models.RequestModels.FebiItems.SearchFebiItemsParams { ArticleNo = pos.ArticleNumber }
                );
                var existingItem = existingItems.Payload.FirstOrDefault();

                SaveFebiItemRequestModel saveItem;

                if (existingItem != null)
                {
                    bool changed = existingItem.FebiArticleName != articleName
                                   || existingItem.FebiPackingUnit != packingUnit
                                   || existingItem.BarCode != ean;

                    if (changed)
                    {
                        saveItem = new SaveFebiItemRequestModel
                        {
                            Id = existingItem.Id,
                            FebiArticleNo = pos.ArticleNumber,
                            FebiArticleName = articleName,
                            FebiPackingUnit = packingUnit,
                            BarCode = ean,
                            WintArticleNo = pos.ExternalArticleNumber
                        };
                        await _febiItemsManager.SaveFebiItemAsync(saveItem);
                    }

                    saveItem = new SaveFebiItemRequestModel { Id = existingItem.Id };
                }
                else
                {
                    saveItem = new SaveFebiItemRequestModel
                    {
                        FebiArticleNo = pos.ArticleNumber,
                        FebiArticleName = articleName,
                        FebiPackingUnit = packingUnit,
                        BarCode = ean,
                        WintArticleNo = pos.ExternalArticleNumber
                    };
                    var savedItem = await _febiItemsManager.SaveFebiItemAsync(saveItem);
                    saveItem.Id = savedItem.Payload;
                }

                int.TryParse(pos.ExternalOrderPosition, out int lineNo);

                DateTime orderDate = DateTime.Now;
                string dayOfWeek = orderDate.ToString("dddd", new CultureInfo("sr-Latn-RS"));

                TimeSpan time = orderDate.TimeOfDay;
                string segment;
                if (time >= TimeSpan.FromHours(0) && time <= TimeSpan.FromHours(11))
                    segment = "I";
                else if (time > TimeSpan.FromHours(11) && time <= TimeSpan.FromHours(16))
                    segment = "II";
                else
                    segment = "III";

                var saveLine = new SaveOrderLineRequestModel
                {
                    LineNo = lineNo,
                    CustomerOrderId = response.Payload,
                    FebiItemId = saveItem.Id,
                    RequestedQuantity = pos.ConfirmedQuantity,
                    CheckedQuantity = 0,
                    OrderDate = orderDate,
                    DayOfWeek = dayOfWeek,
                    Segment = segment
                };

                var savedLine = await _orderLinesManager.SaveOrderLineAsync(saveLine);

                if (savedLine.Valid)
                {
                    await _hubContext.Clients.All.SearchOrderLines();
                }
            }

            return response;
        }

    }
}
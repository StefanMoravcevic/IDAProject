using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.OrderHeaders;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.OrderHeaders;
using IDAProject.Web.Api.Repositories;
using IDAProject.Web.Models.Dto.OrderHeaderArchives;
using IDAProject.Web.Models.Dto.OrderLineArchives;
using IDAProject.Web.Models.RequestModels.OrderLines;

namespace IDAProject.Web.Api.Managers
{
    public class OrderHeadersManager : IOrderHeadersManager
    {
        private readonly IOrderHeadersRepository _OrderHeadersRepository;
        private readonly IOrderLinesRepository _orderLinesRepository;
        private readonly IOrderHeaderArchivesRepository _orderHeaderArchivesRepository;
        private readonly IOrderLineArchivesRepository _orderLineArchivesRepository;
        private readonly ILogger _logger;

        public OrderHeadersManager(ILogger<OrderHeadersManager> logger, IOrderHeadersRepository OrderHeadersRepository, IOrderLinesRepository orderLinesRepository, IOrderHeaderArchivesRepository orderHeaderArchivesRepository, IOrderLineArchivesRepository orderLineArchivesRepository)
        {
            _logger = logger;
            _OrderHeadersRepository = OrderHeadersRepository;
            _orderLinesRepository = orderLinesRepository;
            _orderHeaderArchivesRepository = orderHeaderArchivesRepository;
            _orderLineArchivesRepository = orderLineArchivesRepository;
        }
        public async Task<ResponseModelList<OrderHeaderDto>> SearchOrderHeadersAsync(SearchOrderHeadersParams searchParams)
        {
            var result = new ResponseModelList<OrderHeaderDto>();
            try
            {
                result.Payload = await _OrderHeadersRepository.SearchOrderHeadersAsync(searchParams);
                result.Valid = true;
            }   
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(searchParams);
                _logger.LogError(e,$"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModel<OrderHeaderDto>> GetOrderHeaderByIdAsync(int id)
        {
            var result = new ResponseModel<OrderHeaderDto>();
            try
            {
                result.Payload = await _OrderHeadersRepository.GetOrderHeaderByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The OrderHeader  with the specified id could not be found.";
                }
                else
                {
                    result.Valid = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModelBase> DeleteOrderHeaderAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _OrderHeadersRepository.DeleteOrderHeaderAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveOrderHeaderAsync(SaveOrderHeaderRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _OrderHeadersRepository.SaveOrderHeaderAsync(requestModel);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }


        public async Task ArchiveCompletedOrdersAsync()
        {
            var orderHeaders = await _OrderHeadersRepository.SearchOrderHeadersAsync(new SearchOrderHeadersParams
            {
                IsArchived = false
            });

            foreach (var header in orderHeaders)
            {
                var orderLines = await _orderLinesRepository.SearchOrderLinesAsync(new SearchOrderLinesParams
                {
                    OrderHeaderId = header.Id
                });


                var allChecked = orderLines.All(l => l.CheckedQuantity >= l.RequestedQuantity);

                if (!allChecked)
                    continue; 

                var headerArchiveModel = new SaveOrderHeaderArchiveRequestModel
                {
                    OrderHeaderId = header.Id,
                    PartnerCode = header.PartnerCode,
                    CreatedDate = header.CreatedDate,
                    DeliveryRouteCode = header.DeliveryRouteCode
                };

                var archivedHeader = await _orderHeaderArchivesRepository.SaveOrderHeaderArchiveAsync(headerArchiveModel);

                foreach (var line in orderLines)
                {
                    var lineArchiveModel = new SaveOrderLineArchiveRequestModel
                    {
                        CustomerOrderId = archivedHeader,
                        LineNo = line.LineNo,
                        FebiItemId = line.FebiItemId,
                        RequestedQuantity = line.RequestedQuantity,
                        CheckedQuantity = line.CheckedQuantity,
                        Segment = line.Segment,
                        DayOfWeek = line.DayOfWeek,
                        OrderDate = line.OrderDate,
                        PartnerCode = line.PartnerCode
                    };

                    await _orderLineArchivesRepository.SaveOrderLineArchiveAsync(lineArchiveModel);
                }

                header.IsArchived = true;
                await _OrderHeadersRepository.SaveOrderHeaderAsync(header);
            }
        }
    }
}

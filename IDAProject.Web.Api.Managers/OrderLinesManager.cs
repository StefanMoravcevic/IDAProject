using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.OrderLines;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.OrderLines;
using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Models.Dto.ScannedLines;

namespace IDAProject.Web.Api.Managers
{
    public class OrderLinesManager : IOrderLinesManager
    {
        private readonly IOrderLinesRepository _OrderLinesRepository;
        private readonly IScannedLinesRepository _scannedLinesRepository;
        private readonly IFebiItemsRepository _febiItemsRepository;
        private readonly IPrintersManager _printersManager; 
        private readonly IDAProjectContext _dbContext;
        private readonly ILogger _logger;

        public OrderLinesManager(ILogger<OrderLinesManager> logger, IOrderLinesRepository OrderLinesRepository, IFebiItemsRepository febiItemsRepository, IDAProjectContext dbContext, IScannedLinesRepository scannedLinesRepository, IPrintersManager printersManager)
        {
            _logger = logger;
            _OrderLinesRepository = OrderLinesRepository;
            _febiItemsRepository = febiItemsRepository;
            _dbContext = dbContext;
            _scannedLinesRepository = scannedLinesRepository;
            _printersManager = printersManager;
        }
        public async Task<ResponseModelList<OrderLineDto>> SearchOrderLinesAsync(SearchOrderLinesParams searchParams)
        {
            var result = new ResponseModelList<OrderLineDto>();
            try
            {
                result.Payload = await _OrderLinesRepository.SearchOrderLinesAsync(searchParams);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(searchParams);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModel<OrderLineDto>> GetOrderLineByIdAsync(int id)
        {
            var result = new ResponseModel<OrderLineDto>();
            try
            {
                result.Payload = await _OrderLinesRepository.GetOrderLineByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The OrderLine  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteOrderLineAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _OrderLinesRepository.DeleteOrderLineAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveOrderLineAsync(SaveOrderLineRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _OrderLinesRepository.SaveOrderLineAsync(requestModel);
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

        public async Task<ResponseModel<int>> IncrementOrderLineCheckedQuantityAsync(SearchOrderLinesParams searchParams)
        {
            var result = new ResponseModel<int>();

            if (string.IsNullOrEmpty(searchParams.BarCode))
            {
                result.Valid = false;
                result.Message = "Barcode is required.";
                return result;
            }

            var febiItemResponse = await _febiItemsRepository.SearchFebiItemsAsync(
                new Web.Models.RequestModels.FebiItems.SearchFebiItemsParams
                {
                    BarCode = searchParams.BarCode,
                    ArticleNo = searchParams.BarCode
                }
            );

            var item = febiItemResponse.FirstOrDefault();
            if (item == null)
            {
                result.Valid = false;
                result.Message = "Artikal sa skeniranim bar kodom nije pronađen!";
                return result;
            }

            int febiItemId = item.Id;
            int increment = item.FebiPackingUnit ?? 1;

            var allOrderLines = (await _OrderLinesRepository.SearchOrderLinesAsync(
                new SearchOrderLinesParams
                {
                    ArticleId = febiItemId,
                    ContextOrderHeaderId = searchParams.ContextOrderHeaderId,
                    Segment = searchParams.Segment,
                    DateFrom = searchParams.DateFrom,
                    DateTo = searchParams.DateTo,
                    FebiItemId = searchParams.FebiItemId,
                    PartnerCode = searchParams.PartnerCode,
                    SelectedFilters = searchParams.SelectedFilters
                }
            ))
            .OrderBy(l => l.Id)
            .ToList();

            if (!allOrderLines.Any())
            {
                result.Valid = false;
                result.Message = "Ne postoji skenirani artikal u zadatim kriterijumima pretrage!";
                return result;
            }

            var incompleteLines = allOrderLines.Where(l => l.StatusColor != "green").ToList();

            if (!incompleteLines.Any())
            {
                result.Valid = false;
                result.Message = "Sve linije za skenirani artikal su već kompletno čekirane (green)!";
                return result;
            }

            var orderLine = incompleteLines.First();

            var newQuantity = orderLine.CheckedQuantity + increment;

            if (newQuantity > orderLine.RequestedQuantity)
            {
                result.Valid = false;
                result.Message = "Ne možete skenirati više od tražene količine.";
                return result;
            }

            if (orderLine.CheckedQuantity >= orderLine.RequestedQuantity)
            {
                result.Valid = false;
                result.Message = $"Artikal je već kompletno čekiran ({orderLine.RequestedQuantity}).";
                return result;
            }

            var saveLine = new SaveOrderLineRequestModel
            {
                Id = orderLine.Id,
                LineNo = orderLine.LineNo,
                CustomerOrderId = orderLine.CustomerOrderId,
                FebiItemId = orderLine.FebiItemId,
                RequestedQuantity = orderLine.RequestedQuantity,
                CheckedQuantity = newQuantity,
                OrderDate = orderLine.OrderDate,
                Segment = orderLine.Segment,
                DayOfWeek = orderLine.DayOfWeek,
            };

            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var saveResult = await SaveOrderLineAsync(saveLine);

                if (saveResult.Valid)
                {
                    var lineForPrint = await _OrderLinesRepository.GetOrderLineByIdAsync(saveResult.Payload);
                    await _printersManager.QueuePrintAsync(lineForPrint, searchParams.UserId.Value);

                    var scanHistory = new SaveScannedLineRequestModel
                    {
                        OrderLineId = orderLine.Id,
                        ScannedQuantity = newQuantity,
                        RequestedQuantity = orderLine.RequestedQuantity,
                        Date = DateTime.Now,
                        UserId = searchParams.UserId
                    };

                    await _scannedLinesRepository.SaveScannedLineAsync(scanHistory);

                    await transaction.CommitAsync();

                    result.Valid = true;
                    result.Message = $"Količina uspešno povećana na {newQuantity}.";
                    result.Payload = saveResult.Payload;
                }
                else
                {
                    await transaction.RollbackAsync();
                    result.Valid = false;
                    result.Message = "Greška pri snimanju stavke.";
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                result.Valid = false;
                result.Message = "Greška prilikom obrade skeniranja: " + ex.Message;
            }

            return result;
        }

        public async Task<ResponseModelList<OrderLineDto>> SearchArchivedOrderLinesAsync(SearchOrderLinesParams searchParams)
        {
            var result = new ResponseModelList<OrderLineDto>();
            try
            {
                result.Payload = await _OrderLinesRepository.SearchArchivedOrderLinesAsync(searchParams);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(searchParams);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }
    }
}
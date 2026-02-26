using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.OrderLineArchives;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.OrderLineArchives;

namespace IDAProject.Web.Api.Managers
{
    public class OrderLineArchivesManager : IOrderLineArchivesManager
    {
        private readonly IOrderLineArchivesRepository _OrderLineArchivesRepository;
        private readonly ILogger _logger;

        public OrderLineArchivesManager(ILogger<OrderLineArchivesManager> logger, IOrderLineArchivesRepository OrderLineArchivesRepository)
        {
            _logger = logger;
            _OrderLineArchivesRepository = OrderLineArchivesRepository;
        }
        public async Task<ResponseModelList<OrderLineArchiveDto>> SearchOrderLineArchivesAsync(SearchOrderLineArchivesParams searchParams)
        {
            var result = new ResponseModelList<OrderLineArchiveDto>();
            try
            {
                result.Payload = await _OrderLineArchivesRepository.SearchOrderLineArchivesAsync(searchParams);
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

        public async Task<ResponseModel<OrderLineArchiveDto>> GetOrderLineArchiveByIdAsync(int id)
        {
            var result = new ResponseModel<OrderLineArchiveDto>();
            try
            {
                result.Payload = await _OrderLineArchivesRepository.GetOrderLineArchiveByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The OrderLineArchive  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteOrderLineArchiveAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _OrderLineArchivesRepository.DeleteOrderLineArchiveAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveOrderLineArchiveAsync(SaveOrderLineArchiveRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _OrderLineArchivesRepository.SaveOrderLineArchiveAsync(requestModel);
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
    }
}

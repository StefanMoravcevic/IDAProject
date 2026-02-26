using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.OrderHeaderArchives;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.OrderHeaderArchives;

namespace IDAProject.Web.Api.Managers
{
    public class OrderHeaderArchivesManager : IOrderHeaderArchivesManager
    {
        private readonly IOrderHeaderArchivesRepository _OrderHeaderArchivesRepository;
        private readonly ILogger _logger;

        public OrderHeaderArchivesManager(ILogger<OrderHeaderArchivesManager> logger, IOrderHeaderArchivesRepository OrderHeaderArchivesRepository)
        {
            _logger = logger;
            _OrderHeaderArchivesRepository = OrderHeaderArchivesRepository;
        }
        public async Task<ResponseModelList<OrderHeaderArchiveDto>> SearchOrderHeaderArchivesAsync(SearchOrderHeaderArchivesParams searchParams)
        {
            var result = new ResponseModelList<OrderHeaderArchiveDto>();
            try
            {
                result.Payload = await _OrderHeaderArchivesRepository.SearchOrderHeaderArchivesAsync(searchParams);
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

        public async Task<ResponseModel<OrderHeaderArchiveDto>> GetOrderHeaderArchiveByIdAsync(int id)
        {
            var result = new ResponseModel<OrderHeaderArchiveDto>();
            try
            {
                result.Payload = await _OrderHeaderArchivesRepository.GetOrderHeaderArchiveByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The OrderHeaderArchive  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteOrderHeaderArchiveAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _OrderHeaderArchivesRepository.DeleteOrderHeaderArchiveAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveOrderHeaderArchiveAsync(SaveOrderHeaderArchiveRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _OrderHeaderArchivesRepository.SaveOrderHeaderArchiveAsync(requestModel);
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

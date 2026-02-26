using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.Periods;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Periods;

namespace IDAProject.Web.Api.Managers
{
    public class PeriodsManager : IPeriodsManager
    {
        private readonly IPeriodsRepository _PeriodsRepository;
        private readonly ILogger _logger;

        public PeriodsManager(ILogger<PeriodsManager> logger, IPeriodsRepository PeriodsRepository)
        {
            _logger = logger;
            _PeriodsRepository = PeriodsRepository;
        }
        public async Task<ResponseModelList<PeriodDto>> SearchPeriodsAsync(SearchPeriodsParams searchParams)
        {
            var result = new ResponseModelList<PeriodDto>();
            try
            {
                result.Payload = await _PeriodsRepository.SearchPeriodsAsync(searchParams);
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

        public async Task<ResponseModel<PeriodDto>> GetPeriodByIdAsync(int id)
        {
            var result = new ResponseModel<PeriodDto>();
            try
            {
                result.Payload = await _PeriodsRepository.GetPeriodByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The Period  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeletePeriodAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _PeriodsRepository.DeletePeriodAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SavePeriodAsync(SavePeriodRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _PeriodsRepository.SavePeriodAsync(requestModel);
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

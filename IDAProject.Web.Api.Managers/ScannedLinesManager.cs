using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.ScannedLines;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.ScannedLines;

namespace IDAProject.Web.Api.Managers
{
    public class ScannedLinesManager : IScannedLinesManager
    {
        private readonly IScannedLinesRepository _ScannedLinesRepository;
        private readonly ILogger _logger;

        public ScannedLinesManager(ILogger<ScannedLinesManager> logger, IScannedLinesRepository ScannedLinesRepository)
        {
            _logger = logger;
            _ScannedLinesRepository = ScannedLinesRepository;
        }
        public async Task<ResponseModelList<ScannedLineDto>> SearchScannedLinesAsync(SearchScannedLinesParams searchParams)
        {
            var result = new ResponseModelList<ScannedLineDto>();
            try
            {
                result.Payload = await _ScannedLinesRepository.SearchScannedLinesAsync(searchParams);
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

        public async Task<ResponseModel<ScannedLineDto>> GetScannedLineByIdAsync(int id)
        {
            var result = new ResponseModel<ScannedLineDto>();
            try
            {
                result.Payload = await _ScannedLinesRepository.GetScannedLineByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The ScannedLine  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteScannedLineAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _ScannedLinesRepository.DeleteScannedLineAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveScannedLineAsync(SaveScannedLineRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _ScannedLinesRepository.SaveScannedLineAsync(requestModel);
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

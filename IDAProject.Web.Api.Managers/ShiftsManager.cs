using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.Shifts;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Shifts;

namespace IDAProject.Web.Api.Managers
{
    public class ShiftsManager : IShiftsManager
    {
        private readonly IShiftsRepository _ShiftsRepository;
        private readonly ILogger _logger;

        public ShiftsManager(ILogger<ShiftsManager> logger, IShiftsRepository ShiftsRepository)
        {
            _logger = logger;
            _ShiftsRepository = ShiftsRepository;
        }
        public async Task<ResponseModelList<ShiftDto>> SearchShiftsAsync(SearchShiftsParams searchParams)
        {
            var result = new ResponseModelList<ShiftDto>();
            try
            {
                result.Payload = await _ShiftsRepository.SearchShiftsAsync(searchParams);
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

        public async Task<ResponseModel<ShiftDto>> GetShiftByIdAsync(int id)
        {
            var result = new ResponseModel<ShiftDto>();
            try
            {
                result.Payload = await _ShiftsRepository.GetShiftByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The Shift  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteShiftAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _ShiftsRepository.DeleteShiftAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveShiftAsync(SaveShiftRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _ShiftsRepository.SaveShiftAsync(requestModel);
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

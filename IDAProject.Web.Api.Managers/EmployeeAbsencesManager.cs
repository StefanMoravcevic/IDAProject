using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.EmployeeAbsences;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.EmployeeAbsences;

namespace IDAProject.Web.Api.Managers
{
    public class EmployeeAbsencesManager : IEmployeeAbsencesManager
    {
        private readonly IEmployeeAbsencesRepository _EmployeeAbsencesRepository;
        private readonly ILogger _logger;

        public EmployeeAbsencesManager(ILogger<EmployeeAbsencesManager> logger, IEmployeeAbsencesRepository EmployeeAbsencesRepository)
        {
            _logger = logger;
            _EmployeeAbsencesRepository = EmployeeAbsencesRepository;
        }
        public async Task<ResponseModelList<EmployeeAbsenceDto>> SearchEmployeeAbsencesAsync(SearchEmployeeAbsencesParams searchParams)
        {
            var result = new ResponseModelList<EmployeeAbsenceDto>();
            try
            {
                result.Payload = await _EmployeeAbsencesRepository.SearchEmployeeAbsencesAsync(searchParams);
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

        public async Task<ResponseModel<EmployeeAbsenceDto>> GetEmployeeAbsenceByIdAsync(int id)
        {
            var result = new ResponseModel<EmployeeAbsenceDto>();
            try
            {
                result.Payload = await _EmployeeAbsencesRepository.GetEmployeeAbsenceByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The EmployeeAbsence  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteEmployeeAbsenceAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _EmployeeAbsencesRepository.DeleteEmployeeAbsenceAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveEmployeeAbsenceAsync(SaveEmployeeAbsenceRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _EmployeeAbsencesRepository.SaveEmployeeAbsenceAsync(requestModel);
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

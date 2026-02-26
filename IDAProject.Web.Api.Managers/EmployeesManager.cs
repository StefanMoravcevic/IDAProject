using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Api.Repositories;
using IDAProject.Web.Models.Dto.Contacts;
using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.Dto.Users;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Employees;
using IDAProject.Web.Models.RequestModels.Users;
using System.ComponentModel.Design;

namespace IDAProject.Web.Api.Managers
{
    public class EmployeesManager : IEmployeesManager
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly ILogger _logger;

        public EmployeesManager(ILogger<EmployeesManager> logger, IEmployeesRepository employeesRepository)
        {
            _logger = logger;
            _employeesRepository = employeesRepository;
        }

        public async Task<ResponseModelList<EmployeeDto>> SearchEmployeesAsync(SearchEmployeesParams searchParams)
        {
            var result = new ResponseModelList<EmployeeDto>();
            try
            {
                result.Payload = await _employeesRepository.SearchEmployeesAsync(searchParams);
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

        public async Task<ResponseModel<EmployeeDto>> GetEmployeeByIdAsync(int id)
        {
            var result = new ResponseModel<EmployeeDto>();
            try
            {
                result.Payload = await _employeesRepository.GetEmployeeByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The employee with the specified id could not be found.";
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

        public async Task<ResponseModel<EmployeeSearchResponseModel>> GetEmailPhoneByEmployeeIdAsync(int id)
        {
            var result = new ResponseModel<EmployeeSearchResponseModel>();
            try
            {
                result.Payload = await _employeesRepository.GetEmailPhoneByEmployeeIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The employee with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteEmployeeAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _employeesRepository.DeleteEmployeeAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveEmployeeAsync(SaveEmployeeRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _employeesRepository.SaveEmployeeAsync(requestModel);
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


		public async Task<ResponseModel<EmployeeDto>> GetNextRowAsync(int currentId)
		{
			var result = new ResponseModel<EmployeeDto>();
			try
			{
				var nextEmployee = await _employeesRepository.GetNextEmployeeAsync(currentId);

				if (nextEmployee == null)
				{
					result.Message = "The next employee could not be found.";
				}
				else
				{
					result.Payload = nextEmployee;
					result.Valid = true;
				}
			}
			catch (Exception e)
			{
				result.Message = e.Message;
				_logger.LogError(e, $"currentId: {currentId}");
			}

			return result;
		}

		public async Task<ResponseModel<EmployeeDto>> GetPreviousRowAsync(int currentId)
		{
			var result = new ResponseModel<EmployeeDto>();
			try
			{
				var previousEmployee = await _employeesRepository.GetPreviousEmployeeAsync(currentId);

				if (previousEmployee == null)
				{
					result.Message = "The previous employee could not be found.";
				}
				else
				{
					result.Payload = previousEmployee;
					result.Valid = true;
				}
			}
			catch (Exception e)
			{
				result.Message = e.Message;
				_logger.LogError(e, $"currentId: {currentId}");
			}

			return result;
		}


    }
}
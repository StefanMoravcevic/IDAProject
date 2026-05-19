using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.ProjectEmployees;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.ProjectEmployees;

namespace IDAProject.Web.Api.Managers
{
    public class ProjectEmployeesManager : IProjectEmployeesManager
    {
        private readonly IProjectEmployeesRepository _ProjectEmployeesRepository;
        private readonly ILogger _logger;

        public ProjectEmployeesManager(ILogger<ProjectEmployeesManager> logger, IProjectEmployeesRepository ProjectEmployeesRepository)
        {
            _logger = logger;
            _ProjectEmployeesRepository = ProjectEmployeesRepository;
        }
        public async Task<ResponseModelList<ProjectEmployeeDto>> SearchProjectEmployeesAsync(SearchProjectEmployeesParams searchParams)
        {
            var result = new ResponseModelList<ProjectEmployeeDto>();
            try
            {
                result.Payload = await _ProjectEmployeesRepository.SearchProjectEmployeesAsync(searchParams);
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

        public async Task<ResponseModel<ProjectEmployeeDto>> GetProjectEmployeeByIdAsync(int id)
        {
            var result = new ResponseModel<ProjectEmployeeDto>();
            try
            {
                result.Payload = await _ProjectEmployeesRepository.GetProjectEmployeeByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The ProjectEmployee  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteProjectEmployeeAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _ProjectEmployeesRepository.DeleteProjectEmployeeAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveProjectEmployeeAsync(SaveProjectEmployeeRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _ProjectEmployeesRepository.SaveProjectEmployeeAsync(requestModel);
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

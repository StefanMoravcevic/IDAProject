using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.Projects;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Projects;

namespace IDAProject.Web.Api.Managers
{
    public class ProjectsManager : IProjectsManager
    {
        private readonly IProjectsRepository _ProjectsRepository;
        private readonly ILogger _logger;

        public ProjectsManager(ILogger<ProjectsManager> logger, IProjectsRepository ProjectsRepository)
        {
            _logger = logger;
            _ProjectsRepository = ProjectsRepository;
        }
        public async Task<ResponseModelList<ProjectDto>> SearchProjectsAsync(SearchProjectsParams searchParams)
        {
            var result = new ResponseModelList<ProjectDto>();
            try
            {
                result.Payload = await _ProjectsRepository.SearchProjectsAsync(searchParams);
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

        public async Task<ResponseModel<ProjectDto>> GetProjectByIdAsync(int id)
        {
            var result = new ResponseModel<ProjectDto>();
            try
            {
                result.Payload = await _ProjectsRepository.GetProjectByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The Project  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteProjectAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _ProjectsRepository.DeleteProjectAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveProjectAsync(SaveProjectRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _ProjectsRepository.SaveProjectAsync(requestModel);
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

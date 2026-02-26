using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Roles;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Roles;

namespace IDAProject.Web.Api.Managers
{
    public class RolesManager : IRolesManager
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly ILogger _logger;

        public RolesManager(ILogger<RolesManager> logger, IRolesRepository rolesRepository)
        {
            _logger = logger;
            _rolesRepository = rolesRepository;
        }


        public async Task<ResponseModel<RoleDto>> GetRoleByIdAsync(int id)
        {
            var result = new ResponseModel<RoleDto>();
            try
            {
                result.Payload = await _rolesRepository.GetRoleByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The role with the specified id could not be found.";
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

        public async Task<ResponseModel<int>> SaveRoleAsync(SaveRoleRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _rolesRepository.SaveRoleAsync(requestModel);
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

        public async Task<ResponseModelList<RoleDto>> SearchRolesAsync(SearchRolesParams searchParams)
        {
            var result = new ResponseModelList<RoleDto>();
            try
            {
                result.Payload = await _rolesRepository.SearchRolesAsync(searchParams);
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

        public async Task<ResponseModelBase> DeleteRoleAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _rolesRepository.DeleteRoleAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }
        public async Task<ResponseModelBase> CreateRoleFeatureAsync(CreateRoleFeatureModel requestModel)
        {
            var result = new ResponseModelBase();
            try
            {
                var roleFeatureExists = await _rolesRepository.SearchRoleFeatureAsync(requestModel);
                if (roleFeatureExists)
                {
                    throw new Exception("This feature has been already assigned to role.");
                }
                else
                {
                    await _rolesRepository.CreateRoleFeatureAsync(requestModel);
                    //result.Payload = await _usersRepository.GetRolesByUserIdAsync(requestModel.UserId);
                    result.Valid = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }
        public async Task<ResponseModelBase> DeleteRoleFeatureAsync(int roleFeatureId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _rolesRepository.DeleteRoleFeatureAsync(roleFeatureId);
                result.Valid = true;
                //result.Payload = await _usersRepository.SearchUsersAsync(new SearchUsersParams());
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                //var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"user role id: {roleFeatureId}");
            }
            return result;
        }
        public async Task<ResponseModelList<RoleFeatureDto>> GetRoleFeaturesByRoleIdAsync(int id)
        {
            var result = new ResponseModelList<RoleFeatureDto>();
            try
            {
                result.Payload = await _rolesRepository.GetRoleFeaturesByRoleIdAsync(id);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(id);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }
    }
}
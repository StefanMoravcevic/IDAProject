using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.UserNotifications;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.UserNotifications;

namespace IDAProject.Web.Api.Managers
{
    public class UserNotificationsManager : IUserNotificationsManager
    {
        private readonly IUserNotificationsRepository _UserNotificationsRepository;
        private readonly ILogger _logger;

        public UserNotificationsManager(ILogger<UserNotificationsManager> logger, IUserNotificationsRepository UserNotificationsRepository)
        {
            _logger = logger;
            _UserNotificationsRepository = UserNotificationsRepository;
        }
        public async Task<ResponseModelList<UserNotificationDto>> SearchUserNotificationsAsync(SearchUserNotificationsParams searchParams)
        {
            var result = new ResponseModelList<UserNotificationDto>();
            try
            {
                result.Payload = await _UserNotificationsRepository.SearchUserNotificationsAsync(searchParams);
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

        public async Task<ResponseModel<UserNotificationDto>> GetUserNotificationByIdAsync(int id)
        {
            var result = new ResponseModel<UserNotificationDto>();
            try
            {
                result.Payload = await _UserNotificationsRepository.GetUserNotificationByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The UserNotification  with the specified id could not be found.";
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

        public async Task<ResponseModelBase> DeleteUserNotificationAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _UserNotificationsRepository.DeleteUserNotificationAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveUserNotificationAsync(SaveUserNotificationRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _UserNotificationsRepository.SaveUserNotificationAsync(requestModel);
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

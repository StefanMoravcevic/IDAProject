using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.UserNotifications;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.UserNotifications;

namespace IDAProject.Web.Admin.Managers
{
    public class UserNotificationsManager : BaseManager, IUserNotificationsManager
    {
        public UserNotificationsManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<UserNotificationsManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<UserNotificationDto>> SearchUserNotificationsAsync(SearchUserNotificationsParams searchParams)
        {
            var result =
                await PostAsync<SearchUserNotificationsParams, ResponseModelList<UserNotificationDto>>($"api/UserNotifications/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<UserNotificationDto>> GetUserNotificationByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<UserNotificationDto>>($"api/UserNotifications/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteUserNotificationAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/UserNotifications/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveUserNotificationAsync(SaveUserNotificationRequestModel requestModel)
        {
            var result = await PostAsync<SaveUserNotificationRequestModel, ResponseModel<int>>($"api/UserNotifications", requestModel);
            return result;
        }
    }
}

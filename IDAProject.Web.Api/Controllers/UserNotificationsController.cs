using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.UserNotifications;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.UserNotifications;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserNotificationsController : ControllerBase
    {
        private readonly IUserNotificationsManager _UserNotificationsManager;

        public UserNotificationsController(IUserNotificationsManager UserNotificationsManager)
        {
            _UserNotificationsManager = UserNotificationsManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<UserNotificationDto>> GetUserNotificationByIdAsync(int id)
        {
            var response = await _UserNotificationsManager.GetUserNotificationByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteUserNotificationAsync(int id, int? userId)
        {
            var response = await _UserNotificationsManager.DeleteUserNotificationAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<UserNotificationDto>> SearchUserNotificationsAsync(SearchUserNotificationsParams searchParams)
        {
            var response = await _UserNotificationsManager.SearchUserNotificationsAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveUserNotificationAsync(SaveUserNotificationRequestModel requestModel)
        {
            var response = await _UserNotificationsManager.SaveUserNotificationAsync(requestModel);
            return response;
        }
    }
}

using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.UserNotifications;
using IDAProject.Web.Models.RequestModels.UserNotifications;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IUserNotificationsManager
    {
        Task<ResponseModelList<UserNotificationDto>> SearchUserNotificationsAsync(SearchUserNotificationsParams searchParams);
        Task<ResponseModel<UserNotificationDto>> GetUserNotificationByIdAsync(int id);
        Task<ResponseModelBase> DeleteUserNotificationAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveUserNotificationAsync(SaveUserNotificationRequestModel requestModel);
    }
}

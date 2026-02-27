
using IDAProject.Web.Models.Dto.UserNotifications;
using IDAProject.Web.Models.RequestModels.UserNotifications;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IUserNotificationsRepository
    {
        Task<UserNotificationDto> GetUserNotificationByIdAsync(int id);
        Task<int> SaveUserNotificationAsync(SaveUserNotificationRequestModel requestModel);
        Task<List<UserNotificationDto>> SearchUserNotificationsAsync(SearchUserNotificationsParams searchParams);
        Task DeleteUserNotificationAsync(int id, int? userId);
    }
}


namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface INotificationsRepository
    {
        Task SaveNotificationRecordAsync(int type, int referenceId, int maxRetryCount);
    }
}

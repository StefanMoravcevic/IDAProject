using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;

namespace IDAProject.Web.Api.Repositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly IdaContext _dbContext;

        public NotificationsRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task SaveNotificationRecordAsync(int type, int referenceId, int maxRetryCount)
        {
            var dbRecord = _dbContext.CronNotifications.FirstOrDefault(x => x.ReferenceRecordId == referenceId && x.NotificationTypeId == type);

            if (dbRecord == null)
            {
                dbRecord = new CronNotification
                {
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    NotificationTypeId = type,
                    ReferenceRecordId = referenceId,
                    RetryCount = 1
                };
                _dbContext.CronNotifications.Add(dbRecord!);
            }
            else
            {
                dbRecord.RetryCount++;
            }

            if(dbRecord.RetryCount >= maxRetryCount)
            {
                dbRecord.FinishedDate = DateTime.Now;
            }

            await _dbContext.SaveChangesAsync();
        }

    }
}
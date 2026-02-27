using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.UserNotifications;
using IDAProject.Web.Models.RequestModels.UserNotifications;

namespace IDAProject.Web.Api.Repositories
{
    public class UserNotificationsRepository : IUserNotificationsRepository
    {
        private readonly IdaContext _dbContext;

        public UserNotificationsRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserNotificationDto> GetUserNotificationByIdAsync(int id)
        {
            var searchParams = new SearchUserNotificationsParams
            {
                Id = id
            };
            var result = await SearchUserNotificationsAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<UserNotificationDto>> SearchUserNotificationsAsync(SearchUserNotificationsParams searchParams)
        {

            var result = new List<UserNotificationDto>();
            IQueryable<UserNotification> query = _dbContext.UserNotifications.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
 
            }

            result = await query.Select(a => new UserNotificationDto
            {
                Id = a.Id,
                SectorId = a.SectorId,
                DateFrom = a.DateFrom,
                DateTo = a.DateTo,
                ForAllUsers = a.ForAllUsers,
                Note = a.Note,
                Sector = a.Sector.Name

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveUserNotificationAsync(SaveUserNotificationRequestModel requestModel)
        {
            UserNotification? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.UserNotifications.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveUserNotificationRequestModel,UserNotification>(requestModel);
                _dbContext.UserNotifications.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteUserNotificationAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.UserNotifications.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.UserNotifications.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    
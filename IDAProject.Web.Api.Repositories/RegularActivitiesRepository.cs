using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.RegularActivities;
using IDAProject.Web.Models.RequestModels.RegularActivities;

namespace IDAProject.Web.Api.Repositories
{
    public class RegularActivitiesRepository : IRegularActivitiesRepository
    {
        private readonly IdaContext _dbContext;

        public RegularActivitiesRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RegularActivityDto> GetRegularActivityByIdAsync(int id)
        {
            var searchParams = new SearchRegularActivitiesParams
            {
                Id = id
            };
            var result = await SearchRegularActivitiesAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<RegularActivityDto>> SearchRegularActivitiesAsync(SearchRegularActivitiesParams searchParams)
        {

            var result = new List<RegularActivityDto>();
            IQueryable<RegularActivity> query = _dbContext.RegularActivities.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {  

            }

            result = await query.Select(a => new RegularActivityDto
            {
                Id = a.Id,
                Description = a.Description,
                Name = a.Name

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveRegularActivityAsync(SaveRegularActivityRequestModel requestModel)
        {
            RegularActivity? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.RegularActivities.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveRegularActivityRequestModel,RegularActivity>(requestModel);
                _dbContext.RegularActivities.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteRegularActivityAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.RegularActivities.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.RegularActivities.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    
using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.Periods;
using IDAProject.Web.Models.RequestModels.Periods;

namespace IDAProject.Web.Api.Repositories
{
    public class PeriodsRepository : IPeriodsRepository
    {
        private readonly IDAProjectContext _dbContext;

        public PeriodsRepository(IDAProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PeriodDto> GetPeriodByIdAsync(int id)
        {
            var searchParams = new SearchPeriodsParams
            {
                Id = id
            };
            var result = await SearchPeriodsAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<PeriodDto>> SearchPeriodsAsync(SearchPeriodsParams searchParams)
        {

            var result = new List<PeriodDto>();
            IQueryable<Period> query = _dbContext.Periods.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
            }

            result = await query.Select(a => new PeriodDto
            {
                Id = a.Id,
                Code = a.Code,
                Name = a.Name,
                DateFrom = a.DateFrom,
                DateTo = a.DateTo

            }).ToListAsync();
            return result;

        }

        public async Task<int> SavePeriodAsync(SavePeriodRequestModel requestModel)
        {
            Period? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.Periods.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SavePeriodRequestModel,Period>(requestModel);
                _dbContext.Periods.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeletePeriodAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.Periods.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.Periods.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    
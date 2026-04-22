using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.Shifts;
using IDAProject.Web.Models.RequestModels.Shifts;

namespace IDAProject.Web.Api.Repositories
{
    public class ShiftsRepository : IShiftsRepository
    {
        private readonly IdaContext _dbContext;

        public ShiftsRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShiftDto> GetShiftByIdAsync(int id)
        {
            var searchParams = new SearchShiftsParams
            {
                Id = id
            };
            var result = await SearchShiftsAsync(searchParams);
            return result.FirstOrDefault();
        }
        
        public async Task<List<ShiftDto>> SearchShiftsAsync(SearchShiftsParams searchParams)
        {

            var result = new List<ShiftDto>();
            IQueryable<Shift> query = _dbContext.Shifts.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
  
            }

            result = await query.Select(a => new ShiftDto
            {
                Id = a.Id,
                ShiftNo = a.ShiftNo,
                TimeFrom = a.TimeFrom,
                TimeTo = a.TimeTo

            }).ToListAsync();   
            return result;

        }

        public async Task<int> SaveShiftAsync(SaveShiftRequestModel requestModel)
        {
            Shift? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.Shifts.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveShiftRequestModel,Shift>(requestModel);
                _dbContext.Shifts.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteShiftAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.Shifts.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.Shifts.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    
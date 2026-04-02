using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.EmployeeViewTrackings;
using IDAProject.Web.Models.RequestModels.EmployeeViewTrackings;

namespace IDAProject.Web.Api.Repositories
{
    public class EmployeeViewTrackingsRepository : IEmployeeViewTrackingsRepository
    {
        private readonly IdaContext _dbContext;

        public EmployeeViewTrackingsRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EmployeeViewTrackingDto> GetEmployeeViewTrackingByIdAsync(int id)
        {
            var searchParams = new SearchEmployeeViewTrackingsParams
            {
                Id = id
            };
            var result = await SearchEmployeeViewTrackingsAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<EmployeeViewTrackingDto>> SearchEmployeeViewTrackingsAsync(SearchEmployeeViewTrackingsParams searchParams)
        {

            var result = new List<EmployeeViewTrackingDto>();
            IQueryable<EmployeeViewTracking> query = _dbContext.EmployeeViewTrackings.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.EmployeeId.HasValue)
                {
                    query = query.Where(x => x.ViewerEmployeeId == searchParams.EmployeeId);
                }
                if (searchParams.IsBookmarked.HasValue)
                {
                    query = query.Where(x => x.IsBookmarked == searchParams.IsBookmarked);
                }
                if (searchParams.HideFromHomePage.HasValue)
                {
                    query = query.Where(x => x.HideFromHomePage == searchParams.HideFromHomePage);
                }
                if (searchParams.Date.HasValue)
                {
                    query = query.Where(x => x.ViewedFrom.Value.Date == searchParams.Date.Value.Date);
                }
            }

            result = await query.Select(a => new EmployeeViewTrackingDto
            {
                Id = a.Id,
                ViewedEmployeeId = a.ViewedEmployeeId,
                ViewedFrom = a.ViewedFrom,
                ViewedUntil = a.ViewedUntil,
                ViewerEmployeeId = a.ViewerEmployeeId,
                ViewedEmployee = a.ViewedEmployee.Name + " " + a.ViewedEmployee.Surname,
                IsBookmarked = a.IsBookmarked,
                HideFromHomePage  = a.HideFromHomePage

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveEmployeeViewTrackingAsync(SaveEmployeeViewTrackingRequestModel requestModel)
        {
            EmployeeViewTracking? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.EmployeeViewTrackings.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveEmployeeViewTrackingRequestModel,EmployeeViewTracking>(requestModel);
                _dbContext.EmployeeViewTrackings.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteEmployeeViewTrackingAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.EmployeeViewTrackings.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.EmployeeViewTrackings.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
        
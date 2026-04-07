using System.Linq;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.EmployeeViewTrackings;
using IDAProject.Web.Models.RequestModels.EmployeeViewTrackings;
using Microsoft.EntityFrameworkCore;

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

        private DateTime GetNextWorkingDay()
        {
            var today = DateTime.Today;
            var nextDay = today.AddDays(1);

            if (today.DayOfWeek == DayOfWeek.Friday)
                nextDay = today.AddDays(3);
            else if (today.DayOfWeek == DayOfWeek.Saturday)
                nextDay = today.AddDays(2);

            return nextDay;
        }

        public async Task<List<EmployeeViewTrackingDto>> GetBookmarkedEmployeesWithoutPlanNextWorkingDayAsync(List<int> bookmarkedEmployeeIds)
        {
            if (bookmarkedEmployeeIds == null || !bookmarkedEmployeeIds.Any())
                return new List<EmployeeViewTrackingDto>();

            var nextWorkingDay = GetNextWorkingDay();

            // 1?? Prvo u?itamo iz baze sve zapise koji zadovoljavaju filter
            var employeeTrackings = await _dbContext.EmployeeViewTrackings
                .Where(ev => bookmarkedEmployeeIds.Contains(ev.ViewedEmployeeId.Value) && !ev.IsDeleted)
                .Where(ev => !_dbContext.TasksPlannings
                    .Any(tp => tp.User.EmployeeId == ev.ViewedEmployeeId
                               && tp.PlanDate.HasValue
                               && tp.PlanDate.Value.Date == nextWorkingDay
                               && !tp.IsDeleted))
                .Include(ev => ev.ViewedEmployee) // obavezno Include za navigaciono svojstvo
                .ToListAsync();

            // 2?? Grupisanje po ViewedEmployeeId da uklonimo duplikate
            var distinctEmployees = employeeTrackings
                .GroupBy(ev => ev.ViewedEmployeeId)
                .Select(g => g.First())
                .ToList();

            // 3?? Mapiranje u DTO
            var result = distinctEmployees.Select(a => new EmployeeViewTrackingDto
            {
                Id = a.Id,
                ViewedEmployeeId = a.ViewedEmployeeId,
                ViewedFrom = a.ViewedFrom,
                ViewedUntil = a.ViewedUntil,
                ViewerEmployeeId = a.ViewerEmployeeId,
                ViewedEmployee = a.ViewedEmployee.Name + " " + a.ViewedEmployee.Surname,
                IsBookmarked = a.IsBookmarked,
                HideFromHomePage = a.HideFromHomePage
            }).ToList();

            return result;
        }

    }
}
        
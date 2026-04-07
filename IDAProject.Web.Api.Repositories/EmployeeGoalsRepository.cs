using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.EmployeeGoals;
using IDAProject.Web.Models.RequestModels.EmployeeGoals;
using IDAProject.Web.Db.MainDatabase;

namespace IDAProject.Web.Api.Repositories
{
    public class EmployeeGoalsRepository : IEmployeeGoalsRepository
    {
        private readonly IdaContext _dbContext;

        public EmployeeGoalsRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EmployeeGoalDto> GetEmployeeGoalByIdAsync(int id)
        {
            var searchParams = new SearchEmployeeGoalsParams
            {
                Id = id
            };
            var result = await SearchEmployeeGoalsAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<EmployeeGoalDto>> SearchEmployeeGoalsAsync(SearchEmployeeGoalsParams searchParams)
        {

            var result = new List<EmployeeGoalDto>();
            IQueryable<EmployeeGoal> query = _dbContext.EmployeeGoals.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.EmployeeId.HasValue)
                {
                    query = query.Where(x => x.EmployeeId == searchParams.EmployeeId);
                }
                if (searchParams.YearId.HasValue)
                {
                    query = query.Where(x => x.YearId == searchParams.YearId);
                }

                if (searchParams.IsActive.HasValue)
                {
                    query = query.Where(x => x.Year.IsActive == searchParams.IsActive);
                }
            }

            result = await query.Select(a => new EmployeeGoalDto
            {
                Id = a.Id,
                EmployeeId = a.EmployeeId,
                Goal = a.Goal,
                YearId = a.YearId,
                Employee = a.Employee.Name + " " + a.Employee.Surname,
                Year = a.Year.Year1

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveEmployeeGoalAsync(SaveEmployeeGoalRequestModel requestModel)
        {
            EmployeeGoal? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.EmployeeGoals.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveEmployeeGoalRequestModel,EmployeeGoal>(requestModel);
                _dbContext.EmployeeGoals.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteEmployeeGoalAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.EmployeeGoals.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.EmployeeGoals.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    
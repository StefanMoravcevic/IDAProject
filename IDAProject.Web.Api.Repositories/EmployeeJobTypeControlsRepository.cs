using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.EmployeeJobTypeControls;
using IDAProject.Web.Models.RequestModels.EmployeeJobTypeControls;
using IDAProject.Web.Db.MainDatabase;

namespace IDAProject.Web.Api.Repositories
{
    public class EmployeeJobTypeControlsRepository : IEmployeeJobTypeControlsRepository
    {
        private readonly IdaContext _dbContext;

        public EmployeeJobTypeControlsRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EmployeeJobTypeControlDto> GetEmployeeJobTypeControlByIdAsync(int id)
        {
            var searchParams = new SearchEmployeeJobTypeControlsParams
            {
                Id = id
            };
            var result = await SearchEmployeeJobTypeControlsAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<EmployeeJobTypeControlDto>> SearchEmployeeJobTypeControlsAsync(SearchEmployeeJobTypeControlsParams searchParams)
        {

            var result = new List<EmployeeJobTypeControlDto>();
            IQueryable<EmployeeJobTypeControl> query = _dbContext.EmployeeJobTypeControls.Where(x => x.IsDeleted == false);
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
            }

            result = await query.Select(a => new EmployeeJobTypeControlDto
            {

                Id = a.Id,
                EmployeeId = a.EmployeeId,
                JobTypeId = a.JobTypeId,
                Employee = a.Employee.Name + " " + a.Employee.Surname,
                JobType = a.JobType.Name

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveEmployeeJobTypeControlAsync(SaveEmployeeJobTypeControlRequestModel requestModel)
        {
            EmployeeJobTypeControl? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.EmployeeJobTypeControls.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveEmployeeJobTypeControlRequestModel,EmployeeJobTypeControl>(requestModel);
                _dbContext.EmployeeJobTypeControls.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteEmployeeJobTypeControlAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.EmployeeJobTypeControls.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.EmployeeJobTypeControls.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    
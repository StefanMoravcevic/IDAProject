using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.EmployeeAbsences;
using IDAProject.Web.Models.RequestModels.EmployeeAbsences;

namespace IDAProject.Web.Api.Repositories
{
    public class EmployeeAbsencesRepository : IEmployeeAbsencesRepository
    {
        private readonly IdaContext _dbContext;

        public EmployeeAbsencesRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EmployeeAbsenceDto> GetEmployeeAbsenceByIdAsync(int id)
        {
            var searchParams = new SearchEmployeeAbsencesParams
            {
                Id = id
            };
            var result = await SearchEmployeeAbsencesAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<EmployeeAbsenceDto>> SearchEmployeeAbsencesAsync(SearchEmployeeAbsencesParams searchParams)
        {

            var result = new List<EmployeeAbsenceDto>();
            IQueryable<EmployeeAbsence> query = _dbContext.EmployeeAbsences.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if(searchParams.EmployeeId.HasValue)
                {
                    query = query.Where(x => x.EmployeeId == searchParams.EmployeeId);
                }    
            }

            result = await query.Select(a => new EmployeeAbsenceDto
            {
                Id = a.Id,
                AbsenceTypeId = a.AbsenceTypeId,
                AllDay = a.AllDay,
                Comment = a.Comment,
                DateFrom = a.DateFrom,
                DateTo = a.DateTo,
                EmployeeId = a.EmployeeId,
                TimeFrom = a.TimeFrom,
                TimeTo = a.TimeTo,
                AbsenceType = a.AbsenceType.Name

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveEmployeeAbsenceAsync(SaveEmployeeAbsenceRequestModel requestModel)
        {
            EmployeeAbsence? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.EmployeeAbsences.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveEmployeeAbsenceRequestModel,EmployeeAbsence>(requestModel);
                _dbContext.EmployeeAbsences.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteEmployeeAbsenceAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.EmployeeAbsences.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.EmployeeAbsences.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    
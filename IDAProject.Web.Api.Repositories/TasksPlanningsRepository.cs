using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.TasksPlannings;
using IDAProject.Web.Models.RequestModels.TasksPlannings;

namespace IDAProject.Web.Api.Repositories
{
    public class TasksPlanningsRepository : ITasksPlanningsRepository
    {
        private readonly IdaContext _dbContext;

        public TasksPlanningsRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TasksPlanningDto> GetTasksPlanningByIdAsync(int id)
        {
            var searchParams = new SearchTasksPlanningsParams
            {
                Id = id
            };
            var result = await SearchTasksPlanningsAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<TasksPlanningDto>> SearchTasksPlanningsAsync(SearchTasksPlanningsParams searchParams)
        {

            var result = new List<TasksPlanningDto>();
            IQueryable<TasksPlanning> query = _dbContext.TasksPlannings.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                //implement actual search params here
                throw new System.NotImplementedException();     
            }

            result = await query.Select(a => new TasksPlanningDto
            {
                //implement assignment of properties here, only id is implemented as default
                Id = a.Id

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveTasksPlanningAsync(SaveTasksPlanningRequestModel requestModel)
        {
            TasksPlanning? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.TasksPlannings.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveTasksPlanningRequestModel,TasksPlanning>(requestModel);
                _dbContext.TasksPlannings.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteTasksPlanningAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.TasksPlannings.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.TasksPlannings.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    
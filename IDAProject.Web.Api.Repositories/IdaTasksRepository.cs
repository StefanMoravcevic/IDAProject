using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.IdaTasks;
using IDAProject.Web.Models.RequestModels.IdaTasks;

namespace IDAProject.Web.Api.Repositories
{
    public class IdaTasksRepository : IIdaTasksRepository
    {
        private readonly IdaContext _dbContext;

        public IdaTasksRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IdaTaskDto> GetIdaTaskByIdAsync(int id)
        {
            var searchParams = new SearchIdaTasksParams
            {
                Id = id
            };
            var result = await SearchIdaTasksAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<IdaTaskDto>> SearchIdaTasksAsync(SearchIdaTasksParams searchParams)
        {

            var result = new List<IdaTaskDto>();
            IQueryable<IdaTask> query = _dbContext.IdaTasks.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {    
                if(searchParams.HasProject.HasValue && searchParams.HasProject.Value == true)
                {
                    query = query.Where(x => x.ProjectId != null);
                }
            }

            result = await query.Select(a => new IdaTaskDto
            {
                Id = a.Id,
                Description = a.Description,
                DueDate = a.DueDate,
                IsCompleted = a.IsCompleted,
                Name = a.Name,
                ProjectId = a.ProjectId,
                Project = a.Project.Description

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveIdaTaskAsync(SaveIdaTaskRequestModel requestModel)
        {
            IdaTask? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.IdaTasks.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveIdaTaskRequestModel,IdaTask>(requestModel);
                _dbContext.IdaTasks.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteIdaTaskAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.IdaTasks.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.IdaTasks.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    
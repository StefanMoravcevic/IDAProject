using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.Projects;
using IDAProject.Web.Models.RequestModels.Projects;

namespace IDAProject.Web.Api.Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly IdaContext _dbContext;

        public ProjectsRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectDto> GetProjectByIdAsync(int id)
        {
            var searchParams = new SearchProjectsParams
            {
                Id = id
            };
            var result = await SearchProjectsAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<ProjectDto>> SearchProjectsAsync(SearchProjectsParams searchParams)
        {

            var result = new List<ProjectDto>();
            IQueryable<Project> query = _dbContext.Projects.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                throw new System.NotImplementedException();     
            }

            result = await query.Select(a => new ProjectDto
            {
                Id = a.Id

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveProjectAsync(SaveProjectRequestModel requestModel)
        {
            Project? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.Projects.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveProjectRequestModel,Project>(requestModel);
                _dbContext.Projects.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteProjectAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.Projects.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.Projects.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    
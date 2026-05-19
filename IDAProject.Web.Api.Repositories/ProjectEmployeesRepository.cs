using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.ProjectEmployees;
using IDAProject.Web.Models.RequestModels.ProjectEmployees;

namespace IDAProject.Web.Api.Repositories
{
    public class ProjectEmployeesRepository : IProjectEmployeesRepository
    {
        private readonly IdaContext _dbContext;

        public ProjectEmployeesRepository(IdaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectEmployeeDto> GetProjectEmployeeByIdAsync(int id)
        {
            var searchParams = new SearchProjectEmployeesParams
            {
                Id = id
            };
            var result = await SearchProjectEmployeesAsync(searchParams);
            return result.FirstOrDefault();
        }

        public async Task<List<ProjectEmployeeDto>> SearchProjectEmployeesAsync(SearchProjectEmployeesParams searchParams)
        {

            var result = new List<ProjectEmployeeDto>();
            IQueryable<ProjectEmployee> query = _dbContext.ProjectEmployees.Where(x => x.IsDeleted == false);
            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.ProjectId.HasValue)
                {
                    query = query.Where(x => x.ProjectId == searchParams.ProjectId);
                }
                if (searchParams.EmployeeId.HasValue)
                {
                    query = query.Where(x => x.EmployeeId == searchParams.EmployeeId); 
                }
            }

            result = await query.Select(a => new ProjectEmployeeDto
            {
                Id = a.Id,
                ProjectId = a.ProjectId,
                EmployeeId = a.EmployeeId,
                Employee = a.Employee.Name + " " + a.Employee.Surname,
                Project = a.Project.Description

            }).ToListAsync();
            return result;

        }

        public async Task<int> SaveProjectEmployeeAsync(SaveProjectEmployeeRequestModel requestModel)
        {
            ProjectEmployee? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.ProjectEmployees.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);

            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveProjectEmployeeRequestModel,ProjectEmployee>(requestModel);
                _dbContext.ProjectEmployees.Add(dbRecord!);
            }
            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }

        public async Task DeleteProjectEmployeeAsync(int id, int? userId)
        {
            var dbRecord = await _dbContext.ProjectEmployees.SingleAsync(x => x.Id == id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = userId;
            dbRecord.DeletedDate = DateTime.Now;
            _dbContext.ProjectEmployees.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
        }

    }
}
    
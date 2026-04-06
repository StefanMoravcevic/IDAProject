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
                if (searchParams.HasProject.HasValue)
                {
                    if (searchParams.HasProject.Value)
                        query = query.Where(x => x.ProjectId != null);
                    else
                        query = query.Where(x => x.ProjectId == null);
                }
                if (searchParams.IsCompleted.HasValue)
                {
                    query = query.Where(x => x.IsCompleted == searchParams.IsCompleted);
                }
                if (searchParams.ProjectId.HasValue)
                {
                    query = query.Where(x => x.ProjectId == searchParams.ProjectId);
                }
                if (searchParams.UserId.HasValue)
                {
                    query = query.Where(x => x.UserId == searchParams.UserId);
                }
                if (searchParams.EmployeeId.HasValue)
                {
                    query = query.Where(x => x.User.EmployeeId == searchParams.EmployeeId);
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
                Project = a.Project.Description,
                UserId = a.UserId,
                CompletedDate = a.CompletedDate

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

        public async Task<List<IdaTaskDto>> GetTasksByProjectAsync(int projectId)
        {
            var query = _dbContext.IdaTasks
                .Where(t => t.IsDeleted == false && t.ProjectId == projectId)
                .Include(t => t.User)
                .Include(t => t.Project)   
                .Include(t => t.TasksPlannings)
                .Include(t => t.TasksRealizations)
                .AsQueryable();

            var result = await query
                .Select(t => new IdaTaskDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    IsCompleted = t.IsCompleted,
                    Name = t.Name,
                    ProjectId = t.ProjectId,
                    Project = t.Project.Description,
                    UserId = t.UserId,
                    Employee = t.User.Employee.Name + " " + t.User.Employee.Surname,
                    CompletedDate = t.CompletedDate,
                    Activity = t.TasksPlannings.FirstOrDefault().ActivityName,
                    Report = t.TasksRealizations.FirstOrDefault().Report,
                    Status = t.TasksPlannings.FirstOrDefault().PlanStatus.Name,
                    //ProjectDueDate = t.Project.DueDate,
                    PlanDate = t.TasksPlannings.FirstOrDefault().PlanDate
                })
                .ToListAsync();

            return result;
        }
        public async Task<List<IdaTaskDto>> GetTaskByTaskIdAsync(int taskId)
        {
            var query = _dbContext.IdaTasks
                .Where(t => t.IsDeleted == false && t.Id == taskId)
                .Include(t => t.User)
                .Include(t => t.Project)   
                .Include(t => t.TasksPlannings)
                .Include(t => t.TasksRealizations)
                .AsQueryable();

            var result = await query
                .Select(t => new IdaTaskDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    IsCompleted = t.IsCompleted,
                    Name = t.Name,
                    ProjectId = t.ProjectId,
                    Project = t.Project.Description,
                    UserId = t.UserId,
                    Employee = t.User.Employee.Name + " " + t.User.Employee.Surname,
                    CompletedDate = t.CompletedDate,
                    Activity = t.TasksPlannings.FirstOrDefault().ActivityName,
                    Report = t.TasksRealizations.FirstOrDefault().Report,
                    Status = t.TasksPlannings.FirstOrDefault().PlanStatus.Name,
                    //ProjectDueDate = t.Project.DueDate,
                    PlanDate = t.TasksPlannings.FirstOrDefault().PlanDate
                })
                .ToListAsync();

            return result;
        }
    }
}
        
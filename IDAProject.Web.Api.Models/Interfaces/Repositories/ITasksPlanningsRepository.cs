
using IDAProject.Web.Models.Dto.TasksPlannings;
using IDAProject.Web.Models.RequestModels.TasksPlannings;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface ITasksPlanningsRepository
    {
        Task<TasksPlanningDto> GetTasksPlanningByIdAsync(int id);
        Task<int> SaveTasksPlanningAsync(SaveTasksPlanningRequestModel requestModel);
        Task<EmployeePlanningStatsDto> GetLast30DaysStats(int employeeId);
        Task<EmployeePlanningStatsDto> GetStatsGeneric(int employeeId, DateTime? from, DateTime? to);
        Task<List<TasksPlanningDto>> SearchTasksPlanningsAsync(SearchTasksPlanningsParams searchParams);
        Task DeleteTasksPlanningAsync(int id, int? userId);
    }
}

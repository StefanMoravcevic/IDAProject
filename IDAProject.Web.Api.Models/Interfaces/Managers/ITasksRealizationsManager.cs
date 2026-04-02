using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.TasksRealizations;
using IDAProject.Web.Models.RequestModels.TasksRealizations;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface ITasksRealizationsManager
    {
        Task<ResponseModelList<TasksRealizationDto>> SearchTasksRealizationsAsync(SearchTasksRealizationsParams searchParams);
        Task<ResponseModel<TasksRealizationDto>> GetTasksRealizationByIdAsync(int id);
        Task<ResponseModel<EmployeeRealizationStatsDto>> GetLast30DaysRealizationStats(int employeeId);
        Task<ResponseModel<EmployeeRealizationStatsDto>> GetGenericStats(int employeeId, DateTime? from, DateTime? to);
        Task<ResponseModelBase> DeleteTasksRealizationAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveTasksRealizationAsync(SaveTasksRealizationRequestModel requestModel);
    }
}

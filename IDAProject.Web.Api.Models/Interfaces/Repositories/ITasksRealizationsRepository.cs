
using IDAProject.Web.Models.Dto.TasksRealizations;
using IDAProject.Web.Models.RequestModels.TasksRealizations;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface ITasksRealizationsRepository
    {
        Task<TasksRealizationDto> GetTasksRealizationByIdAsync(int id);
        Task<int> SaveTasksRealizationAsync(SaveTasksRealizationRequestModel requestModel);
        Task<List<TasksRealizationDto>> SearchTasksRealizationsAsync(SearchTasksRealizationsParams searchParams);
        Task DeleteTasksRealizationAsync(int id, int? userId);
    }
}

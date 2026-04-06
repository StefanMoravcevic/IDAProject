
using IDAProject.Web.Models.Dto.IdaTasks;
using IDAProject.Web.Models.RequestModels.IdaTasks;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IIdaTasksRepository
    {
        Task<IdaTaskDto> GetIdaTaskByIdAsync(int id);
        Task<int> SaveIdaTaskAsync(SaveIdaTaskRequestModel requestModel);
        Task<List<IdaTaskDto>> SearchIdaTasksAsync(SearchIdaTasksParams searchParams);
        Task<List<IdaTaskDto>> GetTasksByProjectAsync(int projectId);
        Task<List<IdaTaskDto>> GetTaskByTaskIdAsync(int taskId);
        Task DeleteIdaTaskAsync(int id, int? userId);
    }
}

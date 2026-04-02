
using IDAProject.Web.Models.Dto.EmployeeGoals;
using IDAProject.Web.Models.RequestModels.EmployeeGoals;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IEmployeeGoalsRepository
    {
        Task<EmployeeGoalDto> GetEmployeeGoalByIdAsync(int id);
        Task<int> SaveEmployeeGoalAsync(SaveEmployeeGoalRequestModel requestModel);
        Task<List<EmployeeGoalDto>> SearchEmployeeGoalsAsync(SearchEmployeeGoalsParams searchParams);
        Task DeleteEmployeeGoalAsync(int id, int? userId);
    }
}

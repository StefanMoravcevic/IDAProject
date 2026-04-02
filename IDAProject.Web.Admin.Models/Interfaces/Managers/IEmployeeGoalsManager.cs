using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.EmployeeGoals;
using IDAProject.Web.Models.RequestModels.EmployeeGoals;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IEmployeeGoalsManager
    {
        Task<ResponseModelList<EmployeeGoalDto>> SearchEmployeeGoalsAsync(SearchEmployeeGoalsParams searchParams);
        Task<ResponseModel<EmployeeGoalDto>> GetEmployeeGoalByIdAsync(int id);
        Task<ResponseModelBase> DeleteEmployeeGoalAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveEmployeeGoalAsync(SaveEmployeeGoalRequestModel requestModel);
    }
}



using IDAProject.Web.Models.Dto.EmployeeAbsences;
using IDAProject.Web.Models.RequestModels.EmployeeAbsences;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IEmployeeAbsencesRepository
    {
        Task<EmployeeAbsenceDto> GetEmployeeAbsenceByIdAsync(int id);
        Task<int> SaveEmployeeAbsenceAsync(SaveEmployeeAbsenceRequestModel requestModel);
        Task<List<EmployeeAbsenceDto>> SearchEmployeeAbsencesAsync(SearchEmployeeAbsencesParams searchParams);
        Task DeleteEmployeeAbsenceAsync(int id, int? userId);
    }
}

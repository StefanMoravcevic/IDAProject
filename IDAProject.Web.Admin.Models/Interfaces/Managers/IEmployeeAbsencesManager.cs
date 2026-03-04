using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Dto.EmployeeAbsences;
using IDAProject.Web.Models.RequestModels.EmployeeAbsences;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IEmployeeAbsencesManager
    {
        Task<ResponseModelList<EmployeeAbsenceDto>> SearchEmployeeAbsencesAsync(SearchEmployeeAbsencesParams searchParams);
        Task<ResponseModel<EmployeeAbsenceDto>> GetEmployeeAbsenceByIdAsync(int id);
        Task<ResponseModelBase> DeleteEmployeeAbsenceAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveEmployeeAbsenceAsync(SaveEmployeeAbsenceRequestModel requestModel);
    }
}


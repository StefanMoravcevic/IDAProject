using IDAProject.Web.Models.Common;
using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.Dto.Messages;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.Employees;
using IDAProject.Web.Models.RequestModels.Messages;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IEmployeesManager
    {
        Task<ResponseModelList<EmployeeDto>> SearchEmployeesAsync(SearchEmployeesParams searchParams);

        Task<ResponseModel<EmployeeDto>> GetEmployeeByIdAsync(int id);

        Task<ResponseModel<int>> SaveEmployeeAsync(SaveEmployeeRequestModel requestModel);

        Task<ResponseModelBase> DeleteEmployeeAsync(int id, int userId);

        Task<IEnumerable<ISelectOption>> GetDriversAsSelectOptionsAsync();

        Task<IEnumerable<ISelectOption>> GetEmployeesAsSelectOptionsByJobIdAsync(int jobType);

        Task<IEnumerable<ISelectOption>> GetEmployeesAsSelectOptionsAsync();

		Task<ResponseModel<EmployeeDto>> GetNextRowAsync(int currentId);
		Task<ResponseModel<EmployeeDto>> GetPreviousRowAsync(int currentId);

		Task<ResponseModelList<EmployeeDto>> GetEmployeesByOrgUnitSentFromAsync(int orgUnitId);
		Task<ResponseModelList<EmployeeDto>> GetEmployeesByOrgUnitSentToAsync(int orgUnitId);


		#region Family members

		Task<ResponseModel<FamilyMemberDto>> GetFamilyMemberByIdAsync(int id);
        Task<ResponseModelList<FamilyMemberDto>> SearchFamilyMembersAsync(SearchFamilyMembersParams searchParams);
        Task<ResponseModel<int>> SaveFamilyMemberAsync(SaveFamilyMemberRequestModel requestModel);
        Task<ResponseModelBase> DeleteFamilyMemberAsync(int id, int userId);

        #endregion


        #region User messages

        Task<ResponseModelList<UserMessageDto>> SearchUserMessagesAsync(SearchUserMessagesParams searchParams);
        Task<ResponseModelList<EmailDto>> SearchEmailsAsync(SearchEmailsParams searchParams);

        #endregion

    }
}

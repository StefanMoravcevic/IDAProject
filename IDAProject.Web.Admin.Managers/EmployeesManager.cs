using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.Employees;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.Dto.Messages;
using IDAProject.Web.Models.RequestModels.Messages;
using IDAProject.Web.Models.Dto.Contacts;
using IDAProject.Web.Models.Dto.Users;
using IDAProject.Web.Models.Common;

namespace IDAProject.Web.Admin.Managers
{
    public class EmployeesManager : BaseManager, IEmployeesManager
    {
        public EmployeesManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<EmployeesManager> logger)
            : base(httpClientFactory, configuration, logger)
        {
        }

        public async Task<ResponseModel<EmployeeDto>> GetEmployeeByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<EmployeeDto>>($"api/employees/{id}");
            return result;
        }

        public async Task<ResponseModelList<EmployeeDto>> SearchEmployeesAsync(SearchEmployeesParams searchParams)
        {
            var result = await PostAsync<SearchEmployeesParams, ResponseModelList<EmployeeDto>>($"api/employees/search", searchParams);
            return result;
        }

        public async Task<ResponseModel<int>> SaveEmployeeAsync(SaveEmployeeRequestModel requestModel)
        {
            var result = await PostAsync<SaveEmployeeRequestModel, ResponseModel<int>>($"api/employees", requestModel);
            return result;
        }
        public async Task<ResponseModelBase> DeleteEmployeeAsync(int id, int userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/employees/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModelList<EmployeeDto>> GetEmployeesByOrgUnitSentFromAsync(int orgUnitId)
		{
			var result = await GetAsync<ResponseModelList<EmployeeDto>>($"api/employees/employeeByOrgUnitSentFrom/{orgUnitId}");
			return result;
		}

		public async Task<ResponseModelList<EmployeeDto>> GetEmployeesByOrgUnitSentToAsync(int orgUnitId)
		{
			var result = await GetAsync<ResponseModelList<EmployeeDto>>($"api/employees/employeeByOrgUnitSentTo/{orgUnitId}");
			return result;
		}


		public async Task<IEnumerable<ISelectOption>> GetDriversAsSelectOptionsAsync()
        {
            var searchParams = new SearchEmployeesParams
            {
                JobTypes = new List<int> { JobTypes.Company_driver, JobTypes.External_driver }
            };

            var driversResponse = await SearchEmployeesAsync(searchParams);
            var driversList = driversResponse.Payload.OrderBy(x => x.Name).ThenBy(y => y.Surname);
            var result = driversList.Select(x => new GenericSelectOption
            {
                Value = x.Id,
                Description = x.Name + " " + x.MiddleName + " " + x.Surname
            });
            return result;
        }

        public async Task<IEnumerable<ISelectOption>> GetEmployeesAsSelectOptionsByJobIdAsync(int jobType)
        {
            var jobTypes = new List<int>();
            if (jobType != 0)
            {
                jobTypes.Add(jobType);
            }

            var searchParams = new SearchEmployeesParams
            {
                JobTypes = jobTypes
            };

            var employeesResponse = await SearchEmployeesAsync(searchParams);
            var employeesList = employeesResponse.Payload.OrderBy(x => x.Name).ThenBy(y => y.Surname);
            var result = employeesList.Select(x => new GenericSelectOption
            {
                Value = x.Id,
                Description = x.Name + " " + x.MiddleName + " " + x.Surname
            });
            return result.ToList();
        }

        public async Task<IEnumerable<ISelectOption>> GetEmployeesAsSelectOptionsAsync()
        {
            var searchParams = new SearchEmployeesParams();
            var employeesResponse = await SearchEmployeesAsync(searchParams);
            var employeesList = employeesResponse.Payload.OrderBy(x => x.Name).ThenBy(y => y.Surname);

            var result = employeesList.Select(x => new GenericSelectOption
            {
                Value = x.Id,
                Description = string.IsNullOrEmpty(x.EmployeeNumber)
                    ? $"{x.Name} {x.MiddleName} {x.Surname}"
                    : $"{x.Name} {x.MiddleName} {x.Surname}"
            });

            return result;
        }




        public async Task<ResponseModel<EmployeeDto>> GetNextRowAsync(int currentId)
		{
			var result = await GetAsync<ResponseModel<EmployeeDto>>($"api/employees/nextEmployee/{currentId}");
			return result;
		}

		public async Task<ResponseModel<EmployeeDto>> GetPreviousRowAsync(int currentId)
		{
            var result = await GetAsync<ResponseModel<EmployeeDto>>($"api/employees/previousEmployee/{currentId}");
            return result;
        }

		#region Family members
		public async Task<ResponseModel<FamilyMemberDto>> GetFamilyMemberByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<FamilyMemberDto>>($"api/employees/getFamilyMember/{id}");
            return result;
        }

        public async Task<ResponseModelList<FamilyMemberDto>> SearchFamilyMembersAsync(SearchFamilyMembersParams searchParams)
        {
            var result = await PostAsync<SearchFamilyMembersParams, ResponseModelList<FamilyMemberDto>>($"api/employees/searchFamilyMembers", searchParams);
            return result;
        }

        public async Task<ResponseModel<int>> SaveFamilyMemberAsync(SaveFamilyMemberRequestModel requestModel)
        {
            var result = await PostAsync<SaveFamilyMemberRequestModel, ResponseModel<int>>($"api/employees/saveFamilyMember", requestModel);
            return result;
        }

        public async Task<ResponseModelBase> DeleteFamilyMemberAsync(int id, int userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/employees/deleteFamilyMember/{id}/{userId}");
            return result;
        }

        #endregion


        #region User messages

        public async Task<ResponseModelList<UserMessageDto>> SearchUserMessagesAsync(SearchUserMessagesParams searchParams)
        {
            var result = await PostAsync<SearchUserMessagesParams, ResponseModelList<UserMessageDto>>($"api/messages/searchUserMessages", searchParams);
            return result;
        }

        public async Task<ResponseModelList<EmailDto>> SearchEmailsAsync(SearchEmailsParams searchParams)
        {
            var result = await PostAsync<SearchEmailsParams, ResponseModelList<EmailDto>>($"api/messages/searchEmails", searchParams);
            return result;
        }

        public async Task<IEnumerable<ISelectOption>> GetEmployeesAsSelectOptionsByOrgUnitIdAsync(int? orgUnitId)
        {
            var searchParams = new SearchEmployeesParams {OrgUnitId = orgUnitId};
            var employeesResponse = await SearchEmployeesAsync(searchParams);
            var employeesList = employeesResponse.Payload.OrderBy(x => x.Name).ThenBy(y => y.Surname);
            var result = employeesList.Select(x => new GenericSelectOption
            {
                Value = x.Id,
                Description = x.Name + " " + x.MiddleName + " " + x.Surname
            });
            return result;
        }





        #endregion

    }
}

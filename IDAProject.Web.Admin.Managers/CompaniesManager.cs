using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Companies;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Companies;

namespace IDAProject.Web.Admin.Managers
{
    public class CompaniesManager : BaseManager, ICompaniesManager
    {
        public CompaniesManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<CompaniesManager> logger,IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, configuration, logger)
        {
        }

        public async Task<ResponseModelList<CompanyDto>> GetAllowedCompaniesByUserAsync(int userId)
        {
            var result = await GetAsync<ResponseModelList<CompanyDto>>($"api/companies/allowed/{userId}");
            return result;
        }

        public async Task<List<GenericSelectOption>> GetAllowedCompaniesOptionsByUserAsync(int userId)
        {
            var companies = new List<GenericSelectOption>();
            var companiesResponse = await GetAllowedCompaniesByUserAsync(userId);

            if (companiesResponse.Valid)
            {
                foreach (var company in companiesResponse.Payload)
                {
                    var selectOption = new GenericSelectOption
                    {
                        Value = company.Id,
                        Description = company.Name
                    };
                    companies.Add(selectOption);
                }
            }
            return companies;
        }

        public async Task<ResponseModel<CompanyDto>> GetCompanyByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<CompanyDto>>($"api/companies/{id}");
            return result;
        }

        public async Task<ResponseModelList<CompanyDto>> SearchCompaniesAsync(SearchCompaniesParams searchParams)
        {
            var result = await PostAsync<SearchCompaniesParams, ResponseModelList<CompanyDto>>($"api/companies/search", searchParams);
            return result;
        }

        public async Task<ResponseModel<int>> SaveCompanyAsync(SaveCompanyRequestModel requestModel)
        {
            var result = await PostAsync<SaveCompanyRequestModel, ResponseModel<int>>($"api/companies", requestModel);
            return result;
        }
        public async Task<ResponseModelBase> DeleteCompanyAsync(int id, int userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/companies/delete/{id}/{userId}");
            return result;
        }

        #region Org units
        public async Task<ResponseModel<OrgUnitDto>> GetOrgUnitByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<OrgUnitDto>>($"api/companies/getOrgUnit/{id}");
            return result;
        }

        public async Task<ResponseModelList<OrgUnitDto>> SearchOrgUnitsAsync(SearchOrgUnitsParams searchParams)
        {
            var result = await PostAsync<SearchOrgUnitsParams, ResponseModelList<OrgUnitDto>>($"api/companies/searchOrgUnits", searchParams);
            return result;
        }

        public async Task<ResponseModel<int>> SaveOrgUnitAsync(SaveOrgUnitRequestModel requestModel)
        {
            var result = await PostAsync<SaveOrgUnitRequestModel, ResponseModel<int>>($"api/companies/saveOrgUnit", requestModel);
            return result;
        }

        public async Task<ResponseModelBase> DeleteOrgUnitAsync(int id, int userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/companies/deleteOrgUnit/{id}/{userId}");
            return result;
        }

        #endregion

    }
}
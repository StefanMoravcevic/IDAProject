using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.Companies;
using IDAProject.Web.Models.RequestModels.Companies;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface ICompaniesRepository
    {
        /// <summary>
        /// Retuns a list of companies which are allowed by the specified <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Retuns a list of companies which are allowed by the specified <paramref name="userId"/></returns>

        Task<CompanyDto> GetCompanyAsync(int id);
        Task<CompanyDto?> GetCompanyByIdAsync(int id);
        Task<int> SaveCompanyAsync(SaveCompanyRequestModel requestModel);
        Task DeleteCompanyAsync(int id, int? userId);
        Task<List<CompanyDto>> SearchCompaniesAsync(SearchCompaniesParams searchParams);

        #region Org units
        Task<OrgUnitDto?> GetOrgUnitByIdAsync(int id);
        Task<int> SaveOrgUnitAsync(SaveOrgUnitRequestModel requestModel);
        Task DeleteOrgUnitAsync(int id, int? userId);
        Task<List<OrgUnitDto>> SearchOrgUnitsAsync(SearchOrgUnitsParams searchParams);
        #endregion
    }
}

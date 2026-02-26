using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.Companies;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Companies;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface ICompaniesManager
    {
        Task<ResponseModelList<CompanyDto>> SearchCompaniesAsync(SearchCompaniesParams searchParams);

        Task<ResponseModel<CompanyDto>> GetCompanyByIdAsync(int id);
        Task<ResponseModelBase> DeleteCompanyAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveCompanyAsync(SaveCompanyRequestModel requestModel);

        #region Org units
        Task<ResponseModelList<OrgUnitDto>> SearchOrgUnitsAsync(SearchOrgUnitsParams searchParams);
        Task<ResponseModel<OrgUnitDto>> GetOrgUnitByIdAsync(int id);
        Task<ResponseModelBase> DeleteOrgUnitAsync(int id, int? userId);
        Task<ResponseModel<int>> SaveOrgUnitAsync(SaveOrgUnitRequestModel requestModel);
        #endregion
    }
}

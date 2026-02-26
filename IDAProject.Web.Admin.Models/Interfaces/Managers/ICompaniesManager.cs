using IDAProject.Web.Models.Dto.Companies;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Companies;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface ICompaniesManager
    {
        Task<ResponseModelList<CompanyDto>> GetAllowedCompaniesByUserAsync(int userId);

        Task<List<GenericSelectOption>> GetAllowedCompaniesOptionsByUserAsync(int userId);
        Task<ResponseModel<CompanyDto>> GetCompanyByIdAsync(int id);

        Task<ResponseModelList<CompanyDto>> SearchCompaniesAsync(SearchCompaniesParams searchParams);

        Task<ResponseModel<int>> SaveCompanyAsync(SaveCompanyRequestModel requestModel);
        Task<ResponseModelBase> DeleteCompanyAsync(int id, int userId);

        #region Org units
        Task<ResponseModel<OrgUnitDto>> GetOrgUnitByIdAsync(int id);

        Task<ResponseModelList<OrgUnitDto>> SearchOrgUnitsAsync(SearchOrgUnitsParams searchParams);

        Task<ResponseModel<int>> SaveOrgUnitAsync(SaveOrgUnitRequestModel requestModel);

        Task<ResponseModelBase> DeleteOrgUnitAsync(int id, int userId);

        #endregion
    }
}

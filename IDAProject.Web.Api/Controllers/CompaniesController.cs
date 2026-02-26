using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Companies;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Companies;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompaniesManager _companiesManager;

        public CompaniesController(ICompaniesManager companiesManager)
        {
            _companiesManager = companiesManager;
        }

        [HttpGet("allowed/{userId}")]
        //public async Task<ResponseModelList<CompanyDto>> GetAllowedCompaniesByUserAsync(int userId)
        //{
        //    var result = await _companiesManager.GetAllowedCompaniesByUserAsync(userId);
        //    return result;
        //}

        [HttpGet("{id}")]
        public async Task<ResponseModel<CompanyDto>> GetCompanyByIdAsync(int id)
        {
            var response = await _companiesManager.GetCompanyByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteCompanyAsync(int id, int? userId)
        {
            var response = await _companiesManager.DeleteCompanyAsync(id, userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<CompanyDto>> SearchCompaniesAsync(SearchCompaniesParams searchParams)
        {
            var response = await _companiesManager.SearchCompaniesAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveCompanyAsync(SaveCompanyRequestModel requestModel)
        {
            var response = await _companiesManager.SaveCompanyAsync(requestModel);
            return response;
        }

        #region Org units

        [HttpGet("getOrgUnit/{id}")]
        public async Task<ResponseModel<OrgUnitDto>> GetOrgUnitByIdAsync(int id)
        {
            var response = await _companiesManager.GetOrgUnitByIdAsync(id);
            return response;
        }


        [HttpPost("searchOrgUnits")]
        public async Task<ResponseModelList<OrgUnitDto>> SearchOrgUnitsAsync(SearchOrgUnitsParams searchParams)
        {
            var response = await _companiesManager.SearchOrgUnitsAsync(searchParams);
            return response;
        }


        [HttpPost("saveOrgUnit")]
        public async Task<ResponseModel<int>> SaveOrgUnitAsync(SaveOrgUnitRequestModel requestModel)
        {
            var response = await _companiesManager.SaveOrgUnitAsync(requestModel);
            return response;
        }


        [HttpDelete("deleteOrgUnit/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteOrgUnitAsync(int id, int? userId)
        {
            var response = await _companiesManager.DeleteOrgUnitAsync(id, userId);
            return response;
        }

        #endregion
    }
}

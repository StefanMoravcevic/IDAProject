using IDAProject.Web.Admin.Controllers;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.Companies;
using IDAProject.Web.Models.Dto.Companies;
using IDAProject.Web.Models.RequestModels.Companies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.Companies;
using IDAProject.Web.Models.Dto.Companies;
using IDAProject.Web.Models.RequestModels.Companies;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class CompaniesController : BaseController
    {
        private readonly ICompaniesManager _companiesManager;
        private readonly IMasterDataManager _masterDataManager;
        private IStringLocalizer<SharedResources> _localizer;

        public CompaniesController(
            ILogger<CompaniesController> logger,
            IAccountManager accountManager,
            IStringLocalizer<SharedResources> localizer,
            ICompaniesManager companiesManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _companiesManager = companiesManager;
            _localizer = localizer;
            _masterDataManager = masterDataManager;
        }

        [HttpGet(Name = RouteNames.Companies_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new CompaniesViewModel(_localizer);
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "Companies");

            return View(viewModel);
        }

        [HttpGet("newCompany", Name = RouteNames.Companies_New)]
        public async Task<IActionResult> NewCompanyAsync()
        {
            var viewModel = new CompanyViewModel();
            viewModel.User = GetCurrentUser();
            viewModel.Companies = await _masterDataManager.GetSelectOptionsByTableAsync("Companies", "Name");
            viewModel.FactoringHouses = await _masterDataManager.GetFilteredSelectOptionsByTable("Partners", "PartnerTypeId", 2, "Name");
            viewModel.States = await _masterDataManager.GetSelectOptionsByTableAsync("States", "Name");
            return View("EditCompany", viewModel);
        }

        [HttpGet("editCompany/{id}", Name = RouteNames.Companies_Edit)]
        public async Task<IActionResult> EditCompanyAsync(int id)
        {
            var viewModel = new CompanyViewModel();
            var companyResponse = await _companiesManager.GetCompanyByIdAsync(id);
            viewModel.Company = companyResponse.Payload!;
            viewModel.Companies = await _masterDataManager.GetSelectOptionsByTableAsync("Companies", "Name");
            viewModel.FactoringHouses = await _masterDataManager.GetFilteredSelectOptionsByTable("Partners", "PartnerTypeId", 2, "Name");
            viewModel.States = await _masterDataManager.GetSelectOptionsByTableAsync("States", "Name");
            if (viewModel.Company.StateId > 0)
            {
                //var citiesResponse = await _geoLocationManager.GetCitiesByStateIdAsync(viewModel.Company.StateId);
                //viewModel.Cities = citiesResponse.Payload;
            }
            viewModel.User = GetCurrentUser();

            return View("EditCompany", viewModel);
        }

        [HttpGet("viewCompany/{id}", Name = RouteNames.Companies_View)]
        public async Task<IActionResult> ViewCompanyAsync(int id)
        {
            var viewModel = new CompanyViewModel();
            var companyResponse = await _companiesManager.GetCompanyByIdAsync(id);
            viewModel.Company = companyResponse.Payload!;
            viewModel.Companies = await _masterDataManager.GetSelectOptionsByTableAsync("Companies", "Name");
            viewModel.FactoringHouses = await _masterDataManager.GetFilteredSelectOptionsByTable("Partners", "PartnerTypeId", 2, "Name");
            viewModel.States = await _masterDataManager.GetSelectOptionsByTableAsync("States", "Name");
            if (viewModel.Company.StateId > 0)
            {
                //var citiesResponse = await _geoLocationManager.GetCitiesByStateIdAsync(viewModel.Company.StateId);
                //viewModel.Cities = citiesResponse.Payload;
            }
            viewModel.ReadOnly = 1;
            viewModel.User = GetCurrentUser();

            return View("EditCompany", viewModel);
        }


        [HttpPost("searchCompanies", Name = RouteNames.Companies_Search)]
        public async Task<IActionResult> SearchCompaniesAsync(SearchCompaniesParams searchParams)
        {
            var responseModel = await _companiesManager.SearchCompaniesAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpPost("saveCompany", Name = RouteNames.Companies_Save)]
        public async Task<IActionResult> SaveCompanyAsync(SaveCompanyRequestModel requestModel)
        {
            var user = GetCurrentUser();
            var responseModel = await _companiesManager.SaveCompanyAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Companies_List)!;
            }
            return Json(responseModel);
        }
        [HttpDelete("deleteCompany/{id}", Name = RouteNames.Companies_Delete)]
        public async Task<IActionResult> DeleteCompanyAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _companiesManager.DeleteCompanyAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Companies_List)!;
            }
            return Json(responseModel);
        }

        #region Org units


        [HttpGet("orgUnitsList/{CompanyId}", Name = RouteNames.OrgUnits_List)]
        public async Task<IActionResult> OrgUnitsList(int CompanyId)
        {
            var viewModel = new OrgUnitsViewModel(_localizer);
            var companyResponse = await _companiesManager.GetCompanyByIdAsync(CompanyId);
            viewModel.Company = companyResponse.Payload!.Name;
            viewModel.CompanyId = CompanyId;
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "OrgUnits");

            return View("OrgUnitsIndex", viewModel);
        }


        [HttpGet("newOrgUnit/{CompanyId}", Name = RouteNames.OrgUnits_New)]
        public async Task<IActionResult> NewOrgUnit(int CompanyId)
        {
            var viewModel = new OrgUnitViewModel();
            viewModel.Companies = await _masterDataManager.GetSelectOptionsByTableAsync("Companies", "Name");
            viewModel.OrgUnits = await _masterDataManager.GetFilteredSelectOptionsByTable("OrgUnits", "CompanyId", CompanyId, "Name");
            viewModel.OrgUnit.CompanyId = CompanyId;
            viewModel.User = GetCurrentUser();
            return View("EditOrgUnit", viewModel);
        }


        [HttpGet("editOrgUnit/{id}", Name = RouteNames.OrgUnits_Edit)]
        public async Task<IActionResult> EditOrgUnitAsync(int id)
        {
            var viewModel = new OrgUnitViewModel();
            var orgUnitResponse = await _companiesManager.GetOrgUnitByIdAsync(id);
            viewModel.Companies = await _masterDataManager.GetSelectOptionsByTableAsync("Companies", "Name");
            viewModel.OrgUnits = await _masterDataManager.GetFilteredSelectOptionsByTable("OrgUnits", "CompanyId", orgUnitResponse.Payload!.CompanyId, "Name");
            viewModel.OrgUnit = orgUnitResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditOrgUnit", viewModel);
        }


        [HttpGet("viewOrgUnit/{id}", Name = RouteNames.OrgUnits_View)]
        public async Task<IActionResult> ViewOrgUnitAsync(int id)
        {
            var viewModel = new OrgUnitViewModel();
            var orgUnitResponse = await _companiesManager.GetOrgUnitByIdAsync(id);
            viewModel.Companies = await _masterDataManager.GetSelectOptionsByTableAsync("Companies", "Name");
            viewModel.OrgUnits = await _masterDataManager.GetFilteredSelectOptionsByTable("OrgUnits", "CompanyId", orgUnitResponse.Payload!.CompanyId, "Name");
            viewModel.OrgUnit = orgUnitResponse.Payload!;
            viewModel.ReadOnly = 1;
            viewModel.User = GetCurrentUser();

            return View("EditOrgUnit", viewModel);
        }


        [HttpPost("searchOrgUnits", Name = RouteNames.OrgUnits_Search)]
        public async Task<IActionResult> SearchOrgUnitsAsync(SearchOrgUnitsParams searchParams)
        {
            var responseModel = await _companiesManager.SearchOrgUnitsAsync(searchParams);
            return Json(responseModel.Payload);
        }


        [HttpPost("saveOrgUnit", Name = RouteNames.OrgUnits_Save)]
        public async Task<IActionResult> SaveOrgUnitAsync(SaveOrgUnitRequestModel requestModel)
        {
            var responseModel = await _companiesManager.SaveOrgUnitAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.OrgUnits_List, new { companyId = "111" })!;
            }
            return Json(responseModel);
        }



        [HttpDelete("deleteOrgUnit/{id}", Name = RouteNames.OrgUnits_Delete)]
        public async Task<IActionResult> DeleteOrgUnitAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _companiesManager.DeleteOrgUnitAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.OrgUnits_List, new { companyId = "111" })!;
            }
            return Json(responseModel);
        }

        #endregion

    }
}
using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.Employees;
using IDAProject.Web.Admin.Models.ViewModels.Messages;
using IDAProject.Web.Admin.Models.ViewModels.Addresses;
using IDAProject.Web.Models.Dto.Employees;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.Employees;
using IDAProject.Web.Models.RequestModels.Messages;
using Microsoft.Extensions.Localization;
using IDAProject.Web.Admin.Managers.Attributes;
using System.Text.RegularExpressions;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    [CustomAuthorization((int)AspNetRoles.Any, (int)AspNetFeatures.Any)]

    public class EmployeesController : BaseController
    {
        private readonly IEmployeesManager _employeesManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly ICompaniesManager _companiesManager;
        private readonly IPartnersManager _partnersManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EmployeesController(
            ILogger<EmployeesController> logger,
            IAccountManager accountManager,
            IEmployeesManager employeesManager,
            IMasterDataManager masterDataManager,
            IPartnersManager partnersManager,
            IStringLocalizer<SharedResources> localizer,
            ICompaniesManager companiesManager)
            : base(accountManager, logger)
        {
            _employeesManager = employeesManager;
            _masterDataManager = masterDataManager;
            _companiesManager = companiesManager;
            _partnersManager = partnersManager;
            _localizer = localizer;
        }

        [HttpGet(Name = RouteNames.Employees_List)]
        public async Task<IActionResult> Index(List<int> JobTypes)
        {
            var settings = await _masterDataManager.GetGeneralSettingsAsync();
            var pageViewLookGrouped = settings.EmployeeGroupedView;
            if (pageViewLookGrouped)
            {
                var viewModel = new EmployeesViewModel(_localizer);
                if (JobTypes.Count > 0) { viewModel.JobTypeId = JobTypes.FirstOrDefault(); }
                await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "Employees");
                return View("GroupedIndex", viewModel);
            }
            else
            {
                var viewModel = new EmployeesViewModel(_localizer);
                if (JobTypes.Count > 0) { viewModel.JobTypeId = JobTypes.FirstOrDefault(); }
                await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "Employees");
                return View(viewModel);
            }
        }
        [HttpGet("listById", Name = RouteNames.Employees_ListById)]
        public async Task<IActionResult> IndexById(int JobTypeId)
        {
            var settings = await _masterDataManager.GetGeneralSettingsAsync();
            var pageViewLookGrouped = settings.EmployeeGroupedView;
            if (pageViewLookGrouped)
            {
                var viewModel = new EmployeesViewModel(_localizer);
                viewModel.JobTypeId = JobTypeId;
                await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "Employees");
                return View("GroupedIndex", viewModel);
            }
            else
            {
                var viewModel = new EmployeesViewModel(_localizer);
                viewModel.JobTypeId = JobTypeId;
                await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "Employees");
                return View("Index", viewModel);
            }
        }
        [HttpGet("employeeByOrgUnitSentFrom/{orgUnitId}", Name = RouteNames.Employees_EmployeesByOrgUnitSentFrom)]
        public async Task<IActionResult> GetEmployeesByOrgUnitSentFromAsync(int orgUnitId)
        {
            try
            {
                var response = await _employeesManager.GetEmployeesByOrgUnitSentFromAsync(orgUnitId);
                return Json(response.Payload);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"orgUnitId: {orgUnitId}");
                throw;
            }
        }
        [HttpGet("employeeByOrgUnitSentTo/{orgUnitId}", Name = RouteNames.Employees_EmployeesByOrgUnitSentTo)]
        public async Task<IActionResult> GetEmployeesByOrgUnitSentToAsync(int orgUnitId)
        {
            try
            {
                var response = await _employeesManager.GetEmployeesByOrgUnitSentToAsync(orgUnitId);
                return Json(response.Payload);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"orgUnitId: {orgUnitId}");
                throw;
            }
        }

        private async Task<EmployeeViewModel> GetEmployeeViewModelAsync(int? employeeId)
        {
            var viewModel = new EmployeeViewModel();
            var currentUser = GetCurrentUser();
            if (employeeId.HasValue)
            {
                var employeResponse = await _employeesManager.GetEmployeeByIdAsync(employeeId.Value);
                viewModel.Employee = employeResponse.Payload!;


            }
            viewModel.Genders = await _masterDataManager.GetSelectOptionsByTableAsync("Genders", "Name");
            viewModel.ZipCodes = (await _masterDataManager.GetSelectOptionsByTableAsync("ZipCodes", "ZipCode1"))
            .GroupBy(zip => zip.Description)
            .Select(g => g.First())
            .OrderBy(zip => Convert.ToInt32(Regex.Replace(zip.Description, "[^0-9]", "")))
            .ToList();
            viewModel.JobTypes = await _masterDataManager.GetSelectOptionsByTableAsync("JobTypes", "Name");
            viewModel.States = await _masterDataManager.GetSelectOptionsByTableAsync("States", "Name");
            viewModel.Cities = await _masterDataManager.GetSelectOptionsByTableAsync("Cities", "Name");
            viewModel.Companies = await _masterDataManager.GetSelectOptionsByTableAsync("Companies", "Name");
            viewModel.NoticeTypes = await _masterDataManager.GetSelectOptionsByTableAsync("NoticeTypes", "Name");
            viewModel.OrgUnits = await _masterDataManager.GetSelectOptionsByTableAsync("OrgUnits", "Name");
            viewModel.Sectors = await _masterDataManager.GetSelectOptionsByTableAsync("Sectors", "Name");
            viewModel.User = GetCurrentUser();

            return viewModel;
        }

        [HttpGet("new", Name = RouteNames.Employees_New)]
        public async Task<IActionResult> NewEmployeeAsync()
        {
            ViewData["EditMode"] = false;
            var viewModel = await GetEmployeeViewModelAsync(new int?());
            viewModel.Employee.Blocked = true;
            return View("EditEmployee", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.Employees_Edit)]
        public async Task<IActionResult> EditEmployeeAsync(int id)
        {
            ViewData["EditMode"] = true;
            var viewModel = await GetEmployeeViewModelAsync(id);
            return View("EditEmployee", viewModel);
        }

        [HttpGet("view/{id}", Name = RouteNames.Employees_View)]
        public async Task<IActionResult> ViewEmployeeAsync(int id)
        {
            var viewModel = await GetEmployeeViewModelAsync(id);

            var employeResponse = await _employeesManager.GetEmployeeByIdAsync(id);

            viewModel.Employee = employeResponse.Payload!;

            // viewModel.Companies = await _masterDataManager.GetFilteredSelectOptionsByTable("Companies", "CompanyMemberId", viewModel.Employee.CompanyId);

            if (viewModel.Employee.StateId.HasValue)
            {
                //var citiesResponse = await _geoLocationManager.GetCitiesByStateIdAsync(viewModel.Employee.StateId.Value);
                //viewModel.Cities = citiesResponse.Payload;
            }
            if (viewModel.Employee.CompanyId > 0)
            {
                viewModel.OrgUnits = await _masterDataManager.GetFilteredSelectOptionsByTable("OrgUnits", "CompanyId", viewModel.Employee.CompanyId.Value);
            }
            viewModel.ReadOnly = 1;

            return View("EditEmployee", viewModel);
        }


        [HttpPost("search", Name = RouteNames.Employees_Search)]
        public async Task<IActionResult> SearchAsync(SearchEmployeesParams searchParams)
        {
            var responseModel = await _employeesManager.SearchEmployeesAsync(searchParams);
            return Json(responseModel.Payload);
        }
        [HttpPost("delete/{id}", Name = RouteNames.Employees_Delete)]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _employeesManager.DeleteEmployeeAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Employees_List)!;
            }
            return Json(responseModel);
        }

        [HttpPost("save", Name = RouteNames.Employees_Save)]
        public async Task<IActionResult> SaveEmployeeAsync(SaveEmployeeRequestModel requestModel, IFormFile? PhotoFile)
        {
            if (PhotoFile != null && PhotoFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(PhotoFile.FileName);
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await PhotoFile.CopyToAsync(stream);
                }

                requestModel.Photo = "/images/" + fileName;
            }
            var responseModel = await _employeesManager.SaveEmployeeAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Employees_List)!;
            }
            return Json(responseModel);
        }


        [HttpGet("orgUnitsByCompany/{id}", Name = RouteNames.Employees_OrgUnitsByCompany)]
        public async Task<IActionResult> GetOrgUnitsByCompanyAsync(int id)
        {
            var makesResponse = await _masterDataManager.GetFilteredSelectOptionsByTable("OrgUnits", "CompanyId", id);
            return Json(makesResponse);
        }

        [HttpGet("nextEmployee/{currentId}", Name = RouteNames.Employees_NextRow)]
        public async Task<IActionResult> GetNextRow(int currentId)
        {
            var response = await _employeesManager.GetNextRowAsync(currentId);
            return Json(response);
        }
        [HttpGet("previousEmployee/{currentId}", Name = RouteNames.Employees_PreviousRow)]
        public async Task<IActionResult> GetPreviousRow(int currentId)
        {
            var response = await _employeesManager.GetPreviousRowAsync(currentId);
            return Json(response);
        }

        #region Employee documents

        [HttpGet("employeeDocuments/{employeeId}", Name = RouteNames.Employees_Documents)]
        public async Task<IActionResult> EmployeeDocumentsAsync(int employeeId)
        {
            var documentTypes = await _masterDataManager.GetSelectOptionsByTableAsync("DocumentTypes", "Name");

            var viewModel = new EmployeeDocumentsViewModel
            {
                DocumentTypes = documentTypes,
                DocumentsDownloadUrl = GetApiDocumentDownloadEnpointUrl()
            };
            var employeeResponse = await _employeesManager.GetEmployeeByIdAsync(employeeId);
            viewModel.EmployeeName = employeeResponse.Payload!.Name + " " + employeeResponse.Payload.Surname;
            viewModel.EmployeeId = employeeId;
            viewModel.User = GetCurrentUser();
            //await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "FmcsaInspections");


            return View("EmployeeDocuments", viewModel);
        }
        #endregion


        #region User messages

        [HttpGet("messagesList/{EmployeeId}", Name = RouteNames.Employees_Messages_List)]
        public async Task<IActionResult> UserMessagesList(int EmployeeId)
        {
            var employeeResponse = await _employeesManager.GetEmployeeByIdAsync(EmployeeId);
            var drivers = await _employeesManager.GetDriversAsSelectOptionsAsync();
            var dispatchers = await _employeesManager.GetEmployeesAsSelectOptionsByJobIdAsync(JobTypes.Dispatcher);

            var viewModel = new MessagesViewModel
            {
                EmployeeId = EmployeeId,
                Drivers = dispatchers.Concat(drivers),
                EmployeeName = employeeResponse.Payload!.Name + " " + employeeResponse.Payload.Surname
            };

            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "UserMessages");
            await UpdateNavigationWithAjaxTableViewModel(viewModel.UserMessagesViewModel, _masterDataManager, "UserMessages");
            await UpdateNavigationWithAjaxTableViewModel(viewModel.EmailsViewModel, _masterDataManager, "EmailQueue");

            return View("UserMessagesIndex", viewModel);
        }

        [HttpPost("searchMessages", Name = RouteNames.Employees_Messages_Search)]
        public async Task<IActionResult> SearchUserMessagesAsync(SearchUserMessagesParams searchParams)
        {
            var responseModel = await _employeesManager.SearchUserMessagesAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpPost("searchEmails", Name = RouteNames.Employees_Emails_Search)]
        public async Task<IActionResult> SearchEmailsAsync(SearchEmailsParams searchParams)
        {
            var responseModel = await _employeesManager.SearchEmailsAsync(searchParams);
            return Json(responseModel.Payload);
        }

        #endregion



    }
}
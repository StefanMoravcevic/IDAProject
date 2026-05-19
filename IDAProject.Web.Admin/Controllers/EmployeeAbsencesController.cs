using IDAProject.Web.Admin.Managers;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.EmployeeAbsences;
using IDAProject.Web.Models.Dto.EmployeeAbsences;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.EmployeeAbsences;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class EmployeeAbsencesController : BaseController
    {
        private readonly IEmployeeAbsencesManager _EmployeeAbsencesManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IEmployeesManager _employeesManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EmployeeAbsencesController(
            ILogger<EmployeeAbsencesController> logger,
            IAccountManager accountManager,
            IStringLocalizer<SharedResources> localizer,
            IEmployeesManager employeesManager,
            IEmployeeAbsencesManager EmployeeAbsencesManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _EmployeeAbsencesManager = EmployeeAbsencesManager;
            _masterDataManager = masterDataManager;
            _localizer = localizer;
            _employeesManager = employeesManager;
        }
        [HttpGet("EmployeeAbsencesList", Name = RouteNames.EmployeeAbsences_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new EmployeeAbsencesViewModel(_localizer);
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "EmployeeAbsences");
            viewModel.JobTypes = await _masterDataManager.GetSelectOptionsByTableAsync("JobTypes", "Name");
            viewModel.Employees = await _employeesManager.GetEmployeesAsSelectOptionsAsync();
            //var responseModel = await _EmployeeAbsencesManager.SearchEmployeeAbsencesAsync();
            //viewModel = EmployeeAbsences.Payload;
            //return Json(responseModel.Payload);
            return View(viewModel);
        }

        [HttpGet("absences/{employeeId}", Name = RouteNames.EmployeeAbsences_ListByEmployeeId)]
        public async Task<IActionResult> AbsencesByEmployeeId(int employeeId)
        {
            var absences = await _EmployeeAbsencesManager.SearchEmployeeAbsencesAsync(new SearchEmployeeAbsencesParams { EmployeeId = employeeId});
            var jobTypeId = (await _employeesManager.GetEmployeeByIdAsync(employeeId)).Payload.JobTypeId;
            var viewModel = new EmployeeAbsenceViewModel
            {
                EmployeeId = employeeId,
                EmployeeAbsences = absences.Payload!,
                JobTypeId = jobTypeId
            };
            viewModel.AbsenceTypes = await _masterDataManager.GetSelectOptionsByTableAsync("AbsenceTypes", "Name");
            return PartialView("EditAbsenceModal", viewModel);
        }
        [HttpGet("absences/records/{employeeId}", Name = RouteNames.EmployeeAbsences_RecordsByEmployeeId)]
        public async Task<IActionResult> GetAbsencesAsync(int employeeId)
        {
            var responseModel = await _EmployeeAbsencesManager.SearchEmployeeAbsencesAsync(new SearchEmployeeAbsencesParams { EmployeeId = employeeId});
            return PartialView("EditAbsenceRecords", responseModel.Payload);
        }


        [HttpPost("search", Name = RouteNames.EmployeeAbsences_Search)]
        public async Task<IActionResult> SearchEmployeeAbsences(SearchEmployeeAbsencesParams searchParams)
        {
            if(searchParams.IsFromHomePage.HasValue && searchParams.IsFromHomePage == true)
            {
                searchParams.Date = DateTime.Now;
            }
            var responseModel = await _EmployeeAbsencesManager.SearchEmployeeAbsencesAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new", Name = RouteNames.EmployeeAbsences_New)]
        public async Task<IActionResult> NewEmployeeAbsenceAsync()
        {
            var viewModel = new EmployeeAbsencesEditViewModel();
            viewModel.AbsenceTypes = await _masterDataManager.GetSelectOptionsByTableAsync("AbsenceTypes", "Name");
            viewModel.JobTypes = await _masterDataManager.GetSelectOptionsByTableAsync("JobTypes", "Name");
            viewModel.Employees = await _employeesManager.GetEmployeesAsSelectOptionsAsync();
            viewModel.User = GetCurrentUser();
            return View("EditEmployeeAbsence", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.EmployeeAbsences_Edit)]
        public async Task<IActionResult> EditEmployeeAbsenceAsync(int id)
        {
            var viewModel = new EmployeeAbsencesEditViewModel();

            var EmployeeAbsenceResponse = await _EmployeeAbsencesManager.GetEmployeeAbsenceByIdAsync(id);
            viewModel.AbsenceTypes = await _masterDataManager.GetSelectOptionsByTableAsync("AbsenceTypes", "Name");
            viewModel.JobTypes = await _masterDataManager.GetSelectOptionsByTableAsync("JobTypes", "Name");
            viewModel.Employees = await _employeesManager.GetEmployeesAsSelectOptionsAsync();
            viewModel.Absence = EmployeeAbsenceResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditEmployeeAbsence", viewModel);
        }

        [HttpPost("save", Name = RouteNames.EmployeeAbsences_Save)]
        public async Task<IActionResult> SaveEmployeeAbsenceAsync(SaveEmployeeAbsenceRequestModel requestModel)
        {
            var user = GetCurrentUser();
            if (requestModel.EmployeeId.HasValue)
            {
                requestModel.EmployeeId = requestModel.EmployeeId;
            }
            else
            {
                requestModel.EmployeeId = user.EmployeeId;
            }
            var responseModel = await _EmployeeAbsencesManager.SaveEmployeeAbsenceAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.EmployeeAbsences_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpDelete("delete/{id}", Name = RouteNames.EmployeeAbsences_Delete)]
        public async Task<IActionResult> DeleteEmployeeAbsenceAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _EmployeeAbsencesManager.DeleteEmployeeAbsenceAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.EmployeeAbsences_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
    }
}

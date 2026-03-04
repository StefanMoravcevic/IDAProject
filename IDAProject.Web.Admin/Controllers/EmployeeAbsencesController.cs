using IDAProject.Web.Admin.Managers;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.EmployeeAbsences;
using IDAProject.Web.Models.Dto.EmployeeAbsences;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.EmployeeAbsences;
using Microsoft.AspNetCore.Mvc;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class EmployeeAbsencesController : BaseController
    {
        private readonly IEmployeeAbsencesManager _EmployeeAbsencesManager;
        private readonly IMasterDataManager _masterDataManager;

        public EmployeeAbsencesController(
            ILogger<EmployeeAbsencesController> logger,
            IAccountManager accountManager,
            IEmployeeAbsencesManager EmployeeAbsencesManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _EmployeeAbsencesManager = EmployeeAbsencesManager;
            _masterDataManager = masterDataManager;
        }
        [HttpGet("EmployeeAbsencesList", Name = RouteNames.EmployeeAbsences_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new EmployeeAbsencesViewModel();
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "EmployeeAbsences");
            //var responseModel = await _EmployeeAbsencesManager.SearchEmployeeAbsencesAsync();
            //viewModel = EmployeeAbsences.Payload;
            //return Json(responseModel.Payload);
            return View(viewModel);
        }

        [HttpGet("absences/{employeeId}", Name = RouteNames.EmployeeAbsences_ListByEmployeeId)]
        public async Task<IActionResult> AbsencesByEmployeeId(int employeeId)
        {
            var absences = await _EmployeeAbsencesManager.SearchEmployeeAbsencesAsync(new SearchEmployeeAbsencesParams { EmployeeId = employeeId});

            var viewModel = new EmployeeAbsenceViewModel
            {
                EmployeeId = employeeId,
                EmployeeAbsences = absences.Payload!,
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
            var responseModel = await _EmployeeAbsencesManager.SearchEmployeeAbsencesAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new", Name = RouteNames.EmployeeAbsences_New)]
        public async Task<IActionResult> NewEmployeeAbsenceAsync(int Id)
        {
            var viewModel = new EmployeeAbsenceViewModel();
            viewModel.AbsenceTypes = await _masterDataManager.GetSelectOptionsByTableAsync("AbsenceTypes", "Name");
            viewModel.User = GetCurrentUser();
            return PartialView("EditEmployeeAbsenceModal", viewModel);
        }

        //[HttpGet("edit/{id}", Name = RouteNames.EmployeeAbsences_Edit)]
        //public async Task<IActionResult> EditEmployeeAbsenceAsync(int id)
        //{
        //    var viewModel = new EmployeeAbsenceViewModel();

        //    var EmployeeAbsenceResponse = await _EmployeeAbsencesManager.GetEmployeeAbsenceByIdAsync(id);

        //    viewModel.EmployeeAbsence = EmployeeAbsenceResponse.Payload!;
        //    viewModel.User = GetCurrentUser();

        //    return View("EditEmployeeAbsence", viewModel);
        //}

        //controller method for saving EmployeeAbsence
        [HttpPost("save", Name = RouteNames.EmployeeAbsences_Save)]
        public async Task<IActionResult> SaveEmployeeAbsenceAsync(SaveEmployeeAbsenceRequestModel requestModel)
        {
            var user = GetCurrentUser();
            requestModel.EmployeeId = user.EmployeeId;
            var responseModel = await _EmployeeAbsencesManager.SaveEmployeeAbsenceAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.EmployeeAbsences_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.EmployeeAbsences_Delete)]
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

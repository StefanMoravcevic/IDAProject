using Microsoft.AspNetCore.Mvc;
using DeclarationFactory.Web.Admin.Models.Common;
using DeclarationFactory.Web.Admin.Models.Interfaces.Managers;
using DeclarationFactory.Web.Admin.Models.ViewModels.Shifts;
using DeclarationFactory.Web.Models.Dto.Shifts;
using DeclarationFactory.Web.Models.General.Enums;
using DeclarationFactory.Web.Models.RequestModels.Shifts;

namespace DeclarationFactory.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class ShiftsController : BaseController
    {
        private readonly IShiftsManager _ShiftsManager;
        private readonly IMasterDataManager _masterDataManager;

        public ShiftsController(
            ILogger<ShiftsController> logger,
            IAccountManager accountManager,
            IShiftsManager ShiftsManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _ShiftsManager = ShiftsManager;
            _masterDataManager = masterDataManager;
        }
        [HttpGet("ShiftsList", Name = RouteNames.Shifts_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ShiftsViewModel();
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "Shifts");
            //var responseModel = await _ShiftsManager.SearchShiftsAsync();
            //viewModel = Shifts.Payload;
            //return Json(responseModel.Payload);
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.Shifts_Search)]
        public async Task<IActionResult> SearchShifts(SearchShiftsParams searchParams)
        {
            var responseModel = await _ShiftsManager.SearchShiftsAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new/{Id}", Name = RouteNames.Shifts_New)]
        public async Task<IActionResult> NewShiftAsync(int Id)
        {
            var viewModel = new ShiftViewModel();

            viewModel.User = GetCurrentUser();
            return View("EditShift", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.Shifts_Edit)]
        public async Task<IActionResult> EditShiftAsync(int id)
        {
            var viewModel = new ShiftViewModel();

            var ShiftResponse = await _ShiftsManager.GetShiftByIdAsync(id);

            viewModel.Shift = ShiftResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditShift", viewModel);
        }

        //controller method for saving Shift
        [HttpPost("save", Name = RouteNames.Shifts_Save)]
        public async Task<IActionResult> SaveShiftAsync(SaveShiftRequestModel requestModel)
        {
            var responseModel = await _ShiftsManager.SaveShiftAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Shifts_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.Shifts_Delete)]
        public async Task<IActionResult> DeleteShiftAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _ShiftsManager.DeleteShiftAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Shifts_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
    }
}

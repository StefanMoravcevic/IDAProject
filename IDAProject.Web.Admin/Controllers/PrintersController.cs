using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.Printers;
using IDAProject.Web.Models.Dto.Printers;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.Printers;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class PrintersController : BaseController
    {
        private readonly IPrintersManager _PrintersManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public PrintersController(
            ILogger<PrintersController> logger,
            IAccountManager accountManager,
            IStringLocalizer<SharedResources> localizer,
            IPrintersManager PrintersManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _PrintersManager = PrintersManager;
            _localizer = localizer;
            _masterDataManager = masterDataManager;
        }
        [HttpGet("PrintersList", Name = RouteNames.Printers_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new PrintersViewModel(_localizer);
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "Printers");
            //var responseModel = await _PrintersManager.SearchPrintersAsync();
            //viewModel = Printers.Payload;
            //return Json(responseModel.Payload);
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.Printers_Search)]
        public async Task<IActionResult> SearchPrinters(SearchPrintersParams searchParams)
        {
            var responseModel = await _PrintersManager.SearchPrintersAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new", Name = RouteNames.Printers_New)]
        public async Task<IActionResult> NewPrinterAsync()
        {
            var viewModel = new PrinterViewModel();

            viewModel.User = GetCurrentUser();
            return View("EditPrinter", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.Printers_Edit)]
        public async Task<IActionResult> EditPrinterAsync(int id)
        {
            var viewModel = new PrinterViewModel();

            var PrinterResponse = await _PrintersManager.GetPrinterByIdAsync(id);

            viewModel.Printer = PrinterResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditPrinter", viewModel);
        }

        [HttpGet("view/{id}", Name = RouteNames.Printers_View)]
        public async Task<IActionResult> ViewPrinterAsync(int id)
        {
            var viewModel = new PrinterViewModel();

            var PrinterResponse = await _PrintersManager.GetPrinterByIdAsync(id);

            viewModel.Printer = PrinterResponse.Payload!;
            viewModel.ReadOnly = 1;
            viewModel.User = GetCurrentUser();

            return View("EditPrinter", viewModel);
        }

        //controller method for saving Printer
        [HttpPost("save", Name = RouteNames.Printers_Save)]
        public async Task<IActionResult> SavePrinterAsync(SavePrinterRequestModel requestModel)
        {
            var responseModel = await _PrintersManager.SavePrinterAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Printers_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }

        [HttpDelete("delete/{id}", Name = RouteNames.Printers_Delete)]
        public async Task<IActionResult> DeletePrinterAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _PrintersManager.DeletePrinterAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Printers_List, new { Id = "111" })!;
            }
            return Json(responseModel);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.DocumentSeries;
using IDAProject.Web.Models.Dto.DocumentSeries;
using IDAProject.Web.Models.RequestModels.DocumentSeries;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class DocumentSeriesController : BaseController
    {
        private readonly IDocumentSeriesManager _documentSeriesManager;
        private readonly IMasterDataManager _masterDataManager;

        public DocumentSeriesController(
            ILogger<DocumentSeriesController> logger,
            IAccountManager accountManager,
            IDocumentSeriesManager documentSeriesManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _documentSeriesManager = documentSeriesManager;
            _masterDataManager = masterDataManager;
        }

        [HttpGet(Name = RouteNames.DocumentSeries_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new DocumentSeriesViewModel();
            var documentSeriesTypes = await _masterDataManager.GetSelectOptionsByTableAsync("DocumentSerieTypes", "Name");
            viewModel.DocumentSerieTypes = documentSeriesTypes;

            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "DocumentSeries");
            return View(viewModel);
        }

        private async Task<DocumentSerieViewModel> GetDocumentSerieViewModelAsync(int? documentSerieId)
        {
            var viewModel = new DocumentSerieViewModel();
            var documentSeriesTypes = await _masterDataManager.GetSelectOptionsByTableAsync("DocumentSerieTypes", "Name");
            viewModel.DocumentSerieTypes = documentSeriesTypes;

            if (documentSerieId.HasValue)
            {
                var documentSerieResponse = await _documentSeriesManager.GetDocumentSerieByIdAsync(documentSerieId.Value);
                viewModel.DocumentSerie = documentSerieResponse.Payload!;

            }

            viewModel.User = GetCurrentUser();

            return viewModel;
        }

        [HttpGet("new", Name = RouteNames.DocumentSeries_New)]
        public async Task<IActionResult> NewDocumentSerieAsync()
        {
            var viewModel = await GetDocumentSerieViewModelAsync(new int?());
            return View("EditDocumentSerie", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.DocumentSeries_Edit)]
        public async Task<IActionResult> EditDocumentSerieAsync(int id)
        {
            var viewModel = await GetDocumentSerieViewModelAsync(id);
            return View("EditDocumentSerie", viewModel);
        }

        [HttpGet("view/{id}", Name = RouteNames.DocumentSeries_View)]
        public async Task<IActionResult> ViewDocumentSerieAsync(int id)
        {
            var viewModel = await GetDocumentSerieViewModelAsync(id);

            var documentSerieResponse = await _documentSeriesManager.GetDocumentSerieByIdAsync(id);

            viewModel.DocumentSerie = documentSerieResponse.Payload!;

            viewModel.ReadOnly = 1;

            return View("EditDocumentSerie", viewModel);
        }

        [HttpPost("search", Name = RouteNames.DocumentSeries_Search)]
        public async Task<IActionResult> SearchAsync(SearchDocumentSeriesParams searchParams)
        {
            var responseModel = await _documentSeriesManager.SearchDocumentSeriesAsync(searchParams);
            return Json(responseModel.Payload);
        }
        [HttpPost("delete/{id}", Name = RouteNames.DocumentSeries_Delete)]
        public async Task<IActionResult> DeleteDocumentSerieAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _documentSeriesManager.DeleteDocumentSerieAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.DocumentSeries_List)!;
            }
            return Json(responseModel);
        }

        [HttpPost("save", Name = RouteNames.DocumentSeries_Save)]
        public async Task<IActionResult> SaveDocumentSerieAsync(SaveDocumentSerieRequestModel requestModel)
        {
            var responseModel = await _documentSeriesManager.SaveDocumentSerieAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.DocumentSeries_List)!;
            }
            return Json(responseModel);
        }

    }
}
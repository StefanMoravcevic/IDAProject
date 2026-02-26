using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.FebiItems;
using IDAProject.Web.Models.Dto.FebiItems;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.FebiItems;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class FebiItemsController : BaseController
    {
        private readonly IFebiItemsManager _FebiItemsManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public FebiItemsController(
            ILogger<FebiItemsController> logger,
            IAccountManager accountManager,
            IFebiItemsManager FebiItemsManager,
            IStringLocalizer<SharedResources> localizer, 
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _FebiItemsManager = FebiItemsManager;
            _masterDataManager = masterDataManager;
            _localizer = localizer;
        }
        [HttpGet("FebiItemsList", Name = RouteNames.FebiItems_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new FebiItemsViewModel(_localizer);
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "FebiItems");
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.FebiItems_Search)]
        public async Task<IActionResult> SearchFebiItems(SearchFebiItemsParams searchParams)
        {
            var responseModel = await _FebiItemsManager.SearchFebiItemsAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new", Name = RouteNames.FebiItems_New)]
        public async Task<IActionResult> NewFebiItemAsync()
        {
            var viewModel = new FebiItemViewModel();
            viewModel.User = GetCurrentUser();
            return View("EditFebiItem", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.FebiItems_Edit)]
        public async Task<IActionResult> EditFebiItemAsync(int id)
        {
            var viewModel = new FebiItemViewModel();
            var FebiItemResponse = await _FebiItemsManager.GetFebiItemByIdAsync(id);
            viewModel.FebiItem = FebiItemResponse.Payload!;
            viewModel.User = GetCurrentUser();
            return View("EditFebiItem", viewModel);
        }

        [HttpPost("save", Name = RouteNames.FebiItems_Save)]
        public async Task<IActionResult> SaveFebiItemAsync(SaveFebiItemRequestModel requestModel)
        {
            var responseModel = await _FebiItemsManager.SaveFebiItemAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.FebiItems_List)!;
            }
            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.FebiItems_Delete)]
        public async Task<IActionResult> DeleteFebiItemAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _FebiItemsManager.DeleteFebiItemAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.FebiItems_List)!;
            }
            return Json(responseModel);
        }
    }
}

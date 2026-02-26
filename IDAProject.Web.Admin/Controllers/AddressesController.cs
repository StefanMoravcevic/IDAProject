using IDAProject.Web.Admin.Controllers;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.Addresses;
using IDAProject.Web.Models.Dto.Addresses;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.Addresses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.Addresses;
using IDAProject.Web.Models.Dto.Addresses;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.Addresses;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class AddressesController : BaseController
    {
        private readonly IAddressesManager _addressesManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public AddressesController(
            ILogger<AddressesController> logger,
            IAccountManager accountManager,
            IStringLocalizer<SharedResources> localizer,
            IAddressesManager addressesManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _addressesManager = addressesManager;
            _masterDataManager = masterDataManager;
            _localizer = localizer;
        }
        [HttpGet("addressesList/{AddressTypeId}/{Id}", Name = RouteNames.Addresses_List)]
        public async Task<IActionResult> Index(int AddressTypeId, int Id)

        {
            var viewModel = new AddressesViewModel(_localizer);
            viewModel.AddressTypeId = AddressTypeId;
            viewModel.Id = Id;
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "Addresses");
            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.Addresses_Search)]
        public async Task<IActionResult> SearchAddresses(SearchAddressesParams searchParams)
        {
            var responseModel = await _addressesManager.SearchAddressesAsync(searchParams);
            return Json(responseModel.Payload);
        }

        [HttpGet("new/{AddressTypeId}/{Id}", Name = RouteNames.Addresses_New)]
        public async Task<IActionResult> NewAddressAsync(int AddressTypeId, int Id)
        {
            var viewModel = new AddressViewModel();
            switch (AddressTypeId)
            {
                case AddressTypes.CompanyAddress:
                    viewModel.Address.CompanyId = Id;
                    break;
                case AddressTypes.PartnerAddress:
                    viewModel.Address.PartnerId = Id;
                    break;
                case AddressTypes.BenefitUserAddress:
                    viewModel.Address.BenefitUserId = Id;
                    break;

            }
            viewModel.Cities = await _masterDataManager.GetSelectOptionsByTableAsync("Cities", "Name");
            viewModel.States = await _masterDataManager.GetSelectOptionsByTableAsync("States", "Name");
            viewModel.ZipCodes = await _masterDataManager.GetSelectOptionsByTableAsync("ZipCodes", "ZipCode1");
            viewModel.User = GetCurrentUser();
            return View("EditAddress", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.Addresses_Edit)]
        public async Task<IActionResult> EditAddressAsync(int id)
        {
            var viewModel = new AddressViewModel();
            viewModel.Cities = await _masterDataManager.GetSelectOptionsByTableAsync("Cities", "Name");
            viewModel.States = await _masterDataManager.GetSelectOptionsByTableAsync("States", "Name");
            viewModel.ZipCodes = await _masterDataManager.GetSelectOptionsByTableAsync("ZipCodes", "ZipCode1");

            var addressResponse = await _addressesManager.GetAddressByIdAsync(id);

            viewModel.Address = addressResponse.Payload!;
            viewModel.User = GetCurrentUser();

            return View("EditAddress", viewModel);
        }

        //controller method for saving address
        [HttpPost("save", Name = RouteNames.Addresses_Save)]
        public async Task<IActionResult> SaveAddressAsync(SaveAddressRequestModel requestModel)
        {
            var responseModel = await _addressesManager.SaveAddressAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Addresses_List, new { AddressTypeId = "xxx", Id = "yyy" })!;
            }

            return Json(responseModel);
        }

        [HttpPost("delete/{id}", Name = RouteNames.Addresses_Delete)]
        public async Task<IActionResult> DeleteAddressAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _addressesManager.DeleteAddressAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Addresses_List, new { AddressTypeId = "xxx", Id = "yyy" })!;
            }
            return Json(responseModel);
        }
    }
}

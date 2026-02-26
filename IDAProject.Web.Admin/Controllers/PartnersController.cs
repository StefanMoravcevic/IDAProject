using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.TagHelpers;
using IDAProject.Web.Admin.Models.ViewModels.Partners;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.Partners;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.Partners;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class PartnersController : BaseController
    {
        private readonly IPartnersManager _partnersManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IContactsManager _contactsManager;
        private readonly ICompaniesManager _companiesManager;

        public PartnersController(
            ILogger<PartnersController> logger,
            IAccountManager accountManager,
            IPartnersManager partnersManager,
            IContactsManager contactsManager,
            IMasterDataManager masterDataManager,
            ICompaniesManager companiesManager
            )
            : base(accountManager, logger)
        {
            _partnersManager = partnersManager;
            _contactsManager = contactsManager;
            _masterDataManager = masterDataManager;
            _companiesManager = companiesManager;
        }

        [HttpGet("{partnerCategoryId}", Name = RouteNames.Partners_List)]
        public async Task<IActionResult> Index(int partnerCategoryId)
        {

            var viewModel = new PartnersViewModel();
            if (partnerCategoryId > 0)
            {
                viewModel.PartnerCategoryId = partnerCategoryId;
            }
            viewModel.User = GetCurrentUser();

            try
            {
                var hiddenColumnsResponse = await _masterDataManager.GetTableSettingsAsync(viewModel.User.Id, "Partners");
                if (hiddenColumnsResponse.Valid)
                {
                    viewModel.TableSettings = hiddenColumnsResponse.Payload!;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Route: {RouteNames.Partners_List}");
                viewModel.Notification = new NotificationViewModel
                {
                    Message = e.Message,
                    Type = NotificationType.Error
                };
            }

            return View(viewModel);
        }

        private async Task<PartnerViewModel> GetPartnerViewModelAsync(PartnerDto par)
        {
            var viewModel = new PartnerViewModel();
            viewModel.PartnerCategories = await _masterDataManager.GetSelectOptionsByTableAsync("PartnerCategories", "Name");
            viewModel.PartnerTypes = await _masterDataManager.GetFilteredSelectOptionsByTable("PartnerTypes", "PartnerCategoryId", par.PartnerCategoryId, "Name");
            viewModel.IncomeTypes = await _masterDataManager.GetSelectOptionsByTableAsync("CostIncomeTypes", "Name");
            viewModel.PaymentConditions = await _masterDataManager.GetSelectOptionsByTableAsync("PaymentConditions", "Name");
            viewModel.States = await _masterDataManager.GetSelectOptionsByTableAsync("States", "Name");

            //var contactsResponse = await _contactsManager.SearchContactsAsync(new SearchContactsParams());
            //viewModel.PrimaryContacts = contactsResponse.Payload.Where(z=>z.PartnerId==par.Id).Select(x => new GenericSelectOption
            //{
            //    Value = x.Id,
            //    Description = x.IsCompany ? x.CompanyName! : x.Name! + " " + x.MiddleName! + " " + x.LastName!
            //});

            if (par.StateId.HasValue)
            {
                //var citiesResponseModel = await _geoLocationManager.GetCitiesByStateIdAsync(par.StateId.Value);
                //viewModel.Cities = citiesResponseModel.Payload;
            }
            viewModel.PrimaryContacts = await _contactsManager.GetContactsAsSelectOptionsAsync(null, par.ContactCompanyId, null, false);
            viewModel.User = GetCurrentUser();
            return viewModel;
        }

        [HttpGet("new", Name = RouteNames.Partners_New)]
        public async Task<IActionResult> NewPartnerAsync()
        {
            var viewModel = await GetPartnerViewModelAsync(new PartnerDto());

            if (viewModel.PartnerCategories.Count() >= 1)
            {
                var partnerCategory = viewModel.PartnerCategories.First();
                viewModel.Partner.PartnerCategoryId = partnerCategory!.Value!.Value;
                viewModel.PartnerTypes = await _masterDataManager.GetFilteredSelectOptionsByTable("PartnerTypes", "PartnerCategoryId", viewModel.Partner.PartnerCategoryId);
            }

            viewModel.Partner.Blocked = false;
            return View("EditPartner", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.Partners_Edit)]
        public async Task<IActionResult> EditPartnerAsync(int id)
        {
            var partnerResponse = await _partnersManager.GetPartnerByIdAsync(id);
            var viewModel = await GetPartnerViewModelAsync(partnerResponse.Payload!);

            viewModel.Partner = partnerResponse.Payload!;

            return View("EditPartner", viewModel);
        }

        [HttpGet("from-contact/{contactId}/{categoryId}", Name = RouteNames.Partner_FromContact)]
        public async Task<IActionResult> CreatePartnerFromContactAsync(int contactId, int categoryId)
        {
            var contactResponse = await _contactsManager.GetContactByIdAsync(contactId);
            var contactDto = contactResponse.Payload!;

            var partner = new PartnerDto
            {
                Address = contactDto.Address,
                Fax = contactDto.Fax,
                City = contactDto.City,
                State = contactDto.State,
                StateId = contactDto.StateId,
                CityId = contactDto.CityId,
                ZipCode = contactDto.ZipCode,
                ZipCodeId = contactDto.ZipCodeId,
                Phone = contactDto.Phone,
                Blocked = false,
                Name = contactDto.Name,
                Email = contactDto.Email,
                PartnerCategoryId = categoryId,
                ContactCompanyId = contactId,
                Ein = contactDto.Ein,
                Dot = contactDto.Dot,
                Mc = contactDto.Mc
            };

            var viewModel = await GetPartnerViewModelAsync(partner);
            viewModel.Partner = partner;

            return View("EditPartner", viewModel);
        }

        [HttpGet("view/{id}", Name = RouteNames.Partners_View)]
        public async Task<IActionResult> ViewPartnerAsync(int id)
        {
            var partnerResponse = await _partnersManager.GetPartnerByIdAsync(id);
            var viewModel = await GetPartnerViewModelAsync(partnerResponse.Payload!);

            viewModel.Partner = partnerResponse.Payload!;
            viewModel.ReadOnly = 1;

            return View("EditPartner", viewModel);
        }


        [HttpPost("search", Name = RouteNames.Partners_Search)]
        public async Task<IActionResult> SearchAsync(SearchPartnersParams searchParams)
        {
            var responseModel = await _partnersManager.SearchPartnersAsync(searchParams);
            return Json(responseModel.Payload);
        }


        [HttpPost("save", Name = RouteNames.Partners_Save)]
        public async Task<IActionResult> SavePartnerAsync(SavePartnerRequestModel requestModel)
        {
            var responseModel = await _partnersManager.SavePartnerAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Partners_List)!;
            }
            return Json(responseModel);
        }

        [HttpGet("typesByCategory/{id}", Name = RouteNames.Partners_TypesByCategory)]
        public async Task<IActionResult> GetPartnerTypesByCategoryAsync(int id)
        {
            var catResponse = await _masterDataManager.GetFilteredSelectOptionsByTable("PartnerTypes", "PartnerCategoryId", id, "Name");
            return Json(catResponse);
        }

        [HttpPost("delete/{id}", Name = RouteNames.Partners_Delete)]
        public async Task<IActionResult> DeletePartnerAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _partnersManager.DeletePartnerAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Partners_List)!;
            }
            return Json(responseModel);
        }

        [HttpGet("contactsByPartner/{id}", Name = RouteNames.Partners_Contacts)]
        public async Task<IActionResult> GetPartnerContactsAsync(int id)
        {
            //var user = GetCurrentUser();
            //var contactsResponse = await _contactsManager.GetContactsAsSelectOptionsAsync(user.CompanyId, id, null);
            var partner = await _partnersManager.GetPartnerByIdAsync(id);
            IEnumerable<ISelectOption> contactsResponse;
            if (partner.Payload!.ContactCompanyId.HasValue)
            {
                contactsResponse = await _contactsManager.GetContactsAsSelectOptionsAsync(null, partner.Payload!.ContactCompanyId, null, false);
            }
            else
            {
                contactsResponse = await _contactsManager.GetContactsAsSelectOptionsAsync(null, null, id, false);
            }
            return Json(contactsResponse);
        }

    }
}
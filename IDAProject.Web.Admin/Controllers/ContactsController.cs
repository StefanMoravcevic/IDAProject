using IDAProject.Web.Admin.Controllers;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.Contacts;
using IDAProject.Web.Models.General;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels.Contacts;
using IDAProject.Web.Models.Dto.Contacts;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Contacts;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class ContactsController : BaseController
    {
        private readonly IMasterDataManager _masterDataManager;
        private readonly IContactsManager _contactsManager;

        public ContactsController(
            ILogger<ContactsController> logger,
            IAccountManager accountManager,
            IMasterDataManager masterDataManager,
            IContactsManager contactsManager
            )
            : base(accountManager, logger)
        {
            _masterDataManager = masterDataManager;
            _contactsManager = contactsManager;
        }


        [HttpGet(Name = RouteNames.Contacts_List)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ContactsViewModel();
            viewModel.PartnerCategories = await _masterDataManager.GetSelectOptionsByTableAsync("PartnerCategories", "Name");
            await UpdateNavigationWithAjaxTableViewModel(viewModel, _masterDataManager, "Contacts");

            return View(viewModel);
        }

        [HttpPost("search", Name = RouteNames.Contacts_Search)]
        public async Task<IActionResult> SearchAsync(SearchContactsParams searchParams)
        {
            var currentUser = GetCurrentUser();
            searchParams.CompanyId = currentUser.CompanyId;
            var responseModel = await _contactsManager.SearchContactsAsync(searchParams);
            foreach (var con in responseModel.Payload)
            {
                if (con.IsCompany)
                {
                    con.ContactCompanyForSorting = con.CompanyName!;
                }
                else
                {
                    con.ContactCompanyForSorting = con.ContactCompany!;
                }
            }
            var contactsList = responseModel.Payload.OrderBy(x => x.ContactCompanyForSorting).ThenByDescending(z => z.IsCompany).ToList();
            return Json(contactsList);
        }

        [HttpGet("new", Name = RouteNames.Contacts_New)]
        public async Task<IActionResult> NewContactAsync()
        {
            var viewModel = await GetContactViewModelAsync(new ContactDto());
            return View("EditContact", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.Contacts_Edit)]
        public async Task<IActionResult> EditContactAsync(int id)
        {
            var response = await _contactsManager.GetContactByIdAsync(id);
            var viewModel = await GetContactViewModelAsync(response.Payload!);
            return View("EditContact", viewModel);
        }

        [HttpGet("view/{id}", Name = RouteNames.Contacts_View)]
        public async Task<IActionResult> ViewContactAsync(int id)
        {
            var response = await _contactsManager.GetContactByIdAsync(id);
            var viewModel = await GetContactViewModelAsync(response.Payload!);

            viewModel.ReadOnly = 1;

            return View("EditContact", viewModel);
        }

        private async Task<ContactViewModel> GetContactViewModelAsync(ContactDto contact)
        {
            var viewModel = new ContactViewModel
            {
                Contact = contact
            };

            viewModel.States = await _masterDataManager.GetSelectOptionsByTableAsync("States", "Name");
            viewModel.Genders = await _masterDataManager.GetSelectOptionsByTableAsync("Genders", "Name");
            viewModel.PartnerCategories = await _masterDataManager.GetSelectOptionsByTableAsync("PartnerCategories", "Name");

            if (contact.StateId.HasValue)
            {
                //var citiesResponseModel = await _geoLocationManager.GetCitiesByStateIdAsync(contact.StateId.Value);
                //viewModel.Cities = citiesResponseModel.Payload;
            }

            viewModel.Partners = await _masterDataManager.GetSelectOptionsByTableAsync("Partners", "Name");
            viewModel.ContactCompanies = await _masterDataManager.GetFilteredSelectOptionsByTable("Contacts", "IsCompany", 1, "CompanyName");

            viewModel.MethodsOfCommunication = new List<GenericSelectOption>
            {
                new GenericSelectOption { Value = 1, Description = "Phone" },
                new GenericSelectOption { Value = 2, Description = "Email"}
            };

            viewModel.User = GetCurrentUser();
            return viewModel;
        }

        [HttpPost("save", Name = RouteNames.Contacts_Save)]
        public async Task<IActionResult> SaveContactAsync(SaveContactRequestModel requestModel)
        {
            if (requestModel.Id == 0)
            {
                var currentUser = GetCurrentUser();
                requestModel.CompanyId = currentUser.CompanyId;
            }

            var responseModel = await _contactsManager.SaveContactAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Contacts_List)!;
            }
            return Json(responseModel);
        }

    }
}
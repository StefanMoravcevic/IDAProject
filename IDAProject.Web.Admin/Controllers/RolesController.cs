using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.TagHelpers;
using IDAProject.Web.Admin.Models.ViewModels.Roles;
using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Roles;
using IDAProject.Web.Models.RequestModels.Roles;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class RolesController : BaseController
    {
        private readonly IRolesManager _rolesManager;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public RolesController(
            ILogger<RolesController> logger,
            IAccountManager accountManager,
            IStringLocalizer<SharedResources> localizer,
            IRolesManager rolesManager,
            IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _rolesManager = rolesManager;
            _masterDataManager = masterDataManager;
            _localizer = localizer;
        }

        [HttpGet(Name = RouteNames.Roles_List)]
        public async Task<IActionResult> Index()
        {
            
            var viewModel = new RolesViewModel(_localizer);
            viewModel.User = GetCurrentUser();

            try
            {
                var hiddenColumnsResponse = await _masterDataManager.GetTableSettingsAsync(viewModel.User.Id, "Roles");
                if (hiddenColumnsResponse.Valid)
                {
                    viewModel.TableSettings = hiddenColumnsResponse.Payload!;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Route: {RouteNames.Roles_List}");
                viewModel.Notification = new NotificationViewModel
                {
                    Message = e.Message,
                    Type = NotificationType.Error
                };
            }

            return View(viewModel);
        }

        private async Task<RoleViewModel> GetRoleViewModelAsync(RoleDto role)
        {
            var viewModel = new RoleViewModel();
            viewModel.Features = await _masterDataManager.GetSelectOptionsByTableAsync("AspNetFeatures", "Name");
            viewModel.Companies = await _masterDataManager.GetSelectOptionsByTableAsync("Companies", "Name");
            //viewModel.RoleFeatures = await _masterDataManager.GetFilteredSelectOptionsByTable("AspNetRoleFeatures", "RoleId", par.PartnerCategoryId, "Name");
            viewModel.User = GetCurrentUser();
            return viewModel;
        }

        [HttpGet("new", Name = RouteNames.Role_New)]
        public async Task<IActionResult> NewRoleAsync()
        {
            var viewModel = await GetRoleViewModelAsync(new RoleDto());

            return PartialView("EditRole", viewModel);
        }

        [HttpGet("edit/{id}", Name = RouteNames.Role_Edit)]
        public async Task<IActionResult> EditRoleAsync(int id)
        {
            var roleResponse = await _rolesManager.GetRoleByIdAsync(id);
            var viewModel = await GetRoleViewModelAsync(roleResponse.Payload!);

            viewModel.Role = roleResponse.Payload!;

            return PartialView("EditRole", viewModel);
        }

        [HttpPost("search", Name = RouteNames.Roles_Search)]
        public async Task<IActionResult> SearchAsync(SearchRolesParams searchParams)
        {
            var responseModel = await _rolesManager.SearchRolesAsync(searchParams);
            return Json(responseModel.Payload);
        }
        [HttpPost("delete/{id}", Name = RouteNames.Role_Delete)]
        public async Task<IActionResult> DeleteRoleAsync(int id)
        {
            var user = GetCurrentUser();
            var responseModel = await _rolesManager.DeleteRoleAsync(id, user.Id);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Roles_List)!;
            }
            return Json(responseModel);
        }


        [HttpPost("save", Name = RouteNames.Role_Save)]
        public async Task<IActionResult> SaveRoleAsync(SaveRoleRequestModel requestModel)
        {
            var responseModel = await _rolesManager.SaveRoleAsync(requestModel);
            if (responseModel.Valid)
            {
                responseModel.Message = Url.RouteUrl(RouteNames.Roles_List)!;
            }
            return Json(responseModel);
        }
        [HttpGet("manageFeatures/{id}", Name = RouteNames.Role_ManageFeatures)]
        public async Task<IActionResult> GetFeaturesManageModalAsync(int id)
        {
            try
            {
                var roleResponse = await _rolesManager.GetRoleByIdAsync(id);
                var viewModel = new RoleViewModel();
                viewModel.Features = await _masterDataManager.GetSelectOptionsByTableAsync("AspNetFeatures", "Name");
                var roleFResponse = await _rolesManager.SearchRoleFeaturesAsync(id);
                viewModel.RoleFeatures = roleFResponse.Payload;
                viewModel.Role = roleResponse.Payload!;

                return PartialView("EditRoleFeaturesModal", viewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"id:{id}");
                throw;
            }
        }
        [HttpPost("roleFeatureCreate", Name = RouteNames.RoleFeature_Create)]
        public async Task<IActionResult> CreateRoleFeatureAsync(CreateRoleFeatureModel requestModel)
        {
            try
            {
                var response = await _rolesManager.CreateRoleFeatureAsync(requestModel);
                return Json(response);
            }
            catch (Exception e)
            {
                var requestData = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, requestData);
                throw;
            }
        }
        [HttpDelete("roleFeatureDelete/{roleFeatureId}", Name = RouteNames.RoleFeature_Delete)]
        public async Task<IActionResult> DeleteRoleFeatureAsync(int roleFeatureId)
        {
            try
            {
                var response = await _rolesManager.DeleteRoleFeatureAsync(new DeleteRoleFeatureModel() { RoleFeatureId = roleFeatureId });
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, roleFeatureId.ToString());
                throw;
            }
        }
        [HttpGet("roleFeatures/records/{roleId}", Name = RouteNames.RoleFeatures_Get)]
        public async Task<IActionResult> GetRoleFeaturesRecordsAsync(int roleId)
        {
            var responseModel = await _rolesManager.SearchRoleFeaturesAsync(roleId);
            return PartialView("RoleFeaturesRecords", responseModel.Payload);
        }

    }
}
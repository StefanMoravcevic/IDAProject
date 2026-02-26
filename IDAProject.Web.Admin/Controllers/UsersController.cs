using IDAProject.Web.Admin.Managers;
using IDAProject.Web.Admin.Managers.Attributes;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.TagHelpers;
using IDAProject.Web.Admin.Models.ViewModels.Roles;
using IDAProject.Web.Admin.Models.ViewModels.Users;
using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.AspNetUserOrgUnits;
using IDAProject.Web.Models.Dto.Users;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.RequestModels.Employees;
using IDAProject.Web.Models.RequestModels.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace IDAProject.Web.Admin.Controllers
{
	[Route("[controller]")]
    [CustomAuthorization((int)AspNetRoles.Any, (int)AspNetFeatures.User_Edit)]

    public class UsersController : BaseController
	{
		private readonly IUsersManager _usersManager;
		private readonly IMasterDataManager _masterDataManager;
		private readonly IPartnersManager _partnersManager;
		private readonly IEmployeesManager _employeesManager;
		private readonly IStringLocalizer<SharedResources> _localizer;

		public UsersController(
			ILogger<UsersController> logger,
			IAccountManager accountManager,
			IUsersManager usersManager,
			IStringLocalizer<SharedResources> localizer,
            IPartnersManager partnersManager,
			IEmployeesManager employeesManager,
			IMasterDataManager masterDataManager)
			: base(accountManager, logger)
		{
			_usersManager = usersManager;
			_masterDataManager = masterDataManager;
            _partnersManager = partnersManager;
			_localizer = localizer;
			_employeesManager = employeesManager;
		}

		[HttpGet(Name = RouteNames.Users_List)]
		public async Task<IActionResult> Index()
		{
			var viewModel = new UsersViewModel();
			viewModel.User = GetCurrentUser();

			try
			{
				var searchParams = new SearchUsersParams();
				var masterDataResponse = await _usersManager.SearchUsersAsync(searchParams);

				if (masterDataResponse.Valid)
				{
					viewModel.Users = masterDataResponse.Payload;
				}
				else
				{
					viewModel.Notification = new NotificationViewModel
					{
						Message = masterDataResponse.Message,
						Type = NotificationType.Error
					};
				}

				//var hiddenColumnsResponse = await _masterDataManager.GetTableSettingsAsync(viewModel.User.Id, "Users");
				//if(hiddenColumnsResponse.Valid)
				//{
				//    viewModel.TableSettings = hiddenColumnsResponse.Payload!;
				//}
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"Route: {RouteNames.Users_List}");
				viewModel.Notification = new NotificationViewModel
				{
					Message = e.Message,
					Type = NotificationType.Error
				};
			}

			return View(viewModel);
		}
		[HttpGet("{id}", Name = RouteNames.User_Edit)]
		public async Task<IActionResult> GetEditModalAsync(int id)
		{
			try
			{
				var userResponse = await _usersManager.GetUserByIdAsync(id);
				var model = new UserViewModel
				{
					UserData = userResponse.Payload!
				};

				//var employees = await _employeesManager.SearchEmployeesAsync(new SearchEmployeesParams());
				var listEmp = new List<GenericSelectOption>();
				//if (employees != null)
				//{
				//	listEmp.Add(new GenericSelectOption() { Value = 0, Description = "---" });
				//	foreach (var employee in employees.Payload)
				//	{
				//		listEmp.Add(new GenericSelectOption() { Value = employee.Id, Description = employee.Name + " " + employee.Surname });
				//	}
				//}
				//model.Employees = listEmp;
                //model.Partners = (await _partnersManager.GetPartnersOptionsByCategoryAsync(new List<int>())).Payload;
                model.Roles = await _masterDataManager.GetSelectOptionsByTableAsync("AspNetRoles", "Name");
                model.Employees = await _employeesManager.GetEmployeesAsSelectOptionsAsync();
                var userRoles = await _usersManager.GetRolesByUserIdAsync(id);
				var listUsrRls = new List<GenericSelectOption>();
				if (userRoles != null)
				{
					//listUsrRls.Add(new GenericSelectOption() { Value = 0, Description = "---" });
					foreach (var role in userRoles.Payload)
					{
						listUsrRls.Add(new GenericSelectOption() { Value = role.Id, Description = role.RoleId + " " + role.RoleName });
					}
				}
				model.UserRoles = listUsrRls;
                model.Cultures = new List<GenericSelectOption> {
                    new GenericSelectOption { Value = 1, Description = "sr-Latn" },
                    new GenericSelectOption { Value = 2, Description = "sr-Cyrl" },
                    new GenericSelectOption { Value = 3, Description = "en-US" }
                };
                model.OrgUnits = (await _masterDataManager.GetSelectOptionsByTableAsync("OrgUnits", "Name"));
                model.Printers = (await _masterDataManager.GetSelectOptionsByTableAsync("Printers", "Name"));

                return PartialView("EditUserModalContent", model);
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"id:{id}");
				throw;
			}
		}
		[HttpGet("manageRoles/{id}", Name = RouteNames.User_ManageRoles)]
		public async Task<IActionResult> GetRolesManageModalAsync(int id)
		{
			try
			{
				var userResponse = await _usersManager.GetUserByIdAsync(id);

				var model = new UserViewModel
				{
					UserData = userResponse.Payload!
				};

				var listRoles = new List<GenericSelectOption>();
				listRoles.Add(new GenericSelectOption() { Value = 0, Description = "---" });
				var roles = await _masterDataManager.GetSelectOptionsByTableAsync("AspNetRoles", "Name");
				foreach (var role in roles)
				{
					listRoles.Add(new GenericSelectOption() { Value = role.Value, Description = role.Description });
				}
				model.Roles = listRoles;

				var userRoles = await _usersManager.GetRolesByUserIdAsync(id);
				var listUsrRls = new List<GenericSelectOption>();
				if (userRoles != null)
				{
					foreach (var role in userRoles.Payload)
					{
						listUsrRls.Add(new GenericSelectOption() { Value = role.Id, Description = role.RoleName });
					}
				}
				model.UserRoles = listUsrRls;
				return PartialView("EditUserRolesModal", model);
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"id:{id}");
				throw;
			}
		}
        [HttpGet("manageOrgUnits/{id}", Name = RouteNames.User_ManageOrgUnits)]
        public async Task<IActionResult> GetOrgUnitsManageModalAsync(int id)
        {
            try
            {
                var userResponse = await _usersManager.GetUserByIdAsync(id);

                var model = new UserViewModel
                {
                    UserData = userResponse.Payload!
                };

                var listOrgUnits = new List<GenericSelectOption>();
                listOrgUnits.Add(new GenericSelectOption() { Value = 0, Description = "---" });
                var orgUnits = await _masterDataManager.GetSelectOptionsByTableAsync("OrgUnits", "Name");
                foreach (var orgUnit in orgUnits)
                {
                    listOrgUnits.Add(new GenericSelectOption() { Value = orgUnit.Value, Description = orgUnit.Description });
		}
                model.UserOrgUnits = listOrgUnits;

                var userOrgUnits = await _usersManager.GetOrgUnitsByUserIdAsync(id);
                var listUsrOrgUnits = new List<GenericSelectOption>();
                if (userOrgUnits != null)
                {
                    foreach (var orgUnit in userOrgUnits.Payload)
                    {
                        listUsrOrgUnits.Add(new GenericSelectOption() { Value = orgUnit.Id, Description = orgUnit.OrgUnit });
                    }
                }
                model.OrgUnits = listUsrOrgUnits;
                return PartialView("EditUserOrgUnitsModal", model);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"id:{id}");
                throw;
            }
        }
        [HttpGet("new", Name = RouteNames.User_New)]
		public async Task<IActionResult> NewUserAsync()
		{
			var viewModel = new UserViewModel();
			//var employees = await _employeesManager.SearchEmployeesAsync(new SearchEmployeesParams());
			var listEmp = new List<GenericSelectOption>();

			//if (employees != null)
			//{
			//	foreach (var employee in employees.Payload)
			//	{
			//		listEmp.Add(new GenericSelectOption() { Value = employee.Id, Description = employee.Name + " " + employee.Surname });
			//	}
			//}
			//viewModel.Employees = listEmp;
            //viewModel.Partners = (await _partnersManager.GetPartnersOptionsByCategoryAsync(new List<int>())).Payload;
            viewModel.Cultures = new List<GenericSelectOption> {
                new GenericSelectOption { Value = 1, Description = "sr-Latn" },
                new GenericSelectOption { Value = 2, Description = "sr-Cyrl" },
                new GenericSelectOption { Value = 3, Description = "en-US" }
            };
            viewModel.OrgUnits = (await _masterDataManager.GetSelectOptionsByTableAsync("OrgUnits", "Name"));
			viewModel.Employees = await _employeesManager.GetEmployeesAsSelectOptionsAsync();
            viewModel.Printers = (await _masterDataManager.GetSelectOptionsByTableAsync("Printers", "Name"));

            return PartialView("EditUserModalContent", viewModel);
		}


		[HttpPost("search", Name = RouteNames.Users_Search)]
		public async Task<IActionResult> SearchAsync(SearchUsersParams searchParams)
		{
			var responseModel = await _usersManager.SearchUsersAsync(searchParams);

			var viewModel = new UsersViewModel
			{
				Users = responseModel.Payload
			};

			viewModel.User = GetCurrentUser();

			return PartialView("UsersList", viewModel);
		}



        [HttpPost("save", Name = RouteNames.User_Save)]
		public async Task<IActionResult> SaveUserAsync(SaveUserRequestModel requestModel)
		{
			try
			{
				var responseModel = await _usersManager.SaveUserAsync(requestModel);
				if (responseModel.Valid)
				{
					responseModel.Message = Url.RouteUrl(RouteNames.Users_List)!;
				}
				//return PartialView("UsersList", responseModel);
				return Json(responseModel);
			}
			catch (Exception e)
			{
				var requestData = JsonConvert.SerializeObject(requestModel);
				_logger.LogError(e, requestData);
				throw;
			}
		}



        [HttpPost(Name = RouteNames.User_Create)]
		public async Task<IActionResult> CreateAsync(CreateUserRequestModel requestModel)
		{
			try
			{
				var createModel = new CreateUserModel()
				{
                    UserName = requestModel.UserName,
                    Password = requestModel.Password,
                    EmployeeId = requestModel.EmployeeId,
                    PartnerId = requestModel.PartnerId,
					OrgId = requestModel.OrgId,
                    Active = requestModel.IsActive,
                    PhoneNumber = requestModel.PhoneNumber,
                    PhoneNumberConfirmed = requestModel.PhoneNumberConfirmed,
                    EmailConfirmed = requestModel.EmailConfirmed,
                    UserCulture = requestModel.UserCulture,
                    Email = requestModel.Email
                };
				var response = await _usersManager.CreateUserAsync(createModel);
				return Json(response);
			}
			catch (Exception e)
			{
				var requestData = JsonConvert.SerializeObject(requestModel);
				_logger.LogError(e, requestData);
				throw;
			}
		}

		//[HttpGet("email_phonebyemployee/{employeeId}", Name = RouteNames.User_EmailPhone)]
		//public async Task<IActionResult> GetEmailPhoneByEmployeeIdAsync(int employeeId)
		//{
		//	try
		//	{
		//		var response = await _usersManager.GetEmailPhoneByEmployeeIdAsync(employeeId);
		//		return Json(response.Payload);
		//	}
		//	catch (Exception e)
		//	{
		//		_logger.LogError(e, $"employeeId: {employeeId}");
		//		throw;
		//	}
		//}

		[HttpPost("userRoleCreate", Name = RouteNames.UserRole_Create)]
		public async Task<IActionResult> CreateUserRoleAsync(CreateUserRoleModel requestModel)
		{
			try
			{
				var response = await _usersManager.CreateUserRoleAsync(requestModel);
				return Json(response);
			}
			catch (Exception e)
			{
				var requestData = JsonConvert.SerializeObject(requestModel);
				_logger.LogError(e, requestData);
				throw;
			}
		}

		[HttpPost("userRoleDelete/{userRoleId}", Name = RouteNames.UserRole_Delete)]
		public async Task<IActionResult> DeleteUserRoleAsync(int userRoleId)
		{
			try
			{
				var response = await _usersManager.DeleteUserRoleAsync(new DeleteUserRoleModel() { UserRoleId = userRoleId });
				return Json(response);
			}
			catch (Exception e)
			{
				_logger.LogError(e, userRoleId.ToString());
				throw;
			}
		}

        [HttpPost("userOrgUnitCreate", Name = RouteNames.UserOrgUnits_Create)]
        public async Task<IActionResult> CreateUserOrgUniAsync(SaveAspNetUserOrgUnitRequestModel requestModel)
        {
            try
            {
                var response = await _usersManager.SaveAspNetUserOrgUnitAsync(requestModel);
                return Json(response);
            }
            catch (Exception e)
            {
                var requestData = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, requestData);
                throw;
            }
        }

        [HttpPost("userOrgUnitDelete/{id}", Name = RouteNames.UserOrgUnits_Delete)]
        public async Task<IActionResult> DeleteUserOrgUnitAsync(int id)
        {
            var user = GetCurrentUser();
            try
            {
                var response = await _usersManager.DeleteAspNetUserOrgUnitAsync(id, user.Id);
                return Json(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, id.ToString());
                throw;
            }
        }



    }
}

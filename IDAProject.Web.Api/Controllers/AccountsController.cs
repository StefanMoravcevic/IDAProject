using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Auth;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Users;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Users;
using IDAProject.Web.Api.Repositories.QueryableExtension;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ISecurityManager _securityManager;
        private readonly IUsersManager _usersManager;
        private readonly IRolesManager _rolesManager;
        private readonly ILdapManager _ldapManager;
        private readonly INotificationsManager _notificationsManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;

        public AccountsController(UserManager<AppIdentityUser> userManager, IConfiguration configuration,
            SignInManager<AppIdentityUser> signInManager,
            ISecurityManager securityManager, IUsersManager usersManager, IRolesManager rolesManager,
            INotificationsManager notificationsManager, ILdapManager ldapManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _securityManager = securityManager;
            _usersManager = usersManager;
            _rolesManager = rolesManager;
            _ldapManager = ldapManager;
            _notificationsManager = notificationsManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GenerateToken")]
        public async Task<IActionResult> GenerateTokenAsync([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                bool useLdap = _configuration.GetValue<bool>("LDAPSettings:UseLdap");

                var isValidPassword = useLdap
                    ? _ldapManager.CheckLdapPasswordAsync(user, model.Password)
                    : await _userManager.CheckPasswordAsync(user, model.Password);


                if (isValidPassword && user.IsActive)
                {
                    var userRoles = await _usersManager.GetRolesByUserIdAsync(user.Id);
                    List<string> userRolesList = userRoles.Payload.Select(dto => dto.RoleName).ToList();
                    var userFeaturesList = new List<string>();
                    foreach (var role in userRoles.Payload)
                    {
                        var roleFeatures = await _rolesManager.GetRoleFeaturesByRoleIdAsync(role.RoleId);
                        var featuresList = roleFeatures.Payload.Select(dto => dto.FeatureName).ToList();
                        userFeaturesList.AddRange(featuresList);
                    }

                    var responseModel = new ResponseModel<string>();

                    responseModel.Payload =
                        await _securityManager.GenerateTokenAsync(user, userRolesList, userFeaturesList);
                    responseModel.Valid = true;
                    return new JsonResult(responseModel);
                }
            }

            return Unauthorized();
        }

        [HttpPost("createUser")]
        public async Task<ResponseModelBase> CreateUserAsync(CreateUserModel requestModel)
        {
            var result = new ResponseModelBase();
            var newUser = new AppIdentityUser
            {
                UserName = requestModel.UserName,
                EmployeeId = requestModel.EmployeeId,
                OrgId = requestModel.OrgId,
                PhoneNumber = requestModel.PhoneNumber,
                PhoneNumberConfirmed = requestModel.PhoneNumberConfirmed,
                Email = requestModel.Email,
                EmailConfirmed = requestModel.EmailConfirmed,
                UserCulture = requestModel.UserCulture,
                PartnerId = requestModel.PartnerId
            };

            var identityResult = await _userManager.CreateAsync(newUser, requestModel.Password);

            if (identityResult.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(requestModel.UserName);
                await _userManager.AddToRolesAsync(user, requestModel.Roles);

                result.Valid = true;
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    if (result.Message == string.Empty)
                    {
                        result.Message = error.Description;
                    }
                    else
                    {
                        result.Message += Environment.NewLine + error.Description;
                    }
                }
            }

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<UserDto>> GetUserByIdAsync(int id)
        {
            var response = await _usersManager.GetUserByIdAsync(id);
            return response;
        }

        [HttpGet("roles/{id}")]
        public async Task<ResponseModelList<UserRoleDto>> GetRolesByUserIdAsync(int id)
        {
            var response = await _usersManager.GetRolesByUserIdAsync(id);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<UserTableItemDto>> SearchUsersAsync(SearchUsersParams searchParams)
        {
            var response = await _usersManager.SearchUsersAsync(searchParams);
            return response;
        }

        [HttpPost("save")]
        public async Task<ResponseModelBase> SaveUserAsync(SaveUserRequestModel requestModel)
        
        {
            var response = await _usersManager.UpdateUserAsync(requestModel);
            return response;
        }

        [HttpPost("saveUserRole")]
        public async Task<ResponseModelBase> CreateUserRoleAsync(CreateUserRoleModel requestModel)
        {
            var response = await _usersManager.CreateUserRoleAsync(requestModel);
            return response;
        }

        [HttpPost("deleteUserRole")]
        public async Task<ResponseModelBase> DeleteUserRoleAsync(DeleteUserRoleModel requestModel)
        {
            var response = await _usersManager.DeleteUserRoleAsync(requestModel.UserRoleId);
            return response;
        }

        [HttpGet("getLdapSetting")]
        public async Task<ResponseModel<bool>> GetLdapSetting()
        {
            var response = new ResponseModel<bool>();
            response.Payload = _configuration.GetValue<bool>("LDAPSettings:UseLdap");
            response.Valid = true;
            return response;
        }

        [HttpPost("registerAccount")]
        public async Task<ResponseModelBase> RegisterAccountAsync(RegisterModel requestModel)
        {
            var response = await _notificationsManager.SendNewAccountRequest(requestModel);
            return response;
        }

        [AllowAnonymous]
        [HttpPost("resetPassword")]
        public async Task<ResponseModelBase> ResetPasswordAsync(RegisterModel requestModel)
        {
            var response = await _notificationsManager.SendResetPasswordRequest(requestModel);
            return response;
        }

        [HttpPost("changePassword")]
        public async Task<ResponseModelBase> ChangePasswordAsync(ChangePasswordModel requestModel)
        {
            var response = new ResponseModelBase();
            var user = await _userManager.FindByIdAsync(requestModel.UserId!.ToString());
            try
            {
                if (user == null)
                {
                    response.Message = "Error, no user!";
                }
                else
                {

                    var isPasswordValid = await _userManager.CheckPasswordAsync(user, requestModel.OldPassword);
                    if (isPasswordValid)
                    {
                        var result =
                            await _usersManager.ValidatePasswordAsync(_userManager, user, requestModel.NewPassword);
                        if (result.Succeeded)
                        {
                            var passHash = _userManager.PasswordHasher.HashPassword(user, requestModel.NewPassword);
                            //var result = await _userManager.ChangePasswordAsync(user, requestModel.OldPassword, requestModel.NewPassword);
                            var updateResult = await _usersManager.UpdateUsersPasswordAsync(user.Id, passHash);
                            if (updateResult.Valid)
                            {
                                //await _signInManager.RefreshSignInAsync(user);
                                response.Valid = true;
                            }
                            else
                            {
                                response.Message = updateResult.Message;
                            }
                        }
                        else
                        {
                            response.Message = "Invalid new password! " +
                                               String.Join(" ", result.Errors.Select(x => x.Description));
                        }
                    }
                    else
                    {
                        response.Message = "Invalid old password!";
                    }
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        [HttpGet("adminResetPassword/{userId}/{adminUserId}")]
        public async Task<ResponseModelBase> AdminResetPasswordAsync(int userId, int adminUserId)
        {
            var response = new ResponseModelBase();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var adminUser = await _userManager.FindByIdAsync(adminUserId.ToString());
            try
            {
                if (user == null)
                {
                    response.Message = "Error, no user!";
                }
                else
                {
                    var newPassword = _usersManager.GenerateRandomPassword(null);
                    var passHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
                    var updateResult = await _usersManager.UpdateUsersPasswordAsync(user.Id, passHash);
                    if (updateResult.Valid)
                    {
                        var sendEmailResponse = await _notificationsManager.SendAdminResetPassword(newPassword,
                            user.UserName!, user.Email! + ", " + adminUser!.Email,
                            "Resetovanje korisničke šifre za aplikaciju za IDA aplikaciju");
                        response.Valid = true;
                    }
                    else
                    {
                        response.Message = updateResult.Message;
                    }
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        #region UserLog

        [HttpPost("saveUserLog")]
        public async Task<ResponseModel<int>> SaveUserLogAsync(SaveUserLogRequestModel requestModel)
        {
            var response = await _usersManager.SaveUserLogAsync(requestModel);
            return response;
        }

        [HttpPost("searchUserLogs")]
        public async Task<ResponseModelList<UserLogDto>> SearchUserLogsAsync(SearchUserLogsParams searchParams)
        {
            var response = await _usersManager.SearchUserLogsAsync(searchParams);
            return response;
        }

        #endregion
    }
}
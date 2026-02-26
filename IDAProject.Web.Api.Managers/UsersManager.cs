using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Auth;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.Dto.Users;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Users;

namespace IDAProject.Web.Api.Managers
{
    public class UsersManager : IUsersManager
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger _logger;

        public UsersManager(ILogger<SecurityManager> logger, IUsersRepository usersRepository)
        {
            _logger = logger;
            _usersRepository = usersRepository;
        }

        public async Task<ResponseModelList<UserTableItemDto>> SearchUsersAsync(SearchUsersParams searchParams)
        {
            var result = new ResponseModelList<UserTableItemDto>();
            try
            {
                result.Payload = await _usersRepository.SearchUsersAsync(searchParams);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(searchParams);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModel<UserDto>> GetUserByIdAsync(int id)
        {
            var result = new ResponseModel<UserDto>();
            try
            {
                result.Payload = await _usersRepository.GetUserByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The user with the specified id could not be found.";
                }
                else
                {
                    result.Valid = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }


        public async Task<ResponseModelList<UserRoleDto>> GetRolesByUserIdAsync(int id)
        {
            var result = new ResponseModelList<UserRoleDto>();
            try
            {
                result.Payload = await _usersRepository.GetRolesByUserIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The roles for user with the specified id could not be found.";
                }
                else
                {
                    result.Valid = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModelList<UserDto>> GetUsersByRoleIdAsync(int roleId)
        {
            var result = new ResponseModelList<UserDto>();
            try
            {
                result.Payload = await _usersRepository.GetUsersByRoleIdAsync(roleId);
                if (result.Payload == null)
                {
                    result.Message = "The users with the specified role id could not be found.";
                }
                else
                {
                    result.Valid = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {roleId}");
            }

            return result;
        }

        public async Task<ResponseModelBase> UpdateUserAsync(SaveUserRequestModel requestModel)
        {
            var result = new ResponseModelBase();
            try
            {
                await _usersRepository.UpdateUserAsync(requestModel);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }


        public async Task<ResponseModelBase> CreateUserRoleAsync(CreateUserRoleModel requestModel)
        {
            var result = new ResponseModelBase();
            try
            {
                var userRoleExists = await _usersRepository.SearchUserRoleAsync(requestModel);
                if (userRoleExists)
                {
                    throw new Exception("This role has been already assigned to user.");
                }
                else
                {
                    await _usersRepository.CreateUserRoleAsync(requestModel);
                    result.Valid = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }


        public async Task<ResponseModelBase> DeleteUserRoleAsync(int userRoleId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _usersRepository.DeleteUserRoleAsync(userRoleId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"user role id: {userRoleId}");
            }
            return result;
        }

        public async Task<ResponseModelBase> RegisterAccountAsync(RegisterModel requestModel)
        {
            var result = new ResponseModelBase();
            try
            {
                //await _usersRepository.DeleteUserRoleAsync(userRoleId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"user account name: {requestModel}");
            }

            return result;
        }

        public async Task<ResponseModelBase> ResetPasswordAsync(RegisterModel requestModel)
        {
            var result = new ResponseModelBase();
            try
            {
                //await _usersRepository.DeleteUserRoleAsync(userRoleId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"user account name: {requestModel}");
            }

            return result;
        }

        public async Task<ResponseModelBase> UpdateUsersPasswordAsync(int userId, string newPasswordHash)
        {
            var result = new ResponseModelBase();
            try
            {
                await _usersRepository.UpdateUsersPasswordAsync(userId, newPasswordHash);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(userId);
                _logger.LogError(e, $"request model: {reqModel}");
            }

            return result;
        }

        public async Task<IdentityResult> ValidatePasswordAsync(UserManager<AppIdentityUser> userManager,
            AppIdentityUser user, string password)
        {
            var passwordValidator = new PasswordValidator<AppIdentityUser>();
            var result = await passwordValidator.ValidateAsync(userManager, user, password);
            return result;
        }

        public string GenerateRandomPassword(PasswordOptions? opts = null)
        {
            if (opts == null)
                opts = new PasswordOptions()
                {
                    RequiredLength = 8,
                    RequiredUniqueChars = 4,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = true,
                    RequireUppercase = true
                };

            string[] randomChars = new[]
            {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ", // uppercase 
                "abcdefghijkmnopqrstuvwxyz", // lowercase
                "0123456789", // digits
                "!@$?_-" // non-alphanumeric
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count;
                 i < opts.RequiredLength
                 || chars.Distinct().Count() < opts.RequiredUniqueChars;
                 i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
        public async Task<ResponseModel<int>> SaveUserLogAsync(SaveUserLogRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _usersRepository.SaveUserLogAsync(requestModel);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");

            }
            return result;
        }

        public async Task<ResponseModelList<UserLogDto>> SearchUserLogsAsync(SearchUserLogsParams searchParams)
        {
            var result = new ResponseModelList<UserLogDto>();
            try
            {
                result.Payload = await _usersRepository.SearchUserLogsAsync(searchParams);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(searchParams);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

    }
}
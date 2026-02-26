using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using IDAProject.Web.Admin.Models.Accounts;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Admin.Managers
{
    public class AccountManager : BaseManager, IAccountManager
    {
        public AccountManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<AccountManager> logger,IHttpContextAccessor httpContextAccessor) 
            : base(httpClientFactory, configuration, logger )
        {
        }


        public bool IsTokenValid(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var secretKey = _configuration["JWT:Secret"];
            var tokenValidationParameters = JwtHelpers.GetTokenValidationParameters(secretKey!);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ResponseModel<string>> GenerateTokenAsync(LoginModel model)
        {
            var result = await PostAsync<LoginModel, ResponseModel<string>>("api/accounts/generateToken", model);
            return result;
        }

        public async Task<ResponseModel<bool>> GetLdapSetting()
        {
            var result = await GetAsync<ResponseModel<bool>>("api/accounts/getLdapSetting");
            return result;
        }
        public UserAccount GetUserFromJwt(string token)
        {
            UserAccount? result = null;
            if (!string.IsNullOrEmpty(token))
            {
                var claims = GetTokenClaims(token);

                if (claims.Any())
                {
                    result = new UserAccount();
                    foreach (var claim in claims)
                    {
                        if (claim.Type == ClaimTypes.NameIdentifier)
                        {
                            result.Id = int.Parse(claim.Value);
                        }
                        else if (claim.Type == ClaimTypes.Email)
                        {
                            result.Email = claim.Value;
                        }
                        else if (claim.Type == ClaimTypes.GivenName)
                        {
                            result.FirstName = claim.Value;
                        }
                        else if (claim.Type == ClaimTypes.Surname)
                        {
                            result.LastName = claim.Value;
                        }
                        else if (claim.Type == ClaimTypes.Name)
                        {
                            result.UserName = claim.Value;
                        }
                        else if (claim.Type == Constants.ClaimEmployeeId)
                        {
                            result.EmployeeId = int.Parse(claim.Value);
                        }
                        else if (claim.Type == Constants.ClaimPartnerId)
                        {
                            result.PartnerId = int.Parse(claim.Value);
                        }
                        else if (claim.Type == Constants.ClaimOrgId)
                        {
                            result.OrgId = int.Parse(claim.Value);
                        }
                        else if (claim.Type == Constants.ClaimPrinterId)
                        {
                            result.PrinterId = int.Parse(claim.Value);
                        }
                        else if (claim.Type == Constants.ClaimUserCulture)
                        {
                            result.UserCulture = String.IsNullOrEmpty(claim.Value) ? "Sr-Latn" : claim.Value;
                        }
                        else if (claim.Type == Constants.ClaimEmployeeCompanyName)
                        {
                            result.CompanyName = claim.Value;
                        }
                        else if (claim.Type == ClaimTypes.Role)
                        {
                            result.Roles.Add(claim.Value);
                        }
                        else if (claim.Type == Constants.ClaimFeature)
                        {
                            result.Features.Add(claim.Value);
                        }
                        else if (claim.Type == ClaimTypes.GroupSid)
                        {
                            result.CompanyId = int.Parse(claim.Value);
                        }
                    }
                }
            }
            return result!;
        }



        /// <summary>
        /// Receives the claims of token by given token as string.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>IEnumerable of claims for the given token.</returns>
        private IEnumerable<Claim> GetTokenClaims(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Given token is null or empty.");
            }

            var tokenValidationParameters = GetTokenValidationParameters();

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            var issuer = _configuration["JWT:ValidIssuer"];

            if (validatedToken.Issuer != issuer)
            {
                throw new SecurityTokenInvalidIssuerException(validatedToken.Issuer);
            }

            var refTime = DateTime.Now;
            if (validatedToken.ValidFrom > refTime || validatedToken.ValidTo < refTime)
            {
                throw new SecurityTokenExpiredException();
            }
            return tokenValid.Claims;
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }

        private SecurityKey GetSymmetricSecurityKey()
        {
            var secretKey = _configuration["JWT:Secret"];
            var symmetricKey = Encoding.UTF8.GetBytes(secretKey!);
            return new SymmetricSecurityKey(symmetricKey);
        }

        public async Task<ResponseModelBase> RegisterAccountAsync(RegisterModel model)
        {
            var result = await PostAsync<RegisterModel, ResponseModelBase>("api/accounts/registerAccount", model);
            return result;
        }

        public async Task<ResponseModelBase> ResetPasswordAsync(RegisterModel model)
        {
            var result = await PostAsync<RegisterModel, ResponseModelBase>("api/accounts/resetPassword", model);
            return result;
        }

        public async Task<ResponseModelBase> ChangePasswordAsync(ChangePasswordModel model)
        {
            var result = await PostAsync<ChangePasswordModel, ResponseModelBase>("api/accounts/changePassword", model);
            return result;
        }

        public async Task<ResponseModelBase> AdminResetPasswordAsync(int userId, int adminUserId)
        {
            var result = await GetAsync<ResponseModelBase>($"api/accounts/adminResetPassword/{userId}/{adminUserId}");
            return result;
        }
    }
}
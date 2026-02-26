using System.DirectoryServices.Protocols;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using IDAProject.Web.Api.Models.Auth;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Api.Managers
{
    public class SecurityManager : ISecurityManager
    {
        private readonly ISecurityRepository _securityRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public SecurityManager(ILogger<SecurityManager> logger, IConfiguration configuration, ISecurityRepository securityRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _securityRepository = securityRepository;
        }

        public async Task<ResponseModelBase> ValidateApiKeyAsync(string apiKey)
        {
            var result = new ResponseModelBase();
            try
            {
                result.Valid = await _securityRepository.ValidateApiKeyAsync(apiKey);
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"ApiKey: {apiKey}");
            }
            return result;
        }

        /// <summary>
        /// Generates encrypted token by given model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Generated token.</returns>
        public async Task<string> GenerateTokenAsync(AppIdentityUser userAccount, IList<string> roles, IList<string> features)
        {

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString("F0"), ClaimValueTypes.Integer32),
                new Claim(ClaimTypes.Email, userAccount.Email, ClaimValueTypes.String),
                new Claim(ClaimTypes.GivenName, userAccount.FirstName, ClaimValueTypes.String),
                new Claim(ClaimTypes.Surname, userAccount.LastName, ClaimValueTypes.String),
                new Claim(ClaimTypes.Name, userAccount.UserName, ClaimValueTypes.String),
                new Claim(Constants.ClaimUserCulture, userAccount.UserCulture == null ? "" : userAccount.UserCulture, ClaimValueTypes.String),
                new Claim(Constants.FcmToken, userAccount.FcmToken, ClaimValueTypes.String)
            };
            if (userAccount.EmployeeId.HasValue)
            {
                var companyName = await _securityRepository.GetEmployeeCompanyNameAsync(userAccount.EmployeeId.Value);
                var companyId = await _securityRepository.GetEmployeeCompanyIdAsync(userAccount.EmployeeId.Value);
                claims.Add(new Claim(Constants.ClaimEmployeeId, userAccount.EmployeeId.Value.ToString("F0"), ClaimValueTypes.Integer32));
                claims.Add(new Claim(Constants.ClaimEmployeeCompanyName, companyName, ClaimValueTypes.String));
                var groupSidClaim = new Claim(ClaimTypes.GroupSid, string.Empty);

                if (companyId.HasValue)
                {
                    groupSidClaim = new Claim(ClaimTypes.GroupSid, companyId.Value.ToString("F0"));
                }
                claims.Add(groupSidClaim);
            }
            if (userAccount.PartnerId.HasValue)
            {
                claims.Add(new Claim(Constants.ClaimPartnerId, userAccount.PartnerId.Value.ToString("F0"), ClaimValueTypes.Integer32));
                claims.Add(new Claim(Constants.ClaimPartnerName, userAccount.PartnerName, ClaimValueTypes.String));
            }
            if (userAccount.OrgId.HasValue)
            {
                claims.Add(new Claim(Constants.ClaimOrgId, userAccount.OrgId.Value.ToString("F0"), ClaimValueTypes.Integer32));
                //claims.Add(new Claim(Constants.ClaimOrgCode, userAccount.OrgCode, ClaimValueTypes.String));
                //claims.Add(new Claim(Constants.ClaimOrgName, userAccount.OrgName, ClaimValueTypes.String));
            }

            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role, ClaimValueTypes.String);
                claims.Add(roleClaim);
            }

            foreach (var feature in features)
            {
                var featureClaim = new Claim(Constants.ClaimFeature, feature, ClaimValueTypes.String);
                claims.Add(featureClaim);
            }

            var secretKey = _configuration["JWT:Secret"];
            var symmetricSecurityKey = JwtHelpers.GetSymmetricSecurityKey(secretKey);
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Subject = new ClaimsIdentity(claims),
                //Expires = DateTime.UtcNow.AddMinutes(Constants.ShortLivedTokenExpirationInMinutes),
                Expires = DateTime.UtcNow.AddDays(Constants.LongLivedTokenExpirationInDays),
                SigningCredentials = signingCredentials
            };

            try
            {
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

                var token = jwtSecurityTokenHandler.WriteToken(securityToken);

                return token;
            }
            catch (Exception ex)
            {

                throw new SecurityTokenEncryptionFailedException(ex.Message);
            }
        }
        public bool CheckLdapPasswordAsync(AppIdentityUser? user, string? password)
        {
            try
            {
                var ldapServer = _configuration["LDAPSettings:Domain"];
                using (var ldapConnection = new LdapConnection(ldapServer))
                {
                    var networkCredential = new NetworkCredential(user.UserName, password, ldapServer);
                    ldapConnection.Credential = networkCredential;
                    ldapConnection.Bind();
                    return true;
                }
            }
            catch (LdapException)
            {
                return false;
            }
        }

    }


}

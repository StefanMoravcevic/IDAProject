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
    public class LdapManager : ILdapManager
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public LdapManager(ILogger<SecurityManager> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public bool CheckLdapPasswordAsync(AppIdentityUser? user, string? password)
        {
	        try
	        {
                var ldapServer = _configuration["LDAPSettings:Domain"];
		        using (var ldapConnection = new LdapConnection(ldapServer))
		        {
                    NetworkCredential networkCredential;

                    if (user.UserName.Contains("@"))
                    {
                        networkCredential = new NetworkCredential(user.UserName, password);
                    }
                    else
                    {
                        networkCredential = new NetworkCredential(user.UserName, password,ldapServer);
                    }

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

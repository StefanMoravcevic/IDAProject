using IDAProject.Web.Admin.Models.Accounts;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.TagHelpers;
using IDAProject.Web.Admin.Models.ViewModels;
using IDAProject.Web.Models.Auth.RequestModels;
using IDAProject.Web.Models.General;
using Org.BouncyCastle.Asn1.Ocsp;
using IDAProject.Web.Admin.Managers;

namespace IDAProject.Web.Api.Middlewares
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAccountManager _accountManager)
        {
            // Retrieve user preferences, you may get this from user claims or any other source
            var token = context.Request.Cookies[Constants.AdminCookieToken];
            if (token != null)
            {
                var result = _accountManager.GetUserFromJwt(token!);

                string userCulture = result.UserCulture;

                // Set the culture for the current request
                if (!string.IsNullOrEmpty(userCulture))
                {
                    CultureInfo cultureInfo = new CultureInfo(userCulture);
                    ////CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                    ////CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
                    CultureInfo.CurrentUICulture = cultureInfo;
                }
                else
                {
                    CultureInfo cultureInfo = new CultureInfo("en-US");
                    CultureInfo.CurrentUICulture = cultureInfo;
                }
            }
            // Call the next middleware in the pipeline
            await _next(context);
        }

    }
}
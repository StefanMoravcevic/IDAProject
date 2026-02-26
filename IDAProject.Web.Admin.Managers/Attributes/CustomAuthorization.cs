using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using IDAProject.Web.Admin.Models.Accounts;
using IDAProject.Web.Models.General.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using IDAProject.Web.Models.General;
using IDAProject.Web.Admin.Managers;
using Microsoft.IdentityModel.Tokens;
using IDAProject.Web.Helpers;
using System.IdentityModel.Tokens.Jwt;
using IDAProject.Web.Admin.Models.Interfaces.Managers;

namespace IDAProject.Web.Admin.Managers.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorizationAttribute : TypeFilterAttribute
    {
        public CustomAuthorizationAttribute(int role, int feature) : base(typeof(CustomAuthorizationFilter))
        {
            Arguments = new object[] { role, feature };
        }
    }

    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        private readonly int _role;
        private readonly int _feature;
        private readonly IAccountManager _accountManager;
        private readonly AuthorizationService _authorizationService;

        public CustomAuthorizationFilter(int role, int feature, IAccountManager accountManager)
        {
            _role = role;
            _feature = feature;
            _accountManager = accountManager;
            _authorizationService = new AuthorizationService();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Cookies[Constants.AdminCookieToken];
            var user = _accountManager.GetUserFromJwt(token!);

            if (!_authorizationService.CheckUser(user, _role, _feature))
            {
                //context.Result = new ForbidResult();
                var routeValues = new { userMessage = "You don't have permission to access the page!" };
                context.Result = new RedirectToActionResult("Index", "Error", routeValues);
            }
        }
    }

}
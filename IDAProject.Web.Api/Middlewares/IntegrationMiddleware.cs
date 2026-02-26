using Microsoft.AspNetCore.Authentication;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Api.Middlewares
{
    public class IntegrationMiddleware
    {
        private readonly RequestDelegate _next;

        public IntegrationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ISecurityManager securityManager)
        {
            var apiKey = httpContext.Request.Headers[Constants.ApiKeyName];
            var validationResponse = await securityManager.ValidateApiKeyAsync(apiKey!);

            if (validationResponse.Valid)
            {
                await _next(httpContext);
            }
            else
            {
                await httpContext.ForbidAsync();
            }
        }
    }
}
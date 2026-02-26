namespace IDAProject.Web.Api.Middlewares
{
    public class JwtCookieAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _cookieName;

        public JwtCookieAuthenticationMiddleware(RequestDelegate next, string cookieName)
        {
            _next = next;
            _cookieName = cookieName;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if Authorization header is already present
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                // Check if JWT token exists in the cookie
                if (context.Request.Cookies.TryGetValue(_cookieName, out string? token) && !string.IsNullOrEmpty(token))
                {
                    // Add the JWT token as Bearer token in the Authorization header
                    context.Request.Headers.Append("Authorization", $"Bearer {token}");
                }
            }

            // Continue the pipeline
            await _next(context);
        }
    }

    // Extension method for easy registration
    public static class JwtCookieAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtCookieAuthentication(
            this IApplicationBuilder builder, string cookieName = "admin_token")
        {
            return builder.UseMiddleware<JwtCookieAuthenticationMiddleware>(cookieName);
        }
    }
}

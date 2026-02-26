using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Serilog;
using Serilog.Settings.Configuration;
using IDAProject.Web.Admin.Binders;
using IDAProject.Web.Admin.Ioc;
using IDAProject.Web.Api.Middlewares;
using IDAProject.Web.Models.Common;
using System.Globalization;
using System.Resources;
using IDAProject.Web.Admin.Managers.Helpers;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Serilog configuration
var loggerOptions = new ConfigurationReaderOptions();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration, loggerOptions)
    .CreateLogger();

try
{
    Log.Information("Starting web application");
    builder.Host.UseSerilog();
    builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

// Localization
builder.Services.AddLocalization(options => options.ResourcesPath = Path.Combine("Resources", "ResourceFiles"));

// Add Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Accounts/Login";
        options.LogoutPath = "/Accounts/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.Cookie.IsEssential = true;
    });

// Add services to the container
builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new TmsModelBinderProvider());
})
.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
.AddDataAnnotationsLocalization();

// IHttpClientFactory
builder.Services.AddHttpClient("ApiClient")
    .AddHttpMessageHandler<AuthHeaderHandler>();
builder.Services.AddHttpContextAccessor();

// Cookie Policy
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

// Optional: ConfigureApplicationCookie is redundant here because AddCookie already sets IsEssential
// builder.Services.ConfigureApplicationCookie(options => options.Cookie.IsEssential = true);

AppMappings.CreateMappings(builder.Services);

var hereSection = configuration.GetSection("Here");
builder.Services.Configure<HereSettings>(hereSection);

var app = builder.Build();

// Supported cultures
var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("sr-Latn"),
    new CultureInfo("sr-Cyrl")
};

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRequestLocalization(localizationOptions);
app.UseMiddleware<CultureMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();

app.UseAuthentication();  // <--- obavezno
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accounts}/{action=Login}");

app.Run();
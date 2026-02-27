using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Settings.Configuration;
using System.Text;
using IDAProject.Web.Api.Infrastructure;
using IDAProject.Web.Api.Ioc;
using IDAProject.Web.Api.Middlewares;
using IDAProject.Web.Api.Models.Auth;
using IDAProject.Web.Api.Models.Common;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Models.General;
using IDAProject.Web.Api;
var builder = WebApplication.CreateBuilder(args);

var loggerOptions = new ConfigurationReaderOptions();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration, loggerOptions)
    .CreateLogger();

try
{
    Log.Information("Starting web application");
    builder.Host.UseSerilog();

    builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration));
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

// Add services to the container.

// Entity Framework
ConfigurationManager configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultDatabase");

builder.Services.AddDbContext<IdaContext>(options => options.UseSqlServer(connectionString!));

// CORS configuration disable cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("DisableCors",
        x =>
        {
            x.SetIsOriginAllowed(origin => true)
                .AllowAnyHeader()
                .AllowCredentials()
                .AllowAnyMethod();
        });
});


// For Identity
var identityBuilder = builder.Services.AddIdentity<AppIdentityUser, AppIdentityRole>();
AppMappings.CreateIdentityFrameworkMappings(identityBuilder);
identityBuilder.AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/Hubs")))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };

});

AppMappings.CreateMappings(builder.Services);

builder.Services.AddControllers(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

// IHttpClientFactory
builder.Services.AddHttpClient();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    // API key (postojeći)
    x.AddSecurityDefinition(Constants.ApiKeyName, new OpenApiSecurityScheme
    {
        Description = "WebApi key name. X-Api-Key: key-value",
        In = ParameterLocation.Header,
        Name = Constants.ApiKeyName,
        Type = SecuritySchemeType.ApiKey
    });

    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});


var fileRepoSection = configuration.GetSection("FileRepository");
builder.Services.Configure<FileRepositorySettings>(fileRepoSection);

var emailQueueSection = configuration.GetSection("EmailQueue");
builder.Services.Configure<EmailQueueSettings>(emailQueueSection);

var firebaseSection = configuration.GetSection("Firebase");
builder.Services.Configure<FirebaseSettings>(firebaseSection);

var binPath = AppDomain.CurrentDomain.BaseDirectory;
//var keyPath = Path.Combine(binPath, "Firebase", "key.json");


builder.Services.AddHangfire(configuration => configuration
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(connectionString));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer(options => options.SchedulePollingInterval = TimeSpan.FromSeconds(1));

// Add signalR
builder.Services.AddSignalR();

//builder.Services.AddHttpClient("UhuraClient", client =>
//{
//    client.BaseAddress = new Uri("https://bis1.prod.apimanagement.eu20.hana.ondemand.com/");
//    client.Timeout = TimeSpan.FromSeconds(30);
//    client.DefaultRequestHeaders.Accept.Add(
//        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
//});





var app = builder.Build();

app.UseCors("DisableCors");
//app.UseCors("SignalrCors");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

if (configuration.GetValue<bool>("EnableIntegrationAuth"))
{
    app.UseMiddleware<IntegrationMiddleware>();
}

// Authentication & Authorization
//app.UseJwtCookieAuthentication(Constants.AdminCookieToken);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
var hangfireOptions = new DashboardOptions()
{
    DashboardTitle = "Hangfire Dashboard",
    Authorization = new[] {
        new HangfireBasicAuthenticationFilter {
            User = configuration.GetSection("HangfireCredentials:UserName").Value,
            Pass = configuration.GetSection("HangfireCredentials:Password").Value
        }
    }
};

app.UseHangfireDashboard("/hangfire", hangfireOptions);
app.MapHangfireDashboard();

HangfireRecurringJobs.CreateJobs(configuration);



app.Run();

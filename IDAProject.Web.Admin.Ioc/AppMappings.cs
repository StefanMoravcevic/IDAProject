using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using IDAProject.Web.Admin.Managers;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Users;
using IDAProject.Web.Admin.Managers.Attributes;
using IDAProject.Web.Admin.Managers.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace IDAProject.Web.Admin.Ioc
{
    public static class AppMappings
    {
        public static void CreateMappings(IServiceCollection serviceCollection)
        {
            MapManagers(serviceCollection);
        }

        private static void MapManagers(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAddressesManager, AddressesManager>();
            serviceCollection.AddScoped<IAccountManager, AccountManager>();
            serviceCollection.AddScoped<IMasterDataManager, MasterDataManager>();
            serviceCollection.AddScoped<IUsersManager, UsersManager>();
            serviceCollection.AddScoped<IPartnersManager, PartnersManager>();
            serviceCollection.AddScoped<IRolesManager, RolesManager>();
            serviceCollection.AddScoped<IDocumentsManager, DocumentsManager>();
            serviceCollection.AddScoped<ICompaniesManager, CompaniesManager>();
            serviceCollection.AddScoped<IDocumentSeriesManager, DocumentSeriesManager>();
            serviceCollection.AddScoped<IReportsManager, ReportsManager>();
            serviceCollection.AddScoped<IEmployeesManager, EmployeesManager>();
            serviceCollection.AddScoped<IUserNotificationsManager, UserNotificationsManager>();
            serviceCollection.AddScoped<IProjectsManager, ProjectsManager>();
            serviceCollection.AddScoped<IIdaTasksManager, IdaTasksManager>();
            serviceCollection.AddScoped<IRegularActivitiesManager, RegularActivitiesManager>();
            serviceCollection.AddScoped<ITasksPlanningsManager, TasksPlanningsManager>();
            serviceCollection.AddScoped<IEmployeeAbsencesManager, EmployeeAbsencesManager>();

            serviceCollection.AddScoped<AuthorizationService>();
            serviceCollection.AddScoped<AuthorizationHelpers>();
            serviceCollection.AddTransient<AuthHeaderHandler>();
        }
    }
}

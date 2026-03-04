using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using IDAProject.Web.Api.Managers;
using IDAProject.Web.Api.Models.Interfaces.Internal;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Api.Repositories;
using IDAProject.Web.Api.Repositories.IdentityStores;
using IDAProject.Web.Api.Repositories.MasterDataProviders;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Managers;


namespace IDAProject.Web.Api.Ioc
{
    public class AppMappings
    {
        public static void CreateMappings(IServiceCollection serviceCollection)
        {
            MapRepositories(serviceCollection);
            MapManagers(serviceCollection);
            MapReports(serviceCollection);
            MapServices(serviceCollection);
        }

        private static void MapServices(IServiceCollection serviceCollection)
        {
        }


        public static void CreateIdentityFrameworkMappings(IdentityBuilder identityBuilder)
        {
            identityBuilder.AddEntityFrameworkStores<IdaContext>();
            identityBuilder.AddUserStore<AppUserStore>();            
        }

        private static void MapRepositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAddressesRepository, AddressesRepository>();
            serviceCollection.AddScoped<ISecurityRepository, SecurityRepository>();
            serviceCollection.AddScoped<IEmployeesRepository, EmployeesRepository>();
            serviceCollection.AddScoped<IMasterDataRepository, MasterDataRepository>();
            serviceCollection.AddScoped<IUsersRepository, UsersRepository>();
            serviceCollection.AddScoped<ICompaniesRepository, CompaniesRepository>();
            serviceCollection.AddScoped<IPartnersRepository, PartnersRepository>();
            serviceCollection.AddScoped<IRolesRepository, RolesRepository>();
            serviceCollection.AddScoped<IQueueRepository, QueueRepository>();
            //serviceCollection.AddScoped<IMessagesRepository, MessagesRepository>();
            serviceCollection.AddScoped<INotificationsRepository, NotificationsRepository>();
            serviceCollection.AddScoped<IDocumentsRepository, DocumentsRepository>();
            serviceCollection.AddScoped<IDocumentSeriesRepository, DocumentSeriesRepository>();
            serviceCollection.AddScoped<IUserNotificationsRepository, UserNotificationsRepository>();
            serviceCollection.AddScoped<IProjectsRepository, ProjectsRepository>();
            serviceCollection.AddScoped<IIdaTasksRepository, IdaTasksRepository>();
            serviceCollection.AddScoped<IRegularActivitiesRepository, RegularActivitiesRepository>();
            serviceCollection.AddScoped<ITasksPlanningsRepository, TasksPlanningsRepository>();
            serviceCollection.AddScoped<IEmployeeAbsencesRepository, EmployeeAbsencesRepository>();

   
            serviceCollection.AddSingleton<IMasterDataProvidersFactory, MasterDataProvidersFactory>();            
        }

        private static void MapManagers(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAddressesManager, AddressesManager >();
            serviceCollection.AddScoped<ISecurityManager, SecurityManager>();
            serviceCollection.AddScoped<IEmployeesManager, EmployeesManager>();
            serviceCollection.AddScoped<IMasterDataManager, MasterDataManager>();
            serviceCollection.AddScoped<ICompaniesManager, CompaniesManager>();
            serviceCollection.AddScoped<IPartnersManager, PartnersManager>();
            serviceCollection.AddScoped<IRolesManager, RolesManager>();
            serviceCollection.AddScoped<INotificationsManager, NotificationsManager>();
            serviceCollection.AddScoped<IUsersManager, UsersManager>();
            //serviceCollection.AddScoped<IMessagesManager, MessagesManager>();
            serviceCollection.AddScoped<IDocumentsManager, DocumentsManager>();
            serviceCollection.AddScoped<IDocumentSeriesManager, DocumentSeriesManager>();
            serviceCollection.AddScoped<ILdapManager, LdapManager>();
            serviceCollection.AddScoped<IUserNotificationsManager, UserNotificationsManager>();
            serviceCollection.AddScoped<IProjectsManager, ProjectsManager>();
            serviceCollection.AddScoped<IIdaTasksManager, IdaTasksManager>();
            serviceCollection.AddScoped<IRegularActivitiesManager, RegularActivitiesManager>();
            serviceCollection.AddScoped<ITasksPlanningsManager, TasksPlanningsManager>();
            serviceCollection.AddScoped<IEmployeeAbsencesManager, EmployeeAbsencesManager>();
        }

        private static void MapReports(IServiceCollection serviceCollection)
        {
            
        }        
    }
}
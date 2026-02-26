using System.Reflection;
using IDAProject.Web.Api.Models.Interfaces.Internal;
using IDAProject.Web.Models.Dto.MasterData;

namespace IDAProject.Web.Api.Repositories.MasterDataProviders
{
    public class MasterDataProvidersFactory : IMasterDataProvidersFactory
    {
        private List<IMasterDataProvider> _providers;

        public MasterDataProvidersFactory()
        {
            _providers = new List<IMasterDataProvider>();
        }

        public IMasterDataProvider GetGenericSelectOptionsProvider()
        {
            EnsureProvidersInitialized();

            foreach (var dataProvider in _providers)
            {
                if (dataProvider.IsGenericSelectOptionsProvider())
                {
                    return dataProvider;
                }
            }
            return null!;
        }

        public IMasterDataProvider GetProvider(string tableName)
        {
            EnsureProvidersInitialized();

            foreach (var dataProvider in _providers)
            {
                if (dataProvider.IsProvider(tableName))
                {
                    return dataProvider;
                }
            }
            return null!;
        }

        public IEnumerable<MasterDataTableInfo> GetSupportedMasterDataTables()
        {
            var result = new List<MasterDataTableInfo>();
            EnsureProvidersInitialized();

            foreach (var dataProvider in _providers)
            {
                var tables = dataProvider.GetSupportedMasterDataTables();
                if(tables.Any())
                {
                    result.AddRange(tables);
                }
            }
            return result;
        }

        private void EnsureProvidersInitialized()
        {
            if (_providers.Count == 0)
            {
                var type = typeof(IMasterDataProvider);
                var currentAssembly = Assembly.GetExecutingAssembly();
                var allTypes = currentAssembly.GetTypes();

                var masterDataProviderTypes = allTypes.Where(x => x.IsInterface == false && type.IsAssignableFrom(x));

                foreach (var mdType in masterDataProviderTypes)
                {
                    var instance = Activator.CreateInstance(mdType) as IMasterDataProvider;
                    _providers.Add(instance!);
                }
            }
        }
    }
}

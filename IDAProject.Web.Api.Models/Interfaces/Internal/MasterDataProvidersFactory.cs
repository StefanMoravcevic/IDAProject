using IDAProject.Web.Models.Dto.MasterData;

namespace IDAProject.Web.Api.Models.Interfaces.Internal
{
    public interface IMasterDataProvidersFactory
    {
        IMasterDataProvider GetProvider(string tableName);

        /// <summary>
        /// Returns the data provider which is capable to operate thru all tables and to generate <see cref="IDAProject.Web.Models.Interfaces.Html.ISelectOption"/> items.
        /// </summary>
        /// <returns></returns>
        IMasterDataProvider GetGenericSelectOptionsProvider();

        IEnumerable<MasterDataTableInfo> GetSupportedMasterDataTables();

    }
}

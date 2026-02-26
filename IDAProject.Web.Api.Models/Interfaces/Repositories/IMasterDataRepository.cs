using IDAProject.Web.Models.Dto.Common;
using IDAProject.Web.Models.Dto.MasterData;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.MasterData;

namespace IDAProject.Web.Api.Models.Interfaces.Repositories
{
    public interface IMasterDataRepository
    {
        IEnumerable<MasterDataTableInfo> GetSupportedMasterDataTables();

        Task<MasterEntity> GetTableDataAsync(string tableName, int? id);

        Task UpdateTableDataAsync(MasterEntityRequestModel requestModel);

        Task SoftDeleteByIdAsync(string tableName, int id, int? deletedByUserId);

        Task<int> CreateTableDataAsync(MasterEntityRequestModel requestModel);

        Task<UserTableSettings> GetTableSettingsAsync(int idUser, string tableName);

        Task UpdateTableSettingsColumnAsync(int idUser, string tableName, string columnName, bool isVisible);

        Task UpdateTableSettingsColumnsOrderAsync(int idUser, string tableName, List<string> columns);

        /// <summary>
        /// Gets a list of <see cref="ISelectOption"/> for the specified parameters.
        /// </summary>
        /// <param name="tableName">Main table DB name.</param>
        /// <param name="descriptionExpression">
        /// Display expresion where column name is wrapped with curly braces. Example: {columnName1} {columnName2}, or {columnName1}/{columnName2} etc.
        /// If without curly braces then it'll be considered as column name.</param>
        /// <returns>Returns a list of <see cref="ISelectOption"/> for the specified parameters.</returns>
        List<ISelectOption> GetSelectOptionsByTable(string tableName, string descriptionExpression);

        List<ISelectOption> GetFilteredSelectOptionsByTable(string tableName, string keyPropertyName, int propertyValue, string descriptionPropertyName);

        Task<GeneralSettingDto> GetGeneralSettingsAsync();
        Task<GeneralSettingDto> GetGeneralSettingsByLocationAsync(string locationCode);

        Task<int> SaveGeneralSettingAsync(SaveGeneralSettingRequestModel requestModel);
    }
}

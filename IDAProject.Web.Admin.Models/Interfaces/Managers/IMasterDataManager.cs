using IDAProject.Web.Models.Dto.Common;
using IDAProject.Web.Models.Dto.MasterData;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.MasterData;

namespace IDAProject.Web.Admin.Models.Interfaces.Managers
{
    public interface IMasterDataManager
    {
        Task<ResponseModel<MasterEntity>> GetTableDataAsync(string tableName);

        Task<ResponseModel<MasterEntity>> GetRecordByIdAsync(string tableName, int id);

        Task<ResponseModelBase> UpdateTableDataAsync(MasterEntityRequestModel requestModel);

        Task<ResponseModelBase> CreateTableDataAsync(MasterEntityRequestModel requestModel);

        Task<IEnumerable<MasterDataTableInfo>> GetSupportedMasterDataTables();

        /// <summary>
        /// Gets a list of <see cref="ISelectOption"/> for the specified parameters.
        /// </summary>
        /// <param name="tableName">Main table DB name.</param>
        /// <param name="descriptionExpression">
        /// Display expression where column name is wrapped with curly braces. Example: {columnName1} {columnName2}, or {columnName1}/{columnName2} etc.
        /// If without curly braces then it'll be considered as column name.</param>
        /// <returns>Returns a list of <see cref="ISelectOption"/> for the specified parameters.</returns>
        Task<IEnumerable<ISelectOption>> GetSelectOptionsByTableAsync(string tableName, string descriptionExpression);

        Task<ResponseModel<UserTableSettings>> GetTableSettingsAsync(int idUser, string tableName);

        Task<ResponseModelBase> UpdateTableSettingsColumnAsync(UpdateTableSettingsColumnVisibilityRequestModel requestModel);

        Task<ResponseModelBase> UpdateTableSettingsColumnsOrderAsync(UpdateTableSettingsColumnsOrderRequestModel requestModel);

        Task<IEnumerable<ISelectOption>> GetFilteredSelectOptionsByTable(string tableName, string keyColumnName, int columnValue, string descriptionColumnName);

        Task<IEnumerable<ISelectOption>> GetFilteredSelectOptionsByTable(string tableName, string keyColumnName, int columnValue);

        Task<GeneralSettingDto> GetGeneralSettingsAsync();
        Task<GeneralSettingDto> GetGeneralSettingsByLocationAsync(string locationCode);

        Task<ResponseModel<int>> SaveGeneralSettingAsync(SaveGeneralSettingRequestModel requestModel);

    }
}

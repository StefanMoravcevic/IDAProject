using IDAProject.Web.Models.Dto.Common;
using IDAProject.Web.Models.Dto.MasterData;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.MasterData;

namespace IDAProject.Web.Api.Models.Interfaces.Managers
{
    public interface IMasterDataManager
    {
        ResponseModel<IEnumerable<MasterDataTableInfo>> GetSupportedMasterDataTables();

        Task<ResponseModel<MasterEntity>> GetTableDataAsync(string tableName, int? id);

        Task<ResponseModelBase> UpdateTableDataAsync(MasterEntityRequestModel requestModel);

        Task<ResponseModel<int>> CreateTableDataAsync(MasterEntityRequestModel requestModel);

        Task<ResponseModelBase> SoftDeleteByIdAsync(string tableName, int id, int? deletedByUserId);

        Task<ResponseModel<UserTableSettings>> GetTableSettingsAsync(int idUser, string tableName);

        Task<ResponseModelBase> UpdateTableSettingsColumnAsync(UpdateTableSettingsColumnVisibilityRequestModel requestModel);

        Task<ResponseModelBase> UpdateTableSettingsColumnsOrderAsync(UpdateTableSettingsColumnsOrderRequestModel requestModel);

        /// <summary>
        /// Gets a list of <see cref="ISelectOption"/> for the specified parameters.
        /// </summary>
        /// <param name="tableName">Main table DB name.</param>
        /// <param name="descriptionExpression">
        /// Display expresion where column name is wrapped with curly braces. Example: {columnName1} {columnName2}, or {columnName1}/{columnName2} etc.
        /// If without curly braces then it'll be considered as column name.</param>
        /// <returns>Returns a list of <see cref="ISelectOption"/> for the specified parameters.</returns>
        ResponseModelList<ISelectOption> GetSelectOptionsByTable(string tableName, string descriptionExpression);

        ResponseModelList<ISelectOption> GetFilteredSelectOptionsByTable(string tableName, string keyPropertyName, int propertyValue, string descriptionPropertyName);

        Task<ResponseModel<GeneralSettingDto>> GetGeneralSettingsAsync();
        Task<ResponseModel<GeneralSettingDto>> GetGeneralSettingsByLocationAsync(string locationCode);

        Task<ResponseModel<int>> SaveGeneralSettingAsync(SaveGeneralSettingRequestModel requestModel);
    }
}
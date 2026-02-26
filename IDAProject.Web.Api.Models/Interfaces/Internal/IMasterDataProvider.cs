using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Models.Dto.MasterData;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.MasterData;

namespace IDAProject.Web.Api.Models.Interfaces.Internal
{
    public interface IMasterDataProvider
    {
        bool IsProvider(string tableName);

        Task<MasterEntity> GetTableDataAsync(IDAProjectContext dbContext, string tableName, int? id);

        Task UpdateTableDataAsync(IDAProjectContext dbContext, MasterEntityRequestModel requestModel);

        Task<int> CreateTableDataAsync(IDAProjectContext dbContext, MasterEntityRequestModel requestModel);

        List<MasterDataTableInfo> GetSupportedMasterDataTables();

        bool IsGenericSelectOptionsProvider();

        /// <summary>
        /// Gets a list of <see cref="ISelectOption"/> for the specified parameters.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        /// <param name="tableName">Main table DB name.</param>
        /// <param name="descriptionExpression">
        /// Display expresion where column name is wrapped with curly braces. Example: {columnName1} {columnName2}, or {columnName1}/{columnName2} etc.
        /// If without curly braces then it'll be considered as column name.</param>
        /// <returns>Returns a list of <see cref="ISelectOption"/> for the specified parameters.</returns>
        List<ISelectOption> GetSelectOptionsByTable(IDAProjectContext dbContext, string tableName, string descriptionExpression);

        List<ISelectOption> GetFilteredSelectOptionsByTable(IDAProjectContext dbContext, string tableName, string keyPropertyName, int propertyValue, string descriptionPropertyName);

        Task SoftDeleteByIdAsync(IDAProjectContext dbContext, string tableName, int id, int? deletedByUserId);
    }
}

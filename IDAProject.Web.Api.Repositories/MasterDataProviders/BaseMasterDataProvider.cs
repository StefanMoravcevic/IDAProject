using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.MasterData;
using IDAProject.Web.Models.RequestModels.MasterData;

namespace IDAProject.Web.Api.Repositories.MasterDataProviders
{
    public abstract class BaseMasterDataProvider
    {
        protected List<MasterDataTableInfo> _supportedTables;

        internal BaseMasterDataProvider()
        {
            _supportedTables = new List<MasterDataTableInfo>();
        }

        protected void CopyValuesToEntity<TEntity>(MasterEntityRequestModel requestModel, TEntity? entity)
        {
            var type = entity!.GetType();            

            foreach (var field in requestModel.Fields)
            {
                var property = type.GetProperty(field.Name);

                if (property != null)
                {
                    var propType = property.PropertyType;
                    var rawValue = MasterDataConverter.GetRawValue(field.Value,propType);
                    property.SetValue(entity, rawValue);
                }
            }
        }

        public List<MasterDataTableInfo> GetSupportedMasterDataTables()
        {
            return _supportedTables;
        }

        public virtual bool IsProvider(string tableName)
        {
            return _supportedTables.Any(x => x.TableName == tableName);
        }
    }
}
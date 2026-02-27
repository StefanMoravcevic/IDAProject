using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Internal;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.MasterData;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.MasterData;

namespace IDAProject.Web.Api.Repositories.MasterDataProviders
{
    internal class MeasureUnitsMasterDataProvider : BaseMasterDataProvider, IMasterDataProvider
    {
        public MeasureUnitsMasterDataProvider()
        {
            _supportedTables.Add(new MasterDataTableInfo("MeasureUnits", "Measure Units"));
        }

        public async Task<int> CreateTableDataAsync(IdaContext dbContext, MasterEntityRequestModel requestModel)
        {
            var measureUnit = new MeasureUnit();

            CopyValuesToEntity(requestModel, measureUnit);

            var entity = await dbContext.MeasureUnits.AddAsync(measureUnit);
            await dbContext.SaveChangesAsync();
            return entity.Entity.Id;
        }

        public List<ISelectOption> GetSelectOptionsByTable(IdaContext dbContext, string tableName, string descriptionColumnName)
        {
            throw new NotSupportedException();
        }

        public bool IsGenericSelectOptionsProvider()
        {
            return false;
        }

        public async Task<MasterEntity> GetTableDataAsync(IdaContext dbContext, string tableName, int? id)
        {
            var result = new MasterEntity(tableName);

            result.Fields.Add(new MasterEntityField
            {
                Name = "Sign",
                DisplayName = "Sign",
                Type = typeof(string).ToString()
            });

            result.Fields.Add(new MasterEntityField
            {
                Name = "Name",
                DisplayName = "Name",
                Type = typeof(string).ToString()
            });

            var unitTypeField = new MasterEntityField
            {
                Name = "MeasureUnitTypeId",
                DisplayName = "Unit Type",
                Type = typeof(int).ToString()
            };
                        
            unitTypeField.Options =  await (from mt in dbContext.MeasureUnitTypes
                                            select new GenericSelectOption(mt.Id, mt.Name)).ToListAsync();

            result.Fields.Add(unitTypeField);

            List<MeasureUnit> items;
            if (id.HasValue)
            {
                items = await dbContext.MeasureUnits.Where(x => x.Id == id).ToListAsync();
            }
            else
            {
                items = await dbContext.MeasureUnits.ToListAsync();
            }

            foreach (var item in items)
            {
                var record = new MasterEntityRecord
                {
                    Id = item.Id,
                    Values = new List<string>
                        {
                            MasterDataConverter.SerializeRawValue(item.Sign),
                            MasterDataConverter.SerializeRawValue(item.Name),
                            MasterDataConverter.SerializeRawValue(item.MeasureUnitTypeId)
                        }
                };
                result.Records.Add(record);
            }

            return result;
        }

        public override bool IsProvider(string tableName)
        {
            return false; // turned off!
        }

        public async Task UpdateTableDataAsync(IdaContext dbContext, MasterEntityRequestModel requestModel)
        {
            var idField = requestModel.Fields.Single(x => x.Name == "Id");
            var id = (int)MasterDataConverter.GetRawValue(idField.Value, typeof(int))!;

            var measureUnit = dbContext.MeasureUnits.Single(x => x.Id == id);
            CopyValuesToEntity(requestModel, measureUnit);

            await dbContext.SaveChangesAsync();
        }

        public List<ISelectOption> GetFilteredSelectOptionsByTable(IdaContext dbContext, string tableName, string keyPropertyName, int propertyValue, string descriptionPropertyName)
        {
            throw new NotSupportedException();
        }

        public async Task SoftDeleteByIdAsync(IdaContext dbContext, string tableName, int id, int? deletedByUserId)
        {
            var measureUnit = dbContext.MeasureUnits.Single(x => x.Id == id);
            measureUnit.IsDeleted = true;
            measureUnit.DeletedBy = deletedByUserId;
            measureUnit.DeletedDate = DateTime.Now;

            await dbContext.SaveChangesAsync();
        }
    }
}

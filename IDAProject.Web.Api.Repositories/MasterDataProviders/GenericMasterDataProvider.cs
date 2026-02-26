using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using IDAProject.Web.Api.Models.Interfaces.Internal;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.MasterData;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.MasterData;

namespace IDAProject.Web.Api.Repositories.MasterDataProviders
{
    internal class GenericMasterDataProvider : BaseMasterDataProvider, IMasterDataProvider
    {
        #region Internal cache

        private IEnumerable<IEntityType> _entityTypes;
        private const string IsDeletedColumnName = "IsDeleted";

        #endregion


        public GenericMasterDataProvider()
        {
            _entityTypes = new List<IEntityType>();
            _supportedTables = new List<MasterDataTableInfo>()
            {
                new MasterDataTableInfo("Cities","Cities"),
                new MasterDataTableInfo("CostIncomeTypes","Cost Income Types"),
                new MasterDataTableInfo("Currencies","Currencies"),
                new MasterDataTableInfo("ExchangeRates","Exchange Rates"),
                new MasterDataTableInfo("DocumentTypes","Document Types"),
                new MasterDataTableInfo("DocumentSerieTypes","Document Serie Types"),
                new MasterDataTableInfo("EmploymentTypes","Employment Types"),
                new MasterDataTableInfo("BenefitTypes","Benefit Types"),
                new MasterDataTableInfo("BenefitCategories","Benefit Categories"),
                new MasterDataTableInfo("PaybackReasons","Payback Reasons"),
                new MasterDataTableInfo("Genders","Genders"),
                new MasterDataTableInfo("HierarchyLevels","Hierarchy Levels"),
                new MasterDataTableInfo("JobTypes","Job Types"),
                new MasterDataTableInfo("Languages","Languages"),
                new MasterDataTableInfo("MeasureUnits","Measure Units"),
                new MasterDataTableInfo("MeasureUnitTypes","Measure Unit Types"),
                new MasterDataTableInfo("NoticeTypes","Notice Types"),
                new MasterDataTableInfo("PartnerCategories","Partner Categories"),
                new MasterDataTableInfo("PartnerTypes","Partner Types"),
                new MasterDataTableInfo("Relationships","Relationships"),
                new MasterDataTableInfo("States","States")
            };
        }

        public async Task<int> CreateTableDataAsync(IDAProjectContext dbContext, MasterEntityRequestModel requestModel)
        {
            var dbContextType = dbContext.GetType();
            var tableSetProperty = dbContextType.GetProperty(requestModel.TableName)!;

            dynamic tableSet = tableSetProperty.GetValue(dbContext, null)!;

            var entityClrType = tableSetProperty.PropertyType.GenericTypeArguments[0];
            var entity = Activator.CreateInstance(entityClrType);           

            CopyValuesToEntity(requestModel, entity);

            var entityEntry = await tableSet.AddAsync(entity as dynamic);
            await dbContext.SaveChangesAsync();
            
            return entityEntry.Entity.Id;
        }

        
        private List<GenericSelectOption> GetSelectOptions(IDAProjectContext dbContext, IForeignKey foreignKey)
        {
            var relatedEntity = foreignKey.PrincipalEntityType;
            var dbContextType = dbContext.GetType();

            var relTable = relatedEntity.GetTableName();

            var relTableSet = dbContextType.GetProperty(relTable!)!;

            var relDataSource = relTableSet.GetValue(dbContext) as IQueryable;

            var entityType = _entityTypes.Single(x => x.GetSchemaQualifiedTableName() == relTable);
            var entityClrType = entityType!.ClrType;

            dynamic query = AddIsDeletedLabmdaExpressionClause(relDataSource!, entityClrType);

            var pkProperty = GetPrimaryKeyColumn(relatedEntity);

            var options = new List<GenericSelectOption>();

            foreach (var x in query)
            {
                var gso = new GenericSelectOption();
                var t = x.GetType();
                var idProp = t.GetProperty(pkProperty.Name);

                gso.Value = (int)idProp!.GetValue(x)!;
                gso.Description = DataHelpers.SafeString(x.Name);
                options.Add(gso);
            }
            return options;
        }


        public async Task<MasterEntity> GetTableDataAsync(IDAProjectContext dbContext, string tableName, int? id)
        {
            EnsureEntityTypesInitialized(dbContext);
            var result = new MasterEntity(tableName);

            var dbContextType = dbContext.GetType();
            var entityType = _entityTypes.Single(x => string.Equals(x.GetSchemaQualifiedTableName(), tableName, StringComparison.OrdinalIgnoreCase));
            var entityClrType = entityType!.ClrType;
            var entityColumns = entityType.GetDeclaredProperties();

            foreach (var entityColumn in entityColumns)
            {
                if (entityColumn.Name != "DeletedBy" && entityColumn.Name != "DeletedDate" && entityColumn.Name != "IsDeleted" && !entityColumn.IsPrimaryKey())
                {
                    var propField = new MasterEntityField
                    {
                        Name = entityColumn.Name,
                        DisplayName = entityColumn.Name,
                        Type = entityColumn.ClrType.ToString()
                    };
                    result.Fields.Add(propField);

                    if (entityColumn.IsForeignKey())
                    {
                        var foreignKeys = entityColumn.GetContainingForeignKeys();
                        foreach (var foreignKey in foreignKeys)
                        {
                            propField.DisplayName = GetFieldDisplayNameForSelectOptions(foreignKey);
                            propField.Options = GetSelectOptions(dbContext, foreignKey);
                        }
                    }
                }
            }

            var tableSetProperty = dbContextType.GetProperty(tableName)!;
            var pkColumn = GetPrimaryKeyColumn(entityType)!;
            var pkProp = entityClrType.GetProperty(pkColumn.Name);
            dynamic dbSet = tableSetProperty.GetValue(dbContext)!;

            if (id.HasValue)
            {
                var dbRecord = await dbSet.FindAsync(id.Value);

                if (dbRecord != null)
                {
                    var masterEntityRecord = ReadMasterEntityRecord(entityClrType, result.Fields, pkProp!, dbRecord);
                    result.Records.Add(masterEntityRecord);
                }
            }
            else
            {
                dynamic query = AddIsDeletedLabmdaExpressionClause(dbSet!, entityClrType);

                foreach (var dbRecord in query)
                {
                    var masterEntityRecord = ReadMasterEntityRecord(entityClrType, result.Fields, pkProp!, dbRecord);
                    result.Records.Add(masterEntityRecord);
                }
            }

            return result;
        }

        public List<ISelectOption> GetFilteredSelectOptionsByTable(IDAProjectContext dbContext, string tableName, string keyPropertyName, int propertyValue, string descriptionPropertyName)
        {
            EnsureEntityTypesInitialized(dbContext);
            var result = new List<ISelectOption>();

            var dbContextType = dbContext.GetType();
            var entityType = _entityTypes.Single(x => string.Equals(x.GetSchemaQualifiedTableName(), tableName, StringComparison.OrdinalIgnoreCase));
            var entityClrType = entityType!.ClrType;

            var parameterExpression = Expression.Parameter(entityClrType, "x");
            var leftExpression = Expression.PropertyOrField(parameterExpression, keyPropertyName);

            ConstantExpression rightExpression;

            if (leftExpression.Type == typeof(bool) || leftExpression.Type == typeof(bool?))
            {
                var boolValue = propertyValue == 1;
                rightExpression = Expression.Constant(boolValue, leftExpression.Type);
            }
            else
            {
                rightExpression = Expression.Constant(propertyValue, leftExpression.Type);
            }

            var comparisonExpression = Expression.MakeBinary(ExpressionType.Equal, leftExpression, rightExpression);
            var lambdaExpression = Expression.Lambda(comparisonExpression, parameterExpression);

            var tableSetProperty = dbContextType.GetProperty(tableName)!;
            var relDataSource = tableSetProperty.GetValue(dbContext) as IQueryable;

            dynamic query = AddIsDeletedLabmdaExpressionClause(relDataSource!, entityClrType);
            query = Queryable.Where(query, lambdaExpression as dynamic);

            var records = Enumerable.ToList(query);

            var descriptionProperty = entityClrType.GetProperty(descriptionPropertyName);

            foreach (var item in records)
            {
                var gso = new GenericSelectOption();

                gso.Value = item.Id;
                gso.Description = DataHelpers.SafeString(descriptionProperty!.GetValue(item));
                result.Add(gso);
            }
            return result;
        }

        private dynamic AddIsDeletedLabmdaExpressionClause(IQueryable tableSet, Type entityClrType)
        {
            var isDeletedLambdaExpression = CreateIsDeletedLambdaExpression(entityClrType);
            dynamic query;
            if (isDeletedLambdaExpression == null)
            {
                query = tableSet!;
            }
            else
            {
                query = Queryable.Where(tableSet as dynamic, isDeletedLambdaExpression as dynamic);
            }
            return query;
        }

        private LambdaExpression CreateIsDeletedLambdaExpression(Type entityClrType)
        {
            LambdaExpression? result = null;

            // these tables are not the part of the soft delete logic
            if (!entityClrType.Name.StartsWith("AspNet"))
            {
                var parameterExpression = Expression.Parameter(entityClrType, "x");
                var leftExpression = Expression.PropertyOrField(parameterExpression, IsDeletedColumnName);
                var rightExpression = Expression.Constant(false, leftExpression.Type);
                var comparisonExpression = Expression.MakeBinary(ExpressionType.Equal, leftExpression, rightExpression);
                result = Expression.Lambda(comparisonExpression, parameterExpression);
            }
            return result!;
        }


        public List<ISelectOption> GetSelectOptionsByTable(IDAProjectContext dbContext, string tableName, string descriptionExpression)
        {
            EnsureEntityTypesInitialized(dbContext);
            var result = new List<ISelectOption>();

            var dbContextType = dbContext.GetType();
            var entityType = _entityTypes.Single(x => string.Equals(x.GetSchemaQualifiedTableName(), tableName, StringComparison.OrdinalIgnoreCase));
            var entityClrType = entityType!.ClrType;
            
            var tableSetProperty = dbContextType.GetProperty(tableName)!;
            var pkColumn = GetPrimaryKeyColumn(entityType)!;
            var pkProp = entityClrType.GetProperty(pkColumn.Name);

            var idProp = entityClrType.GetProperty(pkColumn.Name);
            var descriptionBindings = new Dictionary<string, PropertyInfo>();

            if (descriptionExpression.Contains("{") && descriptionExpression.Contains("}"))
            {
                var regex = new Regex("{(.*?)}");
                var matches = regex.Matches(descriptionExpression);

                foreach (Match match in matches)
                {
                    if (descriptionBindings.ContainsKey(match.Value) == false)
                    {
                        var propName = match.Value.Replace("{", string.Empty).Replace("}", string.Empty);
                        var nameProp = entityClrType.GetProperty(propName);
                        descriptionBindings.Add(propName, nameProp!);
                    }
                }
            }
            else
            {
                var nameProp = entityClrType.GetProperty(descriptionExpression);
                descriptionBindings.Add(descriptionExpression, nameProp!);
            }            

            var relDataSource = tableSetProperty.GetValue(dbContext) as IQueryable;
            dynamic query = AddIsDeletedLabmdaExpressionClause(relDataSource!, entityClrType);

            foreach (var x in query)
            {
                var gso = new GenericSelectOption();

                gso.Value = (int)idProp!.GetValue(x)!;

                if (descriptionBindings.Count == 1)
                {
                    var desBind = descriptionBindings.First();
                    gso.Description = DataHelpers.SafeString(desBind.Value.GetValue(x));
                }
                else if (descriptionBindings.Count > 1)
                {
                    var displayText = descriptionExpression;
                    foreach(var desBind in descriptionBindings)
                    {
                        displayText = displayText.Replace("{" + desBind.Key + "}", desBind.Value.GetValue(x));
                    }
                    gso.Description = displayText;
                }
                result.Add(gso);
            }

            return result;
        }


        public async Task UpdateTableDataAsync(IDAProjectContext dbContext, MasterEntityRequestModel requestModel)
        {
            var dbContextType = dbContext.GetType();
            var tableSetProperty = dbContextType.GetProperty(requestModel.TableName)!;
            dynamic tableSet = tableSetProperty.GetValue(dbContext, null)!;

            var entityType = _entityTypes.Single(x => string.Equals(x.GetSchemaQualifiedTableName(), requestModel.TableName, StringComparison.OrdinalIgnoreCase));
            var pkColumn = GetPrimaryKeyColumn(entityType)!;

            var idField = requestModel.Fields.Single(x => x.Name == pkColumn.Name);
            var id = (int)MasterDataConverter.GetRawValue(idField.Value, typeof(int))!;

            var dbRecord = await tableSet.FindAsync(id);
            CopyValuesToEntity(requestModel, dbRecord);

            await dbContext.SaveChangesAsync();
        }

        public async Task SoftDeleteByIdAsync(IDAProjectContext dbContext, string tableName, int id, int? deletedByUserId)
        {
            var dbContextType = dbContext.GetType();
            var tableSetProperty = dbContextType.GetProperty(tableName)!;
            dynamic tableSet = tableSetProperty.GetValue(dbContext, null)!;

            var entityType = _entityTypes.Single(x => string.Equals(x.GetSchemaQualifiedTableName(), tableName, StringComparison.OrdinalIgnoreCase));
            var pkColumn = GetPrimaryKeyColumn(entityType)!;

            var dbRecord = await tableSet.FindAsync(id);
            dbRecord.IsDeleted = true;
            dbRecord.DeletedBy = deletedByUserId;
            dbRecord.DeletedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
        }

        #region Private methods

        private IProperty GetPrimaryKeyColumn(IEntityType entityType)
        {
            var props = entityType.GetDeclaredProperties();
            return props.First(x => x.IsPrimaryKey());
        }

        private string GetFieldDisplayNameForSelectOptions(IForeignKey foreignKey)
        {
            var entityType = foreignKey.PrincipalEntityType;
            var displayName = entityType!.DisplayName();
            var result = Regex.Replace(displayName, @"([a-z])([A-Z])", "$1 $2");
            return result;
        }

        private void EnsureEntityTypesInitialized(IDAProjectContext dbContext)
        {
            if (!_entityTypes.Any())
            {
                _entityTypes = dbContext.Model.GetEntityTypes();
            }
        }

        private MasterEntityRecord ReadMasterEntityRecord(Type entityClrType, List<MasterEntityField> masterEntityFields, PropertyInfo primaryKeyProperty, object? dbRecord)
        {
            var masterEntityRecord = new MasterEntityRecord();

            masterEntityRecord.Id = (int)primaryKeyProperty.GetValue(dbRecord)!;

            foreach (var r in masterEntityFields)
            {
                var p = entityClrType.GetProperty(r.Name);

                var v = p!.GetValue(dbRecord);
                var vStr = MasterDataConverter.SerializeRawValue(v);
                masterEntityRecord.Values.Add(vStr);
            }
            return masterEntityRecord;
        }

        public bool IsGenericSelectOptionsProvider()
        {
            return true;
        }



        #endregion
    }
}

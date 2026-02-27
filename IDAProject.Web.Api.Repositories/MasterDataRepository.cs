using Microsoft.EntityFrameworkCore;
using IDAProject.Web.Api.Models.Interfaces.Internal;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Db.MainDatabase;
using IDAProject.Web.Helpers;
using IDAProject.Web.Models.Dto.Common;
using IDAProject.Web.Models.Dto.MasterData;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.MasterData;

namespace IDAProject.Web.Api.Repositories
{
    public class MasterDataRepository : IMasterDataRepository
    {
        private readonly IdaContext _dbContext;
        private readonly IMasterDataProvidersFactory _masterDataProvidersFactory;

        public MasterDataRepository(IdaContext dbContext, IMasterDataProvidersFactory masterDataProvidersFactory)
        {
            _dbContext = dbContext;
            _masterDataProvidersFactory = masterDataProvidersFactory;
        }

        public IEnumerable<MasterDataTableInfo> GetSupportedMasterDataTables()
        {
            var result = _masterDataProvidersFactory.GetSupportedMasterDataTables();
            return result;
        }

        public async Task<int> CreateTableDataAsync(MasterEntityRequestModel requestModel)
        {
            var dataProvider = _masterDataProvidersFactory.GetProvider(requestModel.TableName);
            var id = await dataProvider.CreateTableDataAsync(_dbContext, requestModel);
            return id;
        }

        public async Task<MasterEntity> GetTableDataAsync(string tableName, int? id)
        {
            var dataProvider = _masterDataProvidersFactory.GetProvider(tableName);
            var result = await dataProvider.GetTableDataAsync(_dbContext, tableName, id);
            return result;
        }


        public async Task UpdateTableDataAsync(MasterEntityRequestModel requestModel)
        {
            var dataProvider = _masterDataProvidersFactory.GetProvider(requestModel.TableName);
            await dataProvider.UpdateTableDataAsync(_dbContext, requestModel);
        }

        public async Task SoftDeleteByIdAsync(string tableName, int id, int? deletedByUserId)
        {
            var dataProvider = _masterDataProvidersFactory.GetProvider(tableName);
            await dataProvider.SoftDeleteByIdAsync(_dbContext, tableName, id, deletedByUserId);
        }

        public List<ISelectOption> GetSelectOptionsByTable(string tableName, string descriptionExpression)
        {
            var dataProvider = _masterDataProvidersFactory.GetGenericSelectOptionsProvider();
            return dataProvider.GetSelectOptionsByTable(_dbContext, tableName, descriptionExpression);
        }

        public List<ISelectOption> GetFilteredSelectOptionsByTable(string tableName, string keyPropertyName, int propertyValue, string descriptionPropertyName)
        {
            var dataProvider = _masterDataProvidersFactory.GetGenericSelectOptionsProvider();
            return dataProvider.GetFilteredSelectOptionsByTable(_dbContext, tableName, keyPropertyName, propertyValue, descriptionPropertyName);
        }

        private string GetTableHiddenColumnsKey(string tableName)
        {
            return $"{Constants.TableSettings_HiddenColumns}_{tableName}";
        }

        private string GetTableColumnsOrderKey(string tableName)
        {
            return $"{Constants.TableSettings_ColumnsOrder}_{tableName}";
        }

        public async Task<UserTableSettings> GetTableSettingsAsync(int idUser, string tableName)
        {
            var result = new UserTableSettings(tableName);
            var hcKeyName = GetTableHiddenColumnsKey(tableName);
            var coKeyName = GetTableColumnsOrderKey(tableName);

            var dbRecord = await _dbContext.UserSettings.FirstOrDefaultAsync(x => x.UserId == idUser && x.SettingsKey == hcKeyName);

            if (dbRecord != null && !string.IsNullOrEmpty(dbRecord.StringValue))
            {
                result.HiddenColumns = dbRecord.StringValue!.Split(',').ToList();
            }

            dbRecord = await _dbContext.UserSettings.FirstOrDefaultAsync(x => x.UserId == idUser && x.SettingsKey == coKeyName);

            if (dbRecord != null && !string.IsNullOrEmpty(dbRecord.StringValue))
            {
                result.CustomColumnsOrder = dbRecord.StringValue!.Split(',').ToList();
            }

            return result;
        }

        public async Task UpdateTableSettingsColumnAsync(int idUser, string tableName, string columnName, bool isVisible)
        {
            var keyName = GetTableHiddenColumnsKey(tableName);
            var dbRecord = await _dbContext.UserSettings.FirstOrDefaultAsync(x => x.UserId == idUser && x.SettingsKey == keyName);

            if (dbRecord == null)
            {
                dbRecord = new UserSetting
                {
                    UserId = idUser,
                    SettingsKey = keyName,
                    StringValue = columnName
                };
                _dbContext.UserSettings.Add(dbRecord);
            }
            else
            {
                var hiddenColumns = dbRecord.StringValue!.Split(',').ToList();

                // ideally should be listed only one,
                // but in case of double click we can have 2 simultaneous requests which might produce duplicates
                // so we are checking all
                hiddenColumns.RemoveAll(x => x.Equals(columnName, StringComparison.OrdinalIgnoreCase));

                // just add if it shoud be hidden because we have already removed it from collection of hidden columns
                if (!isVisible)
                {
                    hiddenColumns.Add(columnName);
                }

                hiddenColumns.Sort();
                dbRecord.StringValue = string.Join(',', hiddenColumns);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTableSettingsColumnsOrderAsync(int idUser, string tableName, List<string> columns)
        {
            var keyName = GetTableColumnsOrderKey(tableName);
            var dbRecord = await _dbContext.UserSettings.FirstOrDefaultAsync(x => x.UserId == idUser && x.SettingsKey == keyName);

            if (dbRecord == null)
            {
                dbRecord = new UserSetting
                {
                    UserId = idUser,
                    SettingsKey = keyName
                };
                _dbContext.UserSettings.Add(dbRecord);
            }

            dbRecord.StringValue = string.Join(',', columns);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<GeneralSettingDto> GetGeneralSettingsAsync()
        {
            var query = from gs in _dbContext.GeneralSettings
                        select new GeneralSettingDto
                        {
                            Id = gs.Id,
                            DateFormat = gs.DateFormat,
                            CurrencyId = gs.CurrencyId,
                            DecimalPlaces = gs.DecimalPlaces,
                            MeasureFuel = DataHelpers.CloneObjectWithIL<MeasureUnit, MeasureUnitDto>(gs.MeasureFuel)!,
                            MeasureTraveledWay = DataHelpers.CloneObjectWithIL<MeasureUnit, MeasureUnitDto>(gs.MeasureTraveledWay)!,
                            MeasureVehicleLength = DataHelpers.CloneObjectWithIL<MeasureUnit, MeasureUnitDto>(gs.MeasureVehicleLength)!,
                            MeasureVehicleWeight = DataHelpers.CloneObjectWithIL<MeasureUnit, MeasureUnitDto>(gs.MeasureVehicleWeight)!,
                            ReminderDaysIfta = gs.ReminderDaysIfta,
                            ReminderDaysSafetyTest = gs.ReminderDaysSafetyTest,
                            EmployeeGroupedView = gs.EmployeeGroupedView,
                            ReminderDaysAdot = gs.ReminderDaysAdot,
                            ReminderDaysDriverLicense = gs.ReminderDaysDriverLicense,
                            ReminderDaysLicensePlate = gs.ReminderDaysLicensePlate,
                            VehicleGroupedView = gs.VehicleGroupedView,
                            ReminderEmployeeCertificates = gs.ReminderEmployeeCertificates,
                            ReminderAdvanceCosts = gs.ReminderAdvanceCosts,
                            ReminderMaintenancePlan = gs.ReminderMaintenancePlan,
                            ReminderMaintenanceMileage = gs.ReminderMaintenanceMileage,
                            ReminderMaintenanceWorkingHours = gs.ReminderMaintenanceWorkingHours,
                            LocationCode = gs.LocationCode,
                            MessageOfTheDay = gs.MessageOfTheDay
                        };

            var result = await query.FirstAsync();

            result.MeasureVehicleVolume = new MeasureUnitDto
            {
                Sign = result.MeasureVehicleLength.Sign + "³"
            };

			return result;
        }
        public async Task<GeneralSettingDto> GetGeneralSettingsByLocationAsync(string locationCode)
        {
            var query = from gs in _dbContext.GeneralSettings where gs.LocationCode == locationCode
                        select new GeneralSettingDto
                        {
                            Id = gs.Id,
                            DateFormat = gs.DateFormat,
                            DecimalPlaces = gs.DecimalPlaces,
                            MeasureFuel = DataHelpers.CloneObjectWithIL<MeasureUnit, MeasureUnitDto>(gs.MeasureFuel)!,
                            MeasureTraveledWay = DataHelpers.CloneObjectWithIL<MeasureUnit, MeasureUnitDto>(gs.MeasureTraveledWay)!,
                            MeasureVehicleLength = DataHelpers.CloneObjectWithIL<MeasureUnit, MeasureUnitDto>(gs.MeasureVehicleLength)!,
                            MeasureVehicleWeight = DataHelpers.CloneObjectWithIL<MeasureUnit, MeasureUnitDto>(gs.MeasureVehicleWeight)!,
                            ReminderDaysIfta = gs.ReminderDaysIfta,
                            ReminderDaysSafetyTest = gs.ReminderDaysSafetyTest,
                            EmployeeGroupedView = gs.EmployeeGroupedView,
                            ReminderDaysAdot = gs.ReminderDaysAdot,
                            ReminderDaysDriverLicense = gs.ReminderDaysDriverLicense,
                            ReminderDaysLicensePlate = gs.ReminderDaysLicensePlate,
                            VehicleGroupedView = gs.VehicleGroupedView,
                            ReminderEmployeeCertificates = gs.ReminderEmployeeCertificates,
                            ReminderAdvanceCosts = gs.ReminderAdvanceCosts,
                            ReminderMaintenancePlan = gs.ReminderMaintenancePlan,
                            ReminderMaintenanceMileage = gs.ReminderMaintenanceMileage,
                            ReminderMaintenanceWorkingHours = gs.ReminderMaintenanceWorkingHours,
                            LocationCode = gs.LocationCode,
                            MessageOfTheDay = gs.MessageOfTheDay,
                            LeftBanner = gs.LeftBanner,
                            RightBanner = gs.RightBanner,
                            FullPageAd = gs.FullPageAd

						};

            var result = await query.FirstAsync();

            result.MeasureVehicleVolume = new MeasureUnitDto
            {
                Sign = result.MeasureVehicleLength.Sign + "³"
            };

			return result;
        }

        public async Task<int> SaveGeneralSettingAsync(SaveGeneralSettingRequestModel requestModel)
        {
            GeneralSetting? dbRecord;
            if (requestModel.Id > 0)
            {
                dbRecord = await _dbContext.GeneralSettings.SingleAsync(x => x.Id == requestModel.Id);
                DataHelpers.CopyObjectWithIL(requestModel, dbRecord);
            }
            else
            {
                dbRecord = DataHelpers.CloneObjectWithIL<SaveGeneralSettingRequestModel, GeneralSetting>(requestModel);
                _dbContext.GeneralSettings.Add(dbRecord!);
            }

            await _dbContext.SaveChangesAsync();
            return dbRecord!.Id;
        }
    }
}

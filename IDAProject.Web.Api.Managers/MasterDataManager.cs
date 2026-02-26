using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.Models.Dto.Common;
using IDAProject.Web.Models.Dto.MasterData;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.MasterData;

namespace IDAProject.Web.Api.Managers
{
    public class MasterDataManager : IMasterDataManager
    {
        private readonly IMasterDataRepository _masterDataRepository;
        private readonly ILogger _logger;

        public MasterDataManager(ILogger<SecurityManager> logger, IMasterDataRepository masterDataRepository)
        {
            _logger = logger;
            _masterDataRepository = masterDataRepository;
        }

        public async Task<ResponseModel<int>> CreateTableDataAsync(MasterEntityRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _masterDataRepository.CreateTableDataAsync(requestModel);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModelBase> SoftDeleteByIdAsync(string tableName, int id, int? deletedByUserId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _masterDataRepository.SoftDeleteByIdAsync(tableName, id, deletedByUserId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"tableName: {tableName}, id: {id}, deletedByUserId: {deletedByUserId}");
            }
            return result;
        }


        public ResponseModel<IEnumerable<MasterDataTableInfo>> GetSupportedMasterDataTables()
        {
            var result = new ResponseModel<IEnumerable<MasterDataTableInfo>>();
            try
            {
                result.Payload = _masterDataRepository.GetSupportedMasterDataTables();
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, nameof(GetSupportedMasterDataTables));
            }
            return result;
        }

        public async Task<ResponseModel<MasterEntity>> GetTableDataAsync(string tableName, int? id)
        {
            var result = new ResponseModel<MasterEntity>();

            try
            {
                result.Payload = await _masterDataRepository.GetTableDataAsync(tableName, id);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"tableName: {tableName}");
            }
            return result;
        }

        public ResponseModelList<ISelectOption> GetSelectOptionsByTable(string tableName, string descriptionExpression)
        {
            var result = new ResponseModelList<ISelectOption>();
            try
            {
                result.Payload = _masterDataRepository.GetSelectOptionsByTable(tableName, descriptionExpression);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"tableName: {tableName}");
            }
            return result;
        }      

        public ResponseModelList<ISelectOption> GetFilteredSelectOptionsByTable(string tableName, string keyPropertyName, int propertyValue, string descriptionPropertyName)
        {
            var result = new ResponseModelList<ISelectOption>();
            try
            {
                result.Payload = _masterDataRepository.GetFilteredSelectOptionsByTable(tableName, keyPropertyName, propertyValue, descriptionPropertyName);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"tableName: {tableName}");
            }
            return result;
        }

        public async Task<ResponseModelBase> UpdateTableDataAsync(MasterEntityRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                await _masterDataRepository.UpdateTableDataAsync(requestModel);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModel<UserTableSettings>> GetTableSettingsAsync(int idUser, string tableName)
        {
            var result = new ResponseModel<UserTableSettings>();
            try
            {
                result.Payload = await _masterDataRepository.GetTableSettingsAsync(idUser, tableName);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;

                _logger.LogError(e, $"idUser: {idUser}, tableName: {tableName}");
            }
            return result;
        }

        public async Task<ResponseModelBase> UpdateTableSettingsColumnAsync(UpdateTableSettingsColumnVisibilityRequestModel requestModel)
        {
            var result = new ResponseModelBase();
            try
            {
                await _masterDataRepository.UpdateTableSettingsColumnAsync(requestModel.IdUser, requestModel.TableName, requestModel.ColumnName, requestModel.IsVisible);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModelBase> UpdateTableSettingsColumnsOrderAsync(UpdateTableSettingsColumnsOrderRequestModel requestModel)
        {
            var result = new ResponseModelBase();
            try
            {
                if (requestModel.Columns == null)
                {
                    requestModel.Columns = new List<string>();
                }
                await _masterDataRepository.UpdateTableSettingsColumnsOrderAsync(requestModel.IdUser, requestModel.TableName, requestModel.Columns);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModel<GeneralSettingDto>> GetGeneralSettingsAsync()
        {
            var result = new ResponseModel<GeneralSettingDto>();
            try
            {
                result.Payload = await _masterDataRepository.GetGeneralSettingsAsync();
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, "NO ARGS");
            }
            return result;
        }
        public async Task<ResponseModel<GeneralSettingDto>> GetGeneralSettingsByLocationAsync(string locationCode)
        {
            var result = new ResponseModel<GeneralSettingDto>();
            try
            {
                result.Payload = await _masterDataRepository.GetGeneralSettingsByLocationAsync(locationCode);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, "NO ARGS");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SaveGeneralSettingAsync(SaveGeneralSettingRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _masterDataRepository.SaveGeneralSettingAsync(requestModel);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

    }
}
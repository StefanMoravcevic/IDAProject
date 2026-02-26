using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Common;
using IDAProject.Web.Models.Dto.MasterData;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.MasterData;

namespace IDAProject.Web.Admin.Managers
{
    public class MasterDataManager : BaseManager, IMasterDataManager
    {
        private IEnumerable<MasterDataTableInfo> _supportedMasterDataTables;

        public MasterDataManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<MasterDataManager> logger, IHttpContextAccessor httpContextAccessor) 
            : base(httpClientFactory, configuration, logger)
        {
            _supportedMasterDataTables = new List<MasterDataTableInfo>();
        }

        public async Task<ResponseModelBase> CreateTableDataAsync(MasterEntityRequestModel requestModel)
        {
            var result = await PostAsync<MasterEntityRequestModel, ResponseModelBase>($"api/masterData", requestModel);
            return result;
        }

        public async Task<ResponseModel<MasterEntity>> GetRecordByIdAsync(string tableName, int id)
        {
            var result = await GetAsync<ResponseModel<MasterEntity>>($"api/masterData/{tableName}/{id}");
            return result;
        }

        public async Task<IEnumerable<MasterDataTableInfo>> GetSupportedMasterDataTables()
        {
            if (!_supportedMasterDataTables.Any())
            {
                var result = await GetAsync<ResponseModel<IEnumerable<MasterDataTableInfo>>>($"api/masterData/supportedMasterDataTables");
                if (result.Valid)
                {
                    _supportedMasterDataTables = result.Payload!;
                }
            }
            return _supportedMasterDataTables;
        }

        public async Task<ResponseModel<MasterEntity>> GetTableDataAsync(string tableName)
        {
            var result = await GetAsync<ResponseModel<MasterEntity>>($"api/masterData/{tableName}");
            return result;
        }

        public async Task<ResponseModelBase> UpdateTableDataAsync(MasterEntityRequestModel requestModel)
        {
            var result = await PutAsync<MasterEntityRequestModel, ResponseModelBase>($"api/masterData", requestModel);
            return result;
        }

        public async Task<ResponseModel<UserTableSettings>> GetTableSettingsAsync(int idUser, string tableName)
        {
            var result = await GetAsync<ResponseModel<UserTableSettings>>($"api/masterData/tableSettingsColumn/{idUser}/{tableName}");

            if(result.Payload == null)
            {
                result.Payload = new UserTableSettings(tableName);
            }

            return result;
        }

        public async Task<IEnumerable<ISelectOption>> GetSelectOptionsByTableAsync(string tableName, string descriptionExpression)
        {
            var result = new List<GenericSelectOption>();

            var responseModel = await GetAsync<ResponseModelList<GenericSelectOption>>($"api/masterData/options/{tableName}/{descriptionExpression}");

            if(responseModel.Payload != null)
            {
                result = responseModel.Payload;
            }

            return result;
        }

        public async Task<IEnumerable<ISelectOption>> GetFilteredSelectOptionsByTable(string tableName, string keyColumnName, int columnValue, string descriptionColumnName)
        {
            var result = new List<GenericSelectOption>();

            var responseModel = await GetAsync<ResponseModelList<GenericSelectOption>>($"api/masterData/filtered-options/{tableName}/{keyColumnName}/{columnValue}/{descriptionColumnName}");

            if (responseModel.Payload != null)
            {
                result = responseModel.Payload;
            }

            return result;
        }

        public async Task<IEnumerable<ISelectOption>> GetFilteredSelectOptionsByTable(string tableName, string keyColumnName, int columnValue)
        {
            var result = await GetFilteredSelectOptionsByTable(tableName, keyColumnName, columnValue, "Name");
            return result;
        }

        public async Task<ResponseModelBase> UpdateTableSettingsColumnAsync(UpdateTableSettingsColumnVisibilityRequestModel requestModel)
        {
            var result = await PutAsync<UpdateTableSettingsColumnVisibilityRequestModel, ResponseModelBase> ($"api/masterData/tableSettingsColumn", requestModel);
            return result;
        }

        public async Task<ResponseModelBase> UpdateTableSettingsColumnsOrderAsync(UpdateTableSettingsColumnsOrderRequestModel requestModel)
        {
            var result = await PutAsync<UpdateTableSettingsColumnsOrderRequestModel, ResponseModelBase>($"api/masterData/tableSettingsColumnsOrder", requestModel);
            return result;
        }

        public async Task<GeneralSettingDto> GetGeneralSettingsAsync()
        {
            var result = new GeneralSettingDto();

            var responseModel = await GetAsync<ResponseModel<GeneralSettingDto>>($"api/masterData/generalSettings");

            if (responseModel.Payload != null)
            {
                result = responseModel.Payload;
            }

            return result;
        }
        public async Task<GeneralSettingDto> GetGeneralSettingsByLocationAsync(string locationCode)
        {
            var result = new GeneralSettingDto();

            var responseModel = await GetAsync<ResponseModel<GeneralSettingDto>>($"api/masterData/generalSettingsByLocation/{locationCode}");

            if (responseModel.Payload != null)
            {
                result = responseModel.Payload;
            }

            return result;
        }

        public async Task<ResponseModel<int>> SaveGeneralSettingAsync(SaveGeneralSettingRequestModel requestModel)
        {
            var result = await PostAsync<SaveGeneralSettingRequestModel, ResponseModel<int>>($"api/masterData/generalSettings", requestModel);
            return result;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Common;
using IDAProject.Web.Models.Dto.MasterData;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.MasterData;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterDataManager _masterDataManager;

        public MasterDataController(IMasterDataManager masterDataManager)
        {
            _masterDataManager = masterDataManager;
        }

        [HttpGet("supportedMasterDataTables")]
        public ResponseModel<IEnumerable<MasterDataTableInfo>> GetSupportedMasterDataTables()
        {
            var response = _masterDataManager.GetSupportedMasterDataTables();
            return response;
        }

        [HttpGet("{tableName}/{id?}")]
        public async Task<ResponseModel<MasterEntity>> GetAsync(string tableName, int? id)
        {
            var response = await _masterDataManager.GetTableDataAsync(tableName, id);
            return response;
        }

        [HttpGet("options/{tableName}/{descriptionExpression}")]
        public ResponseModelList<ISelectOption> GetSelectOptionsByTable(string tableName, string descriptionExpression)
        {
            var response = _masterDataManager.GetSelectOptionsByTable(tableName, descriptionExpression);
            return response;
        }

        [HttpGet("filtered-options/{tableName}/{keyColumnName}/{columnValue}/{descriptionColumnName}")]
        public ResponseModelList<ISelectOption> GetFilteredSelectOptionsByTable(string tableName, string keyColumnName, int columnValue, string descriptionColumnName)
        {
            var response = _masterDataManager.GetFilteredSelectOptionsByTable(tableName, keyColumnName, columnValue, descriptionColumnName);
            return response;
        }


        [HttpPost]
        public async Task<ResponseModelBase> CreateAsync(MasterEntityRequestModel requestModel)
        {
            var response = await _masterDataManager.CreateTableDataAsync(requestModel);
            return response;
        }

        [HttpDelete("{tableName}/{id}/{deletedByUserId?}")]
        public async Task<ResponseModelBase> SoftDeleteByIdAsync(string tableName, int id, int? deletedByUserId)
        {
            var response = await _masterDataManager.SoftDeleteByIdAsync(tableName, id, deletedByUserId);
            return response;
        }

        [HttpPut]
        public async Task<ResponseModelBase> UpdateAsync(MasterEntityRequestModel requestModel)
        {
            var response = await _masterDataManager.UpdateTableDataAsync(requestModel);
            return response;
        }


        [HttpGet("tableSettingsColumn/{idUser}/{tableName}")]
        public async Task<ResponseModel<UserTableSettings>> GetTableSettingsAsync(int idUser, string tableName)
        {
            var response = await _masterDataManager.GetTableSettingsAsync(idUser, tableName);
            return response;
        }

        [HttpPut("tableSettingsColumn")]
        public async Task<ResponseModelBase> UpdateTableSettingsColumnAsync(UpdateTableSettingsColumnVisibilityRequestModel requestModel)
        {
            var response = await _masterDataManager.UpdateTableSettingsColumnAsync(requestModel);
            return response;
        }

        [HttpPut("tableSettingsColumnsOrder")]
        public async Task<ResponseModelBase> UpdateTableSettingsColumnsOrderAsync(UpdateTableSettingsColumnsOrderRequestModel requestModel)
        {
            var response = await _masterDataManager.UpdateTableSettingsColumnsOrderAsync(requestModel);
            return response;
        }

        [HttpGet("generalSettings")]
        public async Task<ResponseModel<GeneralSettingDto>> GetGeneralSettingsAsync()
        {
            var response = await _masterDataManager.GetGeneralSettingsAsync();
            return response;
        }
        [HttpGet("generalSettingsByLocation/{locationCode}")]
        public async Task<ResponseModel<GeneralSettingDto>> GetGeneralSettingsByLocationAsync(string locationCode)
        {
            var response = await _masterDataManager.GetGeneralSettingsByLocationAsync(locationCode);
            return response;
        }

        [HttpPost("generalSettings")]
        public async Task<ResponseModel<int>> SaveGeneralSettingAsync(SaveGeneralSettingRequestModel requestModel)
        {
            var response = await _masterDataManager.SaveGeneralSettingAsync(requestModel);
            return response;
        }

    }
}
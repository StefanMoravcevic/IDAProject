using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.TagHelpers;
using IDAProject.Web.Admin.Models.ViewModels;
using IDAProject.Web.Models.Dto.Common;
using IDAProject.Web.Models.Dto.MasterData;
using IDAProject.Web.Models.RequestModels.MasterData;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class MasterDataController : BaseController
    {
        private readonly IMasterDataManager _masterDataManager;

        public MasterDataController(ILogger<MasterDataController> logger, IAccountManager accountManager, IMasterDataManager masterDataManager)
            : base(accountManager, logger)
        {
            _masterDataManager = masterDataManager;
        }

        #region Core CRUD operations

        [HttpGet("{tableName}", Name = RouteNames.MasterData_List)]
        public async Task<IActionResult> Index(string tableName)
        {
            var viewModel = new PageViewModel<MasterEntity>();
            viewModel.User = GetCurrentUser();

            try
            {
                var masterDataResponse = await _masterDataManager.GetTableDataAsync(tableName);

                if (masterDataResponse.Valid)
                {
                    viewModel.Data = masterDataResponse.Payload;
                }
                else
                {
                    viewModel.Notification = new NotificationViewModel
                    {
                        Message = masterDataResponse.Message,
                        Type = NotificationType.Error
                    };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Table name: {tableName}");
                viewModel.Notification = new NotificationViewModel
                {
                    Message = e.Message,
                    Type = NotificationType.Error
                };
            }

            return View(viewModel);
        }

        [HttpGet("{tableName}/{id}", Name = RouteNames.MasterData_Edit)]
        public async Task<IActionResult> GetEditModalAsync(string tableName, int id)
        {
            try
            {
                var masterDataResponse = await _masterDataManager.GetRecordByIdAsync(tableName, id);
                return PartialView("EditRecordModalContent", masterDataResponse.Payload);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Table name: {tableName}, id:{id}");
                throw;
            }
        }

        [HttpPost(Name = RouteNames.MasterData_Create)]
        public async Task<IActionResult> CreateAsync(MasterEntityRequestModel requestModel)
        {
            try
            {
                var response = await _masterDataManager.CreateTableDataAsync(requestModel);
                return Json(response);
            }
            catch (Exception e)
            {
                var requestData = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, requestData);
                throw;
            }
        }

        [HttpPut(Name = RouteNames.MasterData_Update)]
        public async Task<IActionResult> UpdateTableDataAsync(MasterEntityRequestModel requestModel)
        {
            try
            {
                var response = await _masterDataManager.UpdateTableDataAsync(requestModel);
                return Json(response);
            }
            catch (Exception e)
            {
                var requestData = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, requestData);
                throw;
            }
        }

        #endregion

        #region User settings

        [HttpPut("tableSettingsColumn", Name = RouteNames.MasterDataTableColumn_Update)]
        public async Task UpdateTableSettingsColumnAsync(UpdateTableSettingsColumnVisibilityRequestModel requestModel)
        {
            var user = GetCurrentUser();
            try
            {
                requestModel.IdUser = user.Id;
                await _masterDataManager.UpdateTableSettingsColumnAsync(requestModel);
            }
            catch (Exception e)
            {
                var requestData = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, requestData);
            }
        }

        [HttpGet("tableSettingsColumn/{tableName}", Name = RouteNames.MasterDataTableColumn_Get)]
        public async Task<JsonResult> GetTableSettingsAsync(string tableName)
        {
            var user = GetCurrentUser();
            var result = new UserTableSettings(tableName);

            try
            {
                var response = await _masterDataManager.GetTableSettingsAsync(user.Id, tableName);
                if (response.Valid)
                {
                    result = response.Payload;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"idUser: {user.Id}, tableName: {tableName}");
            }

            // Safe/non-blocking endpoint, always retuns a value. In case of error it's logged in API and AdminUI
            return Json(result);
        }

        [HttpPut("tableSettingsColumnsOrder", Name = RouteNames.MasterDataTableColumnOrder_Update)]
        public async Task<JsonResult> UpdateTableSettingsColumnsOrderAsync(UpdateTableSettingsColumnsOrderRequestModel requestModel)
        {
            var user = GetCurrentUser();
            try
            {
                requestModel.IdUser = user.Id;
                var result = await _masterDataManager.UpdateTableSettingsColumnsOrderAsync(requestModel);
                return Json(result);
            }
            catch (Exception e)
            {
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");
                throw;
            }
        }

        #endregion

    }
}
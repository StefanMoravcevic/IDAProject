using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Shifts;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Shifts;

namespace IDAProject.Web.Admin.Managers
{
    public class ShiftsManager : BaseManager, IShiftsManager
    {
        public ShiftsManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<ShiftsManager> logger) :
            base(httpClientFactory, configuration, logger)
        {
        }
        public async Task<ResponseModelList<ShiftDto>> SearchShiftsAsync(SearchShiftsParams searchParams)
        {
            var result =
                await PostAsync<SearchShiftsParams, ResponseModelList<ShiftDto>>($"api/Shifts/search",
                    searchParams);
            return result;
        }

        public async Task<ResponseModel<ShiftDto>> GetShiftByIdAsync(int id)
        {
            var result = await GetAsync<ResponseModel<ShiftDto>>($"api/Shifts/{id}");
            return result;
        }

        public async Task<ResponseModelBase> DeleteShiftAsync(int id, int? userId)
        {
            var result = await DeleteAsync<ResponseModelBase>($"api/Shifts/delete/{id}/{userId}");
            return result;
        }

        public async Task<ResponseModel<int>> SaveShiftAsync(SaveShiftRequestModel requestModel)
        {
            var result = await PostAsync<SaveShiftRequestModel, ResponseModel<int>>($"api/Shifts", requestModel);
            return result;
        }
    }
}

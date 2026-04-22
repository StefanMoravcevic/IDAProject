using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Shifts;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Shifts;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly IShiftsManager _ShiftsManager;

        public ShiftsController(IShiftsManager ShiftsManager)
        {
            _ShiftsManager = ShiftsManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<ShiftDto>> GetShiftByIdAsync(int id)
        {
            var response = await _ShiftsManager.GetShiftByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteShiftAsync(int id, int? userId)
        {
            var response = await _ShiftsManager.DeleteShiftAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<ShiftDto>> SearchShiftsAsync(SearchShiftsParams searchParams)
        {
            var response = await _ShiftsManager.SearchShiftsAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveShiftAsync(SaveShiftRequestModel requestModel)
        {
            var response = await _ShiftsManager.SaveShiftAsync(requestModel);
            return response;
        }
    }
}

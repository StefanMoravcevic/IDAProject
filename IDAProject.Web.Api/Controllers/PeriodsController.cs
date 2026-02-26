using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.Periods;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Periods;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodsController : ControllerBase
    {
        private readonly IPeriodsManager _PeriodsManager;

        public PeriodsController(IPeriodsManager PeriodsManager)
        {
            _PeriodsManager = PeriodsManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<PeriodDto>> GetPeriodByIdAsync(int id)
        {
            var response = await _PeriodsManager.GetPeriodByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeletePeriodAsync(int id, int? userId)
        {
            var response = await _PeriodsManager.DeletePeriodAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<PeriodDto>> SearchPeriodsAsync(SearchPeriodsParams searchParams)
        {
            var response = await _PeriodsManager.SearchPeriodsAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SavePeriodAsync(SavePeriodRequestModel requestModel)
        {
            var response = await _PeriodsManager.SavePeriodAsync(requestModel);
            return response;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.RegularActivities;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.RegularActivities;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegularActivitiesController : ControllerBase
    {
        private readonly IRegularActivitiesManager _RegularActivitiesManager;

        public RegularActivitiesController(IRegularActivitiesManager RegularActivitiesManager)
        {
            _RegularActivitiesManager = RegularActivitiesManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<RegularActivityDto>> GetRegularActivityByIdAsync(int id)
        {
            var response = await _RegularActivitiesManager.GetRegularActivityByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteRegularActivityAsync(int id, int? userId)
        {
            var response = await _RegularActivitiesManager.DeleteRegularActivityAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<RegularActivityDto>> SearchRegularActivitiesAsync(SearchRegularActivitiesParams searchParams)
        {
            var response = await _RegularActivitiesManager.SearchRegularActivitiesAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveRegularActivityAsync(SaveRegularActivityRequestModel requestModel)
        {
            var response = await _RegularActivitiesManager.SaveRegularActivityAsync(requestModel);
            return response;
        }
    }
}

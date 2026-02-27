using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Models.Dto.IdaTasks;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.IdaTasks;

namespace IDAProject.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdaTasksController : ControllerBase
    {
        private readonly IIdaTasksManager _IdaTasksManager;

        public IdaTasksController(IIdaTasksManager IdaTasksManager)
        {
            _IdaTasksManager = IdaTasksManager;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel<IdaTaskDto>> GetIdaTaskByIdAsync(int id)
        {
            var response = await _IdaTasksManager.GetIdaTaskByIdAsync(id);
            return response;
        }

        [HttpDelete("delete/{id}/{userId}")]
        public async Task<ResponseModelBase> DeleteIdaTaskAsync(int id, int? userId)
        {
            var response = await _IdaTasksManager.DeleteIdaTaskAsync(id,userId);
            return response;
        }

        [HttpPost("search")]
        public async Task<ResponseModelList<IdaTaskDto>> SearchIdaTasksAsync(SearchIdaTasksParams searchParams)
        {
            var response = await _IdaTasksManager.SearchIdaTasksAsync(searchParams);
            return response;
        }

        [HttpPost]
        public async Task<ResponseModel<int>> SaveIdaTaskAsync(SaveIdaTaskRequestModel requestModel)
        {
            var response = await _IdaTasksManager.SaveIdaTaskAsync(requestModel);
            return response;
        }
    }
}

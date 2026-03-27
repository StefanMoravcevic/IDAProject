using IDAProject.Web.Admin.Managers;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class GoogleController : BaseController
    {
        private readonly IGoogleManager _googleManager;
        private readonly IConfiguration _configuration;
        public GoogleController(ILogger<GoogleController> logger, IGoogleManager googleManager, IAccountManager accountManager, IConfiguration configuration) : base(accountManager, logger)
        {
            _googleManager = googleManager;
            _configuration = configuration;
        }
        [HttpGet("callback")]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string state)
        {
            int employeeId = int.Parse(state);
            var redirectUri = "http://localhost:7136/google/callback";
            var result = await _googleManager.HandleOAuthCallbackAsync(code, state, redirectUri);

            return RedirectToAction("Index", "Home");
        }
    }
}

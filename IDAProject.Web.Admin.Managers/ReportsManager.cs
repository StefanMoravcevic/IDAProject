using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.General;

namespace IDAProject.Web.Admin.Managers
{

    public class ReportsManager : BaseManager, IReportsManager
    {
        public ReportsManager(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<ReportsManager> logger , IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, configuration, logger)
        {
        }
    }
}

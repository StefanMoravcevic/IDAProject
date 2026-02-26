using Microsoft.AspNetCore.Mvc;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.General.Enums;
using IDAProject.Web.Models.Interfaces.Html;
using IDAProject.Web.Models.RequestModels.Employees;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    public class ReportsController : BaseController
    {
        private readonly IMasterDataManager _masterDataManager;
        private readonly IReportsManager _reportsManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ReportsController(
            ILogger logger,
            IStringLocalizer<SharedResources> localizer,
            IAccountManager accountManager,
            IMasterDataManager masterDataManager,
            IReportsManager reportsManager)
            : base(accountManager, logger)
        {
            _masterDataManager = masterDataManager;
            _reportsManager = reportsManager;
            _localizer = localizer;
        }


    }
}
using IDAProject.Web.Models.Dto.ScannedLines;
using IDAProject.Web.Models.General;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.ScannedLines
{
    public class ScannedLinesListViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public ScannedLinesListViewModel(IStringLocalizer<SharedResources> localizer)
        {
            ScannedLines = new List<ScannedLineDto>();
            _localizer = localizer;
        }

        public List<ScannedLineDto> ScannedLines { get; set; }
    }
}
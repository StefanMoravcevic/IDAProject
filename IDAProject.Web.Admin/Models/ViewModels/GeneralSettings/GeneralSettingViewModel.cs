using IDAProject.Web.Models.Dto.Common;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.GeneralSettings
{
    public class GeneralSettingViewModel : NavigationBaseViewModel
    {
        public GeneralSettingViewModel()
        {
            GeneralSetting = new GeneralSettingDto();
            MeasureUnits = new List<GenericSelectOption>();
            Currencies = new List<GenericSelectOption>();

        }

        public GeneralSettingDto GeneralSetting { get; set; }

        public IEnumerable<ISelectOption> MeasureUnits { get; set; }
        public IEnumerable<ISelectOption> Currencies { get; set; }
        public int ReadOnly { get; set; }
    }
}

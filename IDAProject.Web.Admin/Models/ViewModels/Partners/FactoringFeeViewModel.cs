using IDAProject.Web.Models.Dto.Common;
using IDAProject.Web.Models.Dto.Partners;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Partners
{
    public class FactoringFeeViewModel : NavigationBaseViewModel
    {
        public FactoringFeeViewModel()
        {
            Fee = new FactoringFeeDto();
            GeneralSetting = new GeneralSettingDto();
            Companies = new List<GenericSelectOption>();
        }

        public FactoringFeeDto Fee { get; set; }
        public GeneralSettingDto GeneralSetting { get; set; }

        public IEnumerable<ISelectOption> Companies { get; set; }
        public int ReadOnly { get; set; }
    }
}

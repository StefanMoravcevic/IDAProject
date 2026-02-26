using IDAProject.Web.Models.Dto.Common;
using IDAProject.Web.Models.Dto.Partners;

namespace IDAProject.Web.Admin.Models.ViewModels.Partners
{
    public class SubcontractorFeeViewModel : NavigationBaseViewModel
    {
        public SubcontractorFeeViewModel()
        {
            Fee = new SubcontractorFeeDto();
            GeneralSetting = new GeneralSettingDto();
            SubcontractorName = string.Empty;
        }

        public SubcontractorFeeDto Fee { get; set; }

        public int ReadOnly { get; set; }

        public GeneralSettingDto GeneralSetting { get; set; }

        public string SubcontractorName { get; set; }
    }
}

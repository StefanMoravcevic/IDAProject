using IDAProject.Web.Models.Dto.Periods;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.Periods
{
    public class PeriodViewModel : NavigationBaseViewModel
    {
        public PeriodViewModel()
        {
            Period = new PeriodDto();
        }
        public PeriodDto Period { get; set; }
        public int ReadOnly { get; set; }

    }
}

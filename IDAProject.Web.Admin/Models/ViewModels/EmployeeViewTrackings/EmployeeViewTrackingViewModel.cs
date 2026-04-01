using DeclarationFactory.Web.Models.Dto.EmployeeViewTrackings;
using DeclarationFactory.Web.Models.General;
using DeclarationFactory.Web.Models.Interfaces.Html;

namespace DeclarationFactory.Web.Admin.Models.ViewModels.EmployeeViewTrackings
{
    public class EmployeeViewTrackingViewModel : NavigationBaseViewModel
    {
        public EmployeeViewTrackingViewModel()
        {
            EmployeeViewTracking = new EmployeeViewTrackingDto();
        }
        public EmployeeViewTrackingDto EmployeeViewTracking { get; set; }

    }
}

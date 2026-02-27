using IDAProject.Web.Models.Dto.RegularActivities;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.RegularActivities
{
    public class RegularActivityViewModel : NavigationBaseViewModel
    {
        public RegularActivityViewModel()
        {
            RegularActivity = new RegularActivityDto();
        }
        public RegularActivityDto RegularActivity { get; set; }

    }
}

using IDAProject.Web.Models.Dto.RegularActivities;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.RegularActivities
{
    public class RegularActivityViewModel : NavigationBaseViewModel
    {
        public RegularActivityViewModel()
        {
            RegularActivity = new List<RegularActivityDto>();
        }
        public List<RegularActivityDto> RegularActivity { get; set; }

        public int? UserId { get; set; }

    }
}

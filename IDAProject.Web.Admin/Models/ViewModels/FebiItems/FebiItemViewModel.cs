using IDAProject.Web.Models.Dto.FebiItems;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.FebiItems
{
    public class FebiItemViewModel : NavigationBaseViewModel
    {
        public FebiItemViewModel()
        {
            FebiItem = new FebiItemDto();
        }
        public FebiItemDto FebiItem { get; set; }

    }
}

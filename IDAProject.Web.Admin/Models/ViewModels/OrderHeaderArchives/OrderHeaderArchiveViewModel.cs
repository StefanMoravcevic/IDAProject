using IDAProject.Web.Models.Dto.OrderHeaderArchives;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.OrderHeaderArchives
{
    public class OrderHeaderArchiveViewModel : NavigationBaseViewModel
    {
        public OrderHeaderArchiveViewModel()
        {
            OrderHeaderArchive = new OrderHeaderArchiveDto();
        }
        public OrderHeaderArchiveDto OrderHeaderArchive { get; set; }

    }
}

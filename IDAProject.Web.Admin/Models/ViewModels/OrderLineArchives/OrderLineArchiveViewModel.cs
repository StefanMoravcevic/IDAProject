using IDAProject.Web.Models.Dto.OrderLineArchives;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.OrderLineArchives
{
    public class OrderLineArchiveViewModel : NavigationBaseViewModel
    {
        public OrderLineArchiveViewModel()
        {
            OrderLineArchive = new OrderLineArchiveDto();
        }
        public OrderLineArchiveDto OrderLineArchive { get; set; }

    }
}

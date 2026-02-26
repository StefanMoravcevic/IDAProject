using IDAProject.Web.Models.Dto.OrderHeaders;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.OrderHeaders
{
    public class OrderHeaderViewModel : NavigationBaseViewModel
    {
        public OrderHeaderViewModel()
        {
            OrderHeader = new OrderHeaderDto();
        }
        public OrderHeaderDto OrderHeader { get; set; }

    }
}

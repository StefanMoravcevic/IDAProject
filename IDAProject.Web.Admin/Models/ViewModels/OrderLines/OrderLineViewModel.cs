using IDAProject.Web.Models.Dto.OrderLines;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.OrderLines
{
    public class OrderLineViewModel : NavigationBaseViewModel
    {
        public OrderLineViewModel()
        {
            OrderLine = new OrderLineDto();
        }
        public OrderLineDto OrderLine { get; set; }

    }
}

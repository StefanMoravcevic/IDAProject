using IDAProject.Web.Admin.Models.Html.AjaxTable;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.OrderHeaderArchives
{
    public class OrderHeaderArchivesViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public OrderHeaderArchivesViewModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            Columns = new List<ColumnDefinition>()
            {
                new ColumnDefinition("Id", _localizer["Id"]) { HeaderStyle = "width:40px;" },
                new ColumnDefinition("CustomerOrderNumber", _localizer["Customer order number"]),
                new ColumnDefinition("CreatedDateFormatted", _localizer["Created date"]),
                new ColumnDefinition("DeliveryRouteCode", _localizer["Delivery route code"]),
                new ColumnDefinition("Partner code", _localizer["Partner code"])
            };
        }

        //add view model properties here

    }
}

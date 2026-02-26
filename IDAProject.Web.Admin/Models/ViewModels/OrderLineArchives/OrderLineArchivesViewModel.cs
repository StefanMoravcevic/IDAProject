using IDAProject.Web.Admin.Models.Html.AjaxTable;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.OrderLineArchives
{
    public class OrderLineArchivesViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public OrderLineArchivesViewModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            _localizer = localizer;
            OrderHeaders = new List<GenericSelectOption>();
            FebiItems = new List<GenericSelectOption>();
            Segments = new List<GenericSelectOption>();
            PartnerCodes = new List<GenericSelectOption>();
            Columns = new List<ColumnDefinition>()
            {
                new ColumnDefinition("Id", _localizer["Id"]) { HeaderStyle = "width:40px;" },
                new ColumnDefinition("CustomerOrderNumber", _localizer["Customer order number"]),
                new ColumnDefinition("LineNo", _localizer["Line no"]),
                new ColumnDefinition("FebiArticleNo", _localizer["Febi article no"]),
                new ColumnDefinition("FebiArticleName", _localizer["Febi article name"]),
                new ColumnDefinition("FebiArticlePackingUnit", _localizer["Febi article packaging unit"]),
                new ColumnDefinition("RequestedQuantity", _localizer["Requested quantity"]),
                new ColumnDefinition("CheckedQuantity", _localizer["Checked quantity"]),
                new ColumnDefinition("PartnerCode", _localizer["Partner code"]),
                new ColumnDefinition("OrderDateFormatted", _localizer["Order date"]),
                new ColumnDefinition("DayOfWeek", _localizer["Day"]),
                new ColumnDefinition("Segment", _localizer["Segment"])
            };
        }

        public int? OrderHeaderArchiveId { get; set; }
        public IEnumerable<ISelectOption> OrderHeaders { get; set; }
        public IEnumerable<ISelectOption> FebiItems { get; set; }
        public IEnumerable<ISelectOption> Segments { get; set; }
        public IEnumerable<ISelectOption> PartnerCodes { get; set; }

    }
}

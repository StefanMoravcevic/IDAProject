using IDAProject.Web.Admin.Models.Html.AjaxTable;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.FebiItems
{
    public class FebiItemsViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public FebiItemsViewModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            Columns = new List<ColumnDefinition>()
            {
                new ColumnDefinition("Id") { HeaderStyle = "width:40px;" },
                new ColumnDefinition("FebiArticleNo", _localizer["FebiArticleNo"]),
                new ColumnDefinition("FebiArticleName", _localizer["FebiArticleName"]),
                new ColumnDefinition("FebiPackingUnit", _localizer["FebiPackingUnit"]),
                new ColumnDefinition("WintArticleNo", _localizer["Wint article no"]),
                new ColumnDefinition("BarCode", _localizer["BarCode"])
            };
        }
    }
}

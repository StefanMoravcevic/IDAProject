using IDAProject.Web.Admin.Models.Html.AjaxTable;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.Roles
{

    public class RolesViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public RolesViewModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id") { HeaderStyle = "width:40px;" },
                new ColumnDefinition("Name", _localizer["Role name"]),
                new ColumnDefinition("Company", _localizer["Company"])
            };
        }
    }
}

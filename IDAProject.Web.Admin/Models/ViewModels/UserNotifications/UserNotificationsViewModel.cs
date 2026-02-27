using IDAProject.Web.Admin.Models.Html.AjaxTable;
using Microsoft.Extensions.Localization;

namespace IDAProject.Web.Admin.Models.ViewModels.UserNotifications
{
    public class UserNotificationsViewModel : NavigationWithAjaxTableViewModel
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public UserNotificationsViewModel(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id", _localizer["Id"]) { HeaderStyle = "width:40px;" },
                new ColumnDefinition("DateFromFormatted", _localizer["Date from"]),
                new ColumnDefinition("DateToFormatted", _localizer["Date to"]),
                new ColumnDefinition("ForAllUsers",_localizer["For all users"]),
                new ColumnDefinition("Sector",_localizer["Group"]),
                new ColumnDefinition("Note", _localizer["Note"])
            };
        }
    }
}

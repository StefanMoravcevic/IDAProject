using Microsoft.Extensions.Localization;
using IDAProject.Web.Admin.Models.Html.AjaxTable;
using IDAProject.Web.Admin.Models.Interfaces.ViewModel;
using IDAProject.Web.Models.Dto.Common;

namespace IDAProject.Web.Admin.Models.ViewModels
{
    public class NavigationWithAjaxTableViewModel : NavigationBaseViewModel, IAjaxTableViewModel
    {
        public NavigationWithAjaxTableViewModel()
        {
            TableSettings = new UserTableSettings();
            Columns = new List<ColumnDefinition>();
        }

        public UserTableSettings TableSettings { get; set; }
        public List<ColumnDefinition> Columns { get; set; }
    }
}

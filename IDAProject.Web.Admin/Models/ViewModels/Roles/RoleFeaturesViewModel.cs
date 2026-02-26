using IDAProject.Web.Admin.Models.Html.AjaxTable;

namespace IDAProject.Web.Admin.Models.ViewModels.Roles
{
    public class RoleFeaturesViewModel : NavigationWithAjaxTableViewModel
    {
        public RoleFeaturesViewModel()
        {
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id") { HeaderStyle = "width:40px;" },
                new ColumnDefinition("Feature.Name","Feature"),
            };
        }
    }
}

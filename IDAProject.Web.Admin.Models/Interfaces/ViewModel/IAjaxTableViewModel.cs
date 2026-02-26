using IDAProject.Web.Admin.Models.Html.AjaxTable;
using IDAProject.Web.Models.Dto.Common;

namespace IDAProject.Web.Admin.Models.Interfaces.ViewModel
{
	public interface IAjaxTableViewModel
	{
        UserTableSettings TableSettings { get; set; }
        List<ColumnDefinition> Columns { get; set; }
    }
}

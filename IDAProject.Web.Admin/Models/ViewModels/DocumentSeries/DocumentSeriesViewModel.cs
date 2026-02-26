using IDAProject.Web.Admin.Models.Html.AjaxTable;
using IDAProject.Web.Models.Dto.Common;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.DocumentSeries
{
    public class DocumentSeriesViewModel : NavigationWithAjaxTableViewModel
    {
        public DocumentSeriesViewModel()
        {
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Id","Id") { HeaderStyle = "width:60px;" },
                new ColumnDefinition("DocumentSerieType", "Document type"),
                new ColumnDefinition("Year", "Year"),
                new ColumnDefinition("NextNumber", "Next number"){ HeaderStyle = "width:120px;" },
                new ColumnDefinition("IncrementSeed", "Increment seed"){ HeaderStyle = "width:120px;" },
                new ColumnDefinition("Pattern"){ HeaderStyle = "width:200px;" },
            };
            DocumentSerieTypes = new List<ISelectOption>();
        }
        public IEnumerable<ISelectOption> DocumentSerieTypes { get; set; }
    }
}

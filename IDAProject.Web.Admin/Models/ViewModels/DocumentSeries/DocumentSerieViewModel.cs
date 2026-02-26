using IDAProject.Web.Admin.Models.Html.AjaxTable;
using IDAProject.Web.Models.Dto.DocumentSeries;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.DocumentSeries
{
    public class DocumentSerieViewModel : NavigationBaseViewModel
    {
        public DocumentSerieViewModel()
        {
            DocumentSerie = new DocumentSerieDto();
            DocumentSerieTypes = new List<ISelectOption>();
        }

        public DocumentSerieDto DocumentSerie { get; set; }
        public int ReadOnly { get; set; }
        public IEnumerable<ISelectOption> DocumentSerieTypes { get; set; }

    }
}

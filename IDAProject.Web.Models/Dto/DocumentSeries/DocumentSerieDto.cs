using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.DocumentSeries
{
    public class DocumentSerieDto : SaveDocumentSerieRequestModel
    {
        public DocumentSerieDto()
        {
            DocumentSerieType = String.Empty;
        }
        public string DocumentSerieType { get; set; }
    }
}

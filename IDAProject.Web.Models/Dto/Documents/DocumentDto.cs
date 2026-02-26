using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.Documents
{
    public class DocumentDto
    {
        public DocumentDto()
        {
            RelativeFilePath = string.Empty;
            DownloadFileName = string.Empty;
            DocumentType = string.Empty;
            UploadedBy = string.Empty;
            SourceId = 1;
        }

        public int Id { get; set; }
        public string RelativeFilePath { get; set; }
        public int DocumentTypeId { get; set; }
        public int ReferenceId { get; set; }
        public DateTime UploadedDate { get; set; }
        public string DownloadFileName { get; set; }
        public int UploadedByUserId { get; set; }
        public int SourceId { get; set; }
        public string DocumentType { get; set; }
        public string UploadedBy { get; set; }

        public string UploadedDateFormatted
        {
            get
            {
                return DisplayFormatHelpers.FormatDateTime(UploadedDate);
            }
        }
    }
}
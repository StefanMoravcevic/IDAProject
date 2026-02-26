namespace IDAProject.Web.Models.Dto.Documents
{
    public class DocumentDownloadData
    {
        public DocumentDownloadData()
        {
            FullPath = string.Empty;
            DownloadFileName = string.Empty;
            MimeType = string.Empty;
            RelativeFilePath = string.Empty;
        }

        public string FullPath { get; set; }
        public string DownloadFileName { get; set; }
        public string MimeType { get; set; }
        public string RelativeFilePath { get; set; }
    }
}
namespace IDAProject.Web.Api.Models.Reports
{
    public class ReportImage
    {
        public ReportImage()
        {
            ImageData = string.Empty;
        }

        public ReportImage(string base64ImageData)
        {
            ImageData = base64ImageData;
        }

        public string ImageData { get; set; }
    }
}
namespace IDAProject.Web.Api.Models.Common
{
    public class FileRepositorySettings
    {
        public FileRepositorySettings()
        {
            StorageType = string.Empty;
            RootPath = string.Empty;
        }

        public string StorageType { get; set; }
        public string RootPath { get; set; }
    }
}

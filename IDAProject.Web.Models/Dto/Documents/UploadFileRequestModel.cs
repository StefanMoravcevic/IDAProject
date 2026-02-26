namespace IDAProject.Web.Models.Dto.Documents
{
    public class UploadFileRequestModel
    {
        public UploadFileRequestModel()
        {
            FileName = string.Empty;
            UserId = 0;
            RelativeFilePath = string.Empty;
            SourceId = 1;
        }

        public int DocumentTypeId { get; set; }
        public int ReferenceId { get; set; }
        public string FileName { get; set; }
        public int UserId { get; set; }
        public string RelativeFilePath { get; set; }

        /// <summary>
        /// 1 - AdminUI
        /// 2 - Android
        /// 3 - iOS
        /// </summary>
        public int SourceId { get; set; }
    }
}

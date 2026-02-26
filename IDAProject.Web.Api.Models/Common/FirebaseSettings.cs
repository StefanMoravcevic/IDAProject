namespace IDAProject.Web.Api.Models.Common
{
    public class FirebaseSettings
    {
        public FirebaseSettings()
        {
            FcmServerKey = string.Empty;
            ProjectId = string.Empty;
        }

        public string FcmServerKey { get; set; }

        public string ProjectId { get; set; }
    }
}

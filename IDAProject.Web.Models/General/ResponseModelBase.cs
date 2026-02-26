namespace IDAProject.Web.Models.General
{
    public class ResponseModelBase
    {
        public ResponseModelBase()
        {
            Valid = false;
            Message = string.Empty;
        }

        public bool Valid { get; set; }

        public string Message { get; set; }

    }
}
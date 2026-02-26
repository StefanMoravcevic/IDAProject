namespace IDAProject.Web.Models.Dto.Messages
{
    public class SaveUserMessageRequestModel
    {
        public SaveUserMessageRequestModel()
        {
            Message = string.Empty;
        }
                
        public int UserFrom { get; set; }
        public int UserTo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Message { get; set; }

        public string CreatedDateFormatted
        {
            get
            {
                var currentDate = DateTime.Today;
                var result = string.Empty;

                if (currentDate.Date == CreatedDate.Date)
                {
                    result = CreatedDate.ToString("HH:mm:ss");
                }
                else
                {
                    result = CreatedDate.ToString("MM/dd/yyyy HH:mm:ss");
                }
                return result;
            }
        }
    }
}

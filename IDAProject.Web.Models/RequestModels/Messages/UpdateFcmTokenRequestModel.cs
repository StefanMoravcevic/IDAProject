namespace IDAProject.Web.Models.RequestModels.Messages
{
    public class UpdateFcmTokenRequestModel
    {
        private string _token;

        public UpdateFcmTokenRequestModel()
        {
            _token = string.Empty;
        }

        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
    }
}

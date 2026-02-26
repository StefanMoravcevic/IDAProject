namespace IDAProject.Web.Api.Models.Common
{
    public class EmailQueueSettings
    {
        public EmailQueueSettings()
        {
            SinglePassSize = 10;
            From = string.Empty;
            DisplayName = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            Host = string.Empty;
            Port = 0;
            EnableSsl = false;
        }


        public int SinglePassSize { get; set; }
        public string From { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
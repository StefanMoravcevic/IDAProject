using IDAProject.Web.Admin.Models.TagHelpers.Base;

namespace IDAProject.Web.Admin.Models.TagHelpers
{
    public class NotificationViewModel : BaseTagHelperViewModel
    {
        private NotificationType _type;

        private string _message;

        public NotificationViewModel()
        {
            _type = NotificationType.Success;
            _message = string.Empty;
        }

        public NotificationType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}

using IDAProject.Web.Admin.Models.TagHelpers;

namespace IDAProject.Web.Admin.Models.ViewModels
{
    public class BaseViewModel
    {
        private NotificationViewModel? _notification;

        public BaseViewModel()
        {
            _notification = null;
        }

        public NotificationViewModel Notification
        {
            get { return _notification!; }
            set { _notification = value; }
        }
    }
}

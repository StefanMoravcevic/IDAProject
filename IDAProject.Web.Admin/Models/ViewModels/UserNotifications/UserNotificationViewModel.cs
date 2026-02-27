using IDAProject.Web.Models.Dto.UserNotifications;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.Interfaces.Html;

namespace IDAProject.Web.Admin.Models.ViewModels.UserNotifications
{
    public class UserNotificationViewModel : NavigationBaseViewModel
    {
        public UserNotificationViewModel()
        {
            UserNotification = new UserNotificationDto();
            Sectors = new List<GenericSelectOption>();
        }
        public UserNotificationDto UserNotification { get; set; }
        public IEnumerable<ISelectOption> Sectors { get; set; }
        public int ReadOnly { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.UserNotifications
{
    public class UserNotificationDto : SaveUserNotificationRequestModel
    {
        public UserNotificationDto()
        {
        }
        #region Basic data

        public string? Sector { get; set; }

        public string? DateFromFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(DateFrom); }
        }
        public string? DateToFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(DateTo); }
        }

        #endregion
    }
}

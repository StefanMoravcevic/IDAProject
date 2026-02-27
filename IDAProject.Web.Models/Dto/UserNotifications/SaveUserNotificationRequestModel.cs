using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.UserNotifications
{
    public class SaveUserNotificationRequestModel
    {
        public Int32 Id { get; set; }
        public bool IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public Int32? SectorId { get; set; }
        public Boolean ForAllUsers { get; set; }
        public String? Note { get; set; }

    }
}

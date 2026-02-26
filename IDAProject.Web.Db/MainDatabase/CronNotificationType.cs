using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class CronNotificationType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual ICollection<CronNotification> CronNotifications { get; set; } = new List<CronNotification>();
}

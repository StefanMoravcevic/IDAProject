using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class CronNotification
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public int NotificationTypeId { get; set; }

    public int ReferenceRecordId { get; set; }

    public int RetryCount { get; set; }

    public DateTime? FinishedDate { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual CronNotificationType NotificationType { get; set; } = null!;
}

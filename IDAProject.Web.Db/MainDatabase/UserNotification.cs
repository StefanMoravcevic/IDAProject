using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class UserNotification
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public int? SectorId { get; set; }

    public bool ForAllUsers { get; set; }

    public string? Note { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual Sector? Sector { get; set; }
}

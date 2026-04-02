using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class EmployeeViewTracking
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? ViewerEmployeeId { get; set; }

    public int? ViewedEmployeeId { get; set; }

    public DateTime? ViewedFrom { get; set; }

    public DateTime? ViewedUntil { get; set; }

    public bool IsBookmarked { get; set; }

    public bool HideFromHomePage { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual Employee? ViewedEmployee { get; set; }

    public virtual Employee? ViewerEmployee { get; set; }
}

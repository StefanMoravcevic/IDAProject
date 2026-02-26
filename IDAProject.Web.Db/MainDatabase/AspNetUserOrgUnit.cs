using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class AspNetUserOrgUnit
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? AspNetUserId { get; set; }

    public int? OrgUnitId { get; set; }

    public virtual AspNetUser? AspNetUser { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual OrgUnit? OrgUnit { get; set; }
}

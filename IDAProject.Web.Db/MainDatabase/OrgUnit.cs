using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class OrgUnit
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int CompanyId { get; set; }

    public int? ParentOrgUnitId { get; set; }

    public string? PhoneNumber { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual ICollection<AspNetUserOrgUnit> AspNetUserOrgUnits { get; set; } = new List<AspNetUserOrgUnit>();

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<OrgUnit> InverseParentOrgUnit { get; set; } = new List<OrgUnit>();

    public virtual OrgUnit? ParentOrgUnit { get; set; }
}

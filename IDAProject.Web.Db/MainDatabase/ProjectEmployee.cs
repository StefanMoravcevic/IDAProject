using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class ProjectEmployee
{
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    public int? EmployeeId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Project? Project { get; set; }
}

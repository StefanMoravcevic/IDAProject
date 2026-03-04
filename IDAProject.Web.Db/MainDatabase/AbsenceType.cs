using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class AbsenceType
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<EmployeeAbsence> EmployeeAbsences { get; set; } = new List<EmployeeAbsence>();
}

using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class EmployeeAbsence
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? EmployeeId { get; set; }

    public int? AbsenceTypeId { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public bool AllDay { get; set; }

    public string? Comment { get; set; }

    public TimeOnly? TimeFrom { get; set; }

    public TimeOnly? TimeTo { get; set; }

    public virtual AbsenceType? AbsenceType { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual Employee? Employee { get; set; }
}

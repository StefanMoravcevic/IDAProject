using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class EmployeeGoal
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? EmployeeId { get; set; }

    public string? Goal { get; set; }

    public int? YearId { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Year? Year { get; set; }
}

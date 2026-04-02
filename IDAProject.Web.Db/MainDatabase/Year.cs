using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class Year
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? Year1 { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<EmployeeGoal> EmployeeGoals { get; set; } = new List<EmployeeGoal>();
}

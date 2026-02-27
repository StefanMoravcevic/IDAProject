using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class RegularActivity
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual ICollection<TasksPlanning> TasksPlannings { get; set; } = new List<TasksPlanning>();
}

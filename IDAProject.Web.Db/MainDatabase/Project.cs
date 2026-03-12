using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class Project
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual ICollection<IdaTask> IdaTasks { get; set; } = new List<IdaTask>();

    public virtual ICollection<TasksPlanning> TasksPlannings { get; set; } = new List<TasksPlanning>();

    public virtual ICollection<TasksRealization> TasksRealizations { get; set; } = new List<TasksRealization>();
}

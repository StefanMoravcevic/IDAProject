using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class TasksPlanning
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? UserId { get; set; }

    public int? ProjectId { get; set; }

    public int? TaskId { get; set; }

    public int? RegularActivityId { get; set; }

    public int? ActivityTypeId { get; set; }

    public string? ActivityName { get; set; }

    public TimeOnly? TimeFrom { get; set; }

    public TimeOnly? TimeTo { get; set; }

    public TimeOnly? Duration { get; set; }

    public int? PlanNo { get; set; }

    public int? PlanStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? EmployeeId { get; set; }

    public virtual ActivityType? ActivityType { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual PlanStatus? PlanStatus { get; set; }

    public virtual Project? Project { get; set; }

    public virtual RegularActivity? RegularActivity { get; set; }

    public virtual IdaTask? Task { get; set; }

    public virtual ICollection<TasksPlanningComment> TasksPlanningComments { get; set; } = new List<TasksPlanningComment>();

    public virtual ICollection<TasksRealization> TasksRealizations { get; set; } = new List<TasksRealization>();

    public virtual AspNetUser? User { get; set; }
}

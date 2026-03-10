using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class TasksRealization
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? TasksPlanningId { get; set; }

    public int? ActivityTypeId { get; set; }

    public int? ProjectId { get; set; }

    public int? IdaTaskId { get; set; }

    public int? RegularActivityId { get; set; }

    public string? Activity { get; set; }

    public string? Report { get; set; }

    public TimeOnly? TimeFrom { get; set; }

    public TimeOnly? TimeTo { get; set; }

    public TimeOnly? Duration { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool Finished { get; set; }

    public int? PlanNo { get; set; }

    public int? UserId { get; set; }

    public virtual ActivityType? ActivityType { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual IdaTask? IdaTask { get; set; }

    public virtual Project? Project { get; set; }

    public virtual RegularActivity? RegularActivity { get; set; }

    public virtual TasksPlanning? TasksPlanning { get; set; }

    public virtual AspNetUser? User { get; set; }
}

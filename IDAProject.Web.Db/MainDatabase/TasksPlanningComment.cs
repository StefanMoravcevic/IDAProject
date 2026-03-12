using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class TasksPlanningComment
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? UserId { get; set; }

    public string? Comment { get; set; }

    public int? TaskPlanningId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ParentTaskPlanningCommentId { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual ICollection<TasksPlanningComment> InverseParentTaskPlanningComment { get; set; } = new List<TasksPlanningComment>();

    public virtual TasksPlanningComment? ParentTaskPlanningComment { get; set; }

    public virtual TasksPlanning? TaskPlanning { get; set; }

    public virtual AspNetUser? User { get; set; }
}

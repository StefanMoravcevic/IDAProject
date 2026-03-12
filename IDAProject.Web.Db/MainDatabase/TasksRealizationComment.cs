using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class TasksRealizationComment
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? UserId { get; set; }

    public string? Comment { get; set; }

    public int? TaskRealizationId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ParentTaskRealizationCommentId { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual ICollection<TasksRealizationComment> InverseParentTaskRealizationComment { get; set; } = new List<TasksRealizationComment>();

    public virtual TasksRealizationComment? ParentTaskRealizationComment { get; set; }

    public virtual TasksRealization? TaskRealization { get; set; }

    public virtual AspNetUser? User { get; set; }
}

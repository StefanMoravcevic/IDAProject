using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class JobType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual ICollection<EmployeeJobTypeControl> EmployeeJobTypeControls { get; set; } = new List<EmployeeJobTypeControl>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
}

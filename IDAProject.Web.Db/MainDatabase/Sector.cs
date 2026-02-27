using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class Sector
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
}

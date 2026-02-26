using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class HierarchyLevel
{
    public int Id { get; set; }

    public int LevelValue { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }
}

using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class Shift
{
    public int Id { get; set; }

    public int ShiftNo { get; set; }

    public TimeOnly? TimeFrom { get; set; }

    public TimeOnly? TimeTo { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }
}

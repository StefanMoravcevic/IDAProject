using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class EmailJobSetting
{
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? EmailJobTypeId { get; set; }

    public string? Email1 { get; set; }

    public string? Email2 { get; set; }

    public string? Email3 { get; set; }

    public string? Email4 { get; set; }

    public string? Email5 { get; set; }

    public string? Email6 { get; set; }

    public string? Email7 { get; set; }

    public string? Email8 { get; set; }

    public string? Email9 { get; set; }

    public string? Email10 { get; set; }

    public string? Note { get; set; }

    public bool Enabled { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual EmailJobType? EmailJobType { get; set; }
}

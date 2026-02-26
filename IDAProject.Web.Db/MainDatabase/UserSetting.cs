using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class UserSetting
{
    public int UserId { get; set; }

    public string SettingsKey { get; set; } = null!;

    public string? StringValue { get; set; }

    public int? IntValue { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}

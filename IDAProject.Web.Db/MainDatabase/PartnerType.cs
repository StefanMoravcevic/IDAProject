using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class PartnerType
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public int PartnerCategoryId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual PartnerCategory PartnerCategory { get; set; } = null!;

    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();
}

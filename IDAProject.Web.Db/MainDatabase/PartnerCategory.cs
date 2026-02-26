using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class PartnerCategory
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual ICollection<PartnerType> PartnerTypes { get; set; } = new List<PartnerType>();
}

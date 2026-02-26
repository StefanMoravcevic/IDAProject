using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class AspNetRoleFeature
{
    public int Id { get; set; }

    public int AspNetRoleId { get; set; }

    public int AspNetFeatureId { get; set; }

    public virtual AspNetFeature AspNetFeature { get; set; } = null!;

    public virtual AspNetRole AspNetRole { get; set; } = null!;
}

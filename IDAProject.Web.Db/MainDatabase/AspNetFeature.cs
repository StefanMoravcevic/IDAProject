using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class AspNetFeature
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? AspNetFeatureType { get; set; }

    public virtual ICollection<AspNetRoleFeature> AspNetRoleFeatures { get; set; } = new List<AspNetRoleFeature>();
}

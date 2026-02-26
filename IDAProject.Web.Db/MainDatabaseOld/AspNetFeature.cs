using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class AspNetFeature
    {
        public AspNetFeature()
        {
            AspNetRoleFeatures = new HashSet<AspNetRoleFeature>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? AspNetFeatureType { get; set; }

        public virtual ICollection<AspNetRoleFeature> AspNetRoleFeatures { get; set; }
    }
}

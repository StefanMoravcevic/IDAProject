using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleIpassType
    {
        public VehicleIpassType()
        {
            VehiclePasses = new HashSet<VehiclePass>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual ICollection<VehiclePass> VehiclePasses { get; set; }
    }
}

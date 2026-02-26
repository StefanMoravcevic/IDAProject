using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleFmcsaViolation
    {
        public VehicleFmcsaViolation()
        {
            VehicleFmcsainspections = new HashSet<VehicleFmcsainspection>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string GroupDescription { get; set; } = null!;
        public string SubGroupDescription { get; set; } = null!;
        public int SeverityWeight { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<VehicleFmcsainspection> VehicleFmcsainspections { get; set; }
    }
}

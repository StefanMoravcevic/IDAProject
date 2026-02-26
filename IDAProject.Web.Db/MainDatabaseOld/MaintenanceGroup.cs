using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class MaintenanceGroup
    {
        public MaintenanceGroup()
        {
            MaintenancePlans = new HashSet<MaintenancePlan>();
            ServiceItems = new HashSet<ServiceItem>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<MaintenancePlan> MaintenancePlans { get; set; }
        public virtual ICollection<ServiceItem> ServiceItems { get; set; }
    }
}

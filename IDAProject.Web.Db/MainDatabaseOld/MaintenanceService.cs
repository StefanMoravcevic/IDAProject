using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class MaintenanceService
    {
        public MaintenanceService()
        {
            ServiceItems = new HashSet<ServiceItem>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int? ServiceTypeId { get; set; }
        public int? MeasureUnitId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual MeasureUnit? MeasureUnit { get; set; }
        public virtual ServiceType? ServiceType { get; set; }
        public virtual ICollection<ServiceItem> ServiceItems { get; set; }
    }
}

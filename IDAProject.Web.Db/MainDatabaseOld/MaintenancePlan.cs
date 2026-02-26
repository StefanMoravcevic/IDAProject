using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class MaintenancePlan
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int? ServiceTypeId { get; set; }
        public int? MaintenanceGroupId { get; set; }
        public DateTime? NextServiceDate { get; set; }
        public int? NextServiceMileage { get; set; }
        public int? NextServiceWorkingHours { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual MaintenanceGroup? MaintenanceGroup { get; set; }
        public virtual ServiceType? ServiceType { get; set; }
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}

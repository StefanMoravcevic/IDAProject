using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleServiceStatus
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int ServiceStatusId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ServiceStatus ServiceStatus { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleStatusHistory
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int InactivityReasonId { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual InactivityReason InactivityReason { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}

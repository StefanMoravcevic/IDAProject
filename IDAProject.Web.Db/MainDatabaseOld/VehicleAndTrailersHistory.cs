using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleAndTrailersHistory
    {
        public int Id { get; set; }
        public int TruckId { get; set; }
        public int TrailerId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Vehicle Trailer { get; set; } = null!;
        public virtual Vehicle Truck { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleEldDatum
    {
        public int Id { get; set; }
        public int VehicleEldhistoryId { get; set; }
        public DateTime Date { get; set; }
        public decimal? Odometer { get; set; }
        public decimal? WorkingHours { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual VehicleEldHistory VehicleEldhistory { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class TourCalculationType
    {
        public TourCalculationType()
        {
            DriverFees = new HashSet<DriverFee>();
            Tours = new HashSet<Tour>();
            VehicleAdvanceCosts = new HashSet<VehicleAdvanceCost>();
            VehicleOptionalCosts = new HashSet<VehicleOptionalCost>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<DriverFee> DriverFees { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
        public virtual ICollection<VehicleAdvanceCost> VehicleAdvanceCosts { get; set; }
        public virtual ICollection<VehicleOptionalCost> VehicleOptionalCosts { get; set; }
    }
}

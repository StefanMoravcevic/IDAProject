using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleCostPayerType
    {
        public VehicleCostPayerType()
        {
            VehicleAdvanceCosts = new HashSet<VehicleAdvanceCost>();
            VehicleIncomes = new HashSet<VehicleIncome>();
            VehicleOptionalCosts = new HashSet<VehicleOptionalCost>();
            VehiclePasses = new HashSet<VehiclePass>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<VehicleAdvanceCost> VehicleAdvanceCosts { get; set; }
        public virtual ICollection<VehicleIncome> VehicleIncomes { get; set; }
        public virtual ICollection<VehicleOptionalCost> VehicleOptionalCosts { get; set; }
        public virtual ICollection<VehiclePass> VehiclePasses { get; set; }
    }
}

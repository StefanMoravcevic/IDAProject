using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleMaintenanceCost
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int PartnerId { get; set; }
        public int CostId { get; set; }
        public decimal? CostInterest { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual CostIncomeType Cost { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Partner Partner { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}

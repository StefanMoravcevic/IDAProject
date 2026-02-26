using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehiclePass
    {
        public VehiclePass()
        {
            Tolls = new HashSet<Toll>();
        }

        public int Id { get; set; }
        public int TypeId { get; set; }
        public int VehicleId { get; set; }
        public string SerialNumber { get; set; } = null!;
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int PassStatusId { get; set; }
        public int PaidBy { get; set; }
        public int? CompanyId { get; set; }
        public int? PartnerId { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual Company? Company { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual VehicleCostPayerType PaidByNavigation { get; set; } = null!;
        public virtual Partner? Partner { get; set; }
        public virtual PassStatus PassStatus { get; set; } = null!;
        public virtual VehiclePassType Type { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
        public virtual ICollection<Toll> Tolls { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleIftasetting
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string DecalNo { get; set; } = null!;
        public string? LicenseNo { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public bool IsHandledByCompany { get; set; }
        public decimal QuarterlyAmount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}

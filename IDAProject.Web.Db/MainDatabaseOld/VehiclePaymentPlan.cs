using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehiclePaymentPlan
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int SupplierId { get; set; }
        public DateTime? LeasedStartDate { get; set; }
        public DateTime? LeasedEndDate { get; set; }
        public DateTime InstallmentDueDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal Interset { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Partner Supplier { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}

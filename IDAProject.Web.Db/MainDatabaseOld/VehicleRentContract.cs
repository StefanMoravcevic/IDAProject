using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleRentContract
    {
        public int Id { get; set; }
        public int ContractTypeId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int? SubcontractorId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual VehicleRentContractType ContractType { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee Driver { get; set; } = null!;
        public virtual Partner? Subcontractor { get; set; }
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}

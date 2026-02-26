using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleFmcsainspection
    {
        public int Id { get; set; }
        public int InspectionTypeId { get; set; }
        public bool IsViolation { get; set; }
        public bool IsOutOfOrder { get; set; }
        public string? ReportNumber { get; set; }
        public int? ControlLevel { get; set; }
        public int DriverId { get; set; }
        public int VehicleId { get; set; }
        public int? ViolationId { get; set; }
        public string? Note { get; set; }
        public bool IsSigned { get; set; }
        public DateTime? SentDate { get; set; }
        public int? PenaltyTypeId { get; set; }
        public decimal? PenaltyAmount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee Driver { get; set; } = null!;
        public virtual VehicleFmcsainspectionType InspectionType { get; set; } = null!;
        public virtual VehicleFmcsainspectionPenaltyType? PenaltyType { get; set; }
        public virtual Vehicle Vehicle { get; set; } = null!;
        public virtual VehicleFmcsaViolation? Violation { get; set; }
    }
}

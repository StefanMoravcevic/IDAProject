using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class Violation
    {
        public Violation()
        {
            ViolationCalculations = new HashSet<ViolationCalculation>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public int EmployeeId { get; set; }
        public int VehicleId { get; set; }
        public DateTime? Date { get; set; }
        public int? DotstateId { get; set; }
        public string? Location { get; set; }
        public string? Highway { get; set; }
        public int? StateId { get; set; }
        public int? ViolationTypeId { get; set; }
        public int? IncidentTypeId { get; set; }
        public bool? DriversGuilt { get; set; }
        public int? InsectionLevel { get; set; }
        public int? Points { get; set; }
        public decimal? LegalCost { get; set; }
        public decimal? AdditionalFee { get; set; }
        public double? UnindetifiedDrivingDuration { get; set; }
        public double? UnindetifiedDrivingMiles { get; set; }
        public int? UnindetifiedDrivingAssignedId { get; set; }
        public int? ElddisconectedNoteId { get; set; }
        public double? Hosviolations { get; set; }
        public int? HosviolationFixedId { get; set; }
        public int? FormMannerErrorId { get; set; }
        public double? FixedForm { get; set; }
        public int? PtimissingFixedId { get; set; }
        public TimeSpan? PersonalConveyanceDuration { get; set; }
        public bool PreEmployment { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual State? Dotstate { get; set; }
        public virtual EldDisconnectedNote? ElddisconectedNote { get; set; }
        public virtual Employee Employee { get; set; } = null!;
        public virtual FormMannerError? FormMannerError { get; set; }
        public virtual HosviolationsFixed? HosviolationFixed { get; set; }
        public virtual IncidentType? IncidentType { get; set; }
        public virtual PtisMissingFixed? PtimissingFixed { get; set; }
        public virtual State? State { get; set; }
        public virtual Vehicle Vehicle { get; set; } = null!;
        public virtual ViolationType? ViolationType { get; set; }
        public virtual ICollection<ViolationCalculation> ViolationCalculations { get; set; }
    }
}

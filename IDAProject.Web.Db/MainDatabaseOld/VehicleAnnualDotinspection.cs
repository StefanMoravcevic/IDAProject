using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleAnnualDotinspection
    {
        public VehicleAnnualDotinspection()
        {
            ServiceDocuments = new HashSet<ServiceDocument>();
        }

        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime InspectionDate { get; set; }
        public string? Note { get; set; }
        public DateTime NextInspectionDate { get; set; }
        public DateTime? NextPreventiveControlDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Vehicle Vehicle { get; set; } = null!;
        public virtual ICollection<ServiceDocument> ServiceDocuments { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class InactivityReason
    {
        public InactivityReason()
        {
            EmployeeAbsences = new HashSet<EmployeeAbsence>();
            VehicleStatusHistories = new HashSet<VehicleStatusHistory>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool DriverVehicle { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<EmployeeAbsence> EmployeeAbsences { get; set; }
        public virtual ICollection<VehicleStatusHistory> VehicleStatusHistories { get; set; }
    }
}

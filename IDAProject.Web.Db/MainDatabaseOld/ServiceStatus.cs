using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class ServiceStatus
    {
        public ServiceStatus()
        {
            ServiceDocuments = new HashSet<ServiceDocument>();
            VehicleServiceStatuses = new HashSet<VehicleServiceStatus>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<ServiceDocument> ServiceDocuments { get; set; }
        public virtual ICollection<VehicleServiceStatus> VehicleServiceStatuses { get; set; }
    }
}

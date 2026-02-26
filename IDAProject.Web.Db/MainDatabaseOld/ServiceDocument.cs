using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class ServiceDocument
    {
        public ServiceDocument()
        {
            ServiceItems = new HashSet<ServiceItem>();
        }

        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public int VehicleId { get; set; }
        public int? VehicleMileage { get; set; }
        public int? VehicleWorkingHours { get; set; }
        public DateTime? Date { get; set; }
        public int? SupplierId { get; set; }
        public string? ExternalNumber { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? ServiceStatusId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? DotinspectionId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual VehicleAnnualDotinspection? Dotinspection { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual ServiceStatus? ServiceStatus { get; set; }
        public virtual Partner? Supplier { get; set; }
        public virtual Vehicle Vehicle { get; set; } = null!;
        public virtual ICollection<ServiceItem> ServiceItems { get; set; }
    }
}

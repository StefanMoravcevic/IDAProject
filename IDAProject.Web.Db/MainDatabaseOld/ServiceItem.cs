using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class ServiceItem
    {
        public int Id { get; set; }
        public int ServiceDocumentId { get; set; }
        public bool ServicePart { get; set; }
        public int? SparePartId { get; set; }
        public int? MaintenanceServiceId { get; set; }
        public string? Name { get; set; }
        public int? MeasureUnitId { get; set; }
        public double? Ammount { get; set; }
        public decimal? SinglePrice { get; set; }
        public decimal? Value { get; set; }
        public bool Garantie { get; set; }
        public int? DefectReasonId { get; set; }
        public int? MaintenanceGroupId { get; set; }
        public int? ServiceTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual DefectReason? DefectReason { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual MaintenanceGroup? MaintenanceGroup { get; set; }
        public virtual MaintenanceService? MaintenanceService { get; set; }
        public virtual MeasureUnit? MeasureUnit { get; set; }
        public virtual ServiceDocument ServiceDocument { get; set; } = null!;
        public virtual ServiceType? ServiceType { get; set; }
        public virtual SparePart? SparePart { get; set; }
    }
}

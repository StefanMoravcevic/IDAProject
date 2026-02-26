using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class Equipment
    {
        public Equipment()
        {
            VehicleAndEquipmentHistories = new HashSet<VehicleAndEquipmentHistory>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int EquipmentTypeId { get; set; }
        public string? SerialNumber { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual EquipmentType EquipmentType { get; set; } = null!;
        public virtual ICollection<VehicleAndEquipmentHistory> VehicleAndEquipmentHistories { get; set; }
    }
}

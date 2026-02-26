using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class EquipmentType
    {
        public EquipmentType()
        {
            Equipment = new HashSet<Equipment>();
            VehicleAndEquipmentHistories = new HashSet<VehicleAndEquipmentHistory>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<VehicleAndEquipmentHistory> VehicleAndEquipmentHistories { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleAndEquipmentHistory
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int EquipmentTypeId { get; set; }
        public int? PalletJackId { get; set; }
        public int? GenericEquipmentId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual EquipmentType EquipmentType { get; set; } = null!;
        public virtual Equipment? GenericEquipment { get; set; }
        public virtual PalletJack? PalletJack { get; set; }
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}

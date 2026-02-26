using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            InverseIdParentNavigation = new HashSet<VehicleType>();
            VehicleManufacturers = new HashSet<VehicleManufacturer>();
            VehicleModels = new HashSet<VehicleModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? IdParent { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual VehicleType? IdParentNavigation { get; set; }
        public virtual ICollection<VehicleType> InverseIdParentNavigation { get; set; }
        public virtual ICollection<VehicleManufacturer> VehicleManufacturers { get; set; }
        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}

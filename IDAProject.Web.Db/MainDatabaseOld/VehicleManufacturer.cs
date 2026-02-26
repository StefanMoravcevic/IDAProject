using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleManufacturer
    {
        public VehicleManufacturer()
        {
            VehicleModels = new HashSet<VehicleModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int VehicleTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual VehicleType VehicleType { get; set; } = null!;
        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}

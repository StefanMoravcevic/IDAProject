using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleRentContractType
    {
        public VehicleRentContractType()
        {
            VehicleRentContracts = new HashSet<VehicleRentContract>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<VehicleRentContract> VehicleRentContracts { get; set; }
    }
}

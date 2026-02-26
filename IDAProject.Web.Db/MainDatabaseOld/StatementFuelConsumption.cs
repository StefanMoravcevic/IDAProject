using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class StatementFuelConsumption
    {
        public int Id { get; set; }
        public int StatementId { get; set; }
        public int FuelConsumptionId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual FuelConsumption FuelConsumption { get; set; } = null!;
        public virtual Statement Statement { get; set; } = null!;
    }
}

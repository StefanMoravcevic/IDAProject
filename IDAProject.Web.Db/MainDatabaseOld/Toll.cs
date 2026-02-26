using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class Toll
    {
        public Toll()
        {
            StatementTolls = new HashSet<StatementToll>();
        }

        public int Id { get; set; }
        public int VehiclePassId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Location { get; set; }
        public string? Agency { get; set; }
        public decimal? Amount { get; set; }
        public string? TransactionType { get; set; }
        public int? VehicleId { get; set; }
        public int? EmployeeId { get; set; }
        public bool IncludedInStatement { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public virtual VehiclePass VehiclePass { get; set; } = null!;
        public virtual ICollection<StatementToll> StatementTolls { get; set; }
    }
}

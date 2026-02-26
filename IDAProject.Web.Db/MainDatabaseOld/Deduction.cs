using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class Deduction
    {
        public Deduction()
        {
            DeductionSchedules = new HashSet<DeductionSchedule>();
            StatementDeductions = new HashSet<StatementDeduction>();
        }

        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public int? CostIncomeTypeId { get; set; }
        public decimal? Amount { get; set; }
        public int EmployeeId { get; set; }
        public int? VehicleId { get; set; }
        public bool IsScheduled { get; set; }
        public bool IncludedInStatement { get; set; }
        public bool IsInitialScheduledDeduction { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual CostIncomeType? CostIncomeType { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee Employee { get; set; } = null!;
        public virtual Vehicle? Vehicle { get; set; }
        public virtual ICollection<DeductionSchedule> DeductionSchedules { get; set; }
        public virtual ICollection<StatementDeduction> StatementDeductions { get; set; }
    }
}

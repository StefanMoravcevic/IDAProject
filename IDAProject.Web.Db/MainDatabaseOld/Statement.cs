using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class Statement
    {
        public Statement()
        {
            StatementCredits = new HashSet<StatementCredit>();
            StatementDeductions = new HashSet<StatementDeduction>();
            StatementFuelConsumptions = new HashSet<StatementFuelConsumption>();
            StatementTolls = new HashSet<StatementToll>();
            StatementTours = new HashSet<StatementTour>();
        }

        public int Id { get; set; }
        public string? StatementNumber { get; set; }
        public DateTime? AccountingDate { get; set; }
        public int? AccountingPeriodId { get; set; }
        public int? AccountingUserId { get; set; }
        public int? EmployeeId { get; set; }
        public int? VehicleId { get; set; }
        public int? CompanyId { get; set; }
        public decimal? Amount { get; set; }
        public bool Registered { get; set; }
        public int? StatementStatusId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AccountingPeriod? AccountingPeriod { get; set; }
        public virtual AspNetUser? AccountingUser { get; set; }
        public virtual Company? Company { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual InvoiceStatus? StatementStatus { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public virtual ICollection<StatementCredit> StatementCredits { get; set; }
        public virtual ICollection<StatementDeduction> StatementDeductions { get; set; }
        public virtual ICollection<StatementFuelConsumption> StatementFuelConsumptions { get; set; }
        public virtual ICollection<StatementToll> StatementTolls { get; set; }
        public virtual ICollection<StatementTour> StatementTours { get; set; }
    }
}

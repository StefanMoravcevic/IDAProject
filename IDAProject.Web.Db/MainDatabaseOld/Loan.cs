using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class Loan
    {
        public Loan()
        {
            LoanPayments = new HashSet<LoanPayment>();
        }

        public int Id { get; set; }
        public int? LoanRequestId { get; set; }
        public DateTime? Date { get; set; }
        public int? Instalments { get; set; }
        public decimal? InstalmentAmount { get; set; }
        public DateTime? PaybackDate { get; set; }
        public int? LoanPaybackId { get; set; }
        public decimal? InterestRate { get; set; }
        public int? VehicleId { get; set; }
        public decimal? AmountGranted { get; set; }
        public int? LoanStatusId { get; set; }
        public string? WayOfPay { get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? RealPaymentDate { get; set; }
        public int? DecisionEmployeeId { get; set; }
        public int? StatementId { get; set; }
        public string? Comment { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? StatusChangeDate { get; set; }

        public virtual Employee? DecisionEmployee { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual LoanPayback? LoanPayback { get; set; }
        public virtual LoanRequest? LoanRequest { get; set; }
        public virtual LoanStatus? LoanStatus { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public virtual ICollection<LoanPayment> LoanPayments { get; set; }
    }
}

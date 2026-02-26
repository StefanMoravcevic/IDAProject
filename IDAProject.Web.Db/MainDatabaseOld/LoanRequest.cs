using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class LoanRequest
    {
        public LoanRequest()
        {
            Loans = new HashSet<Loan>();
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? Date { get; set; }
        public int? LoanPurposeId { get; set; }
        public decimal RequestAmount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee Employee { get; set; } = null!;
        public virtual LoanPurpose? LoanPurpose { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}

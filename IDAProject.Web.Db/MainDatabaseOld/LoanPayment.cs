using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class LoanPayment
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? LoanId { get; set; }
        public decimal? Ammount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Loan? Loan { get; set; }
    }
}

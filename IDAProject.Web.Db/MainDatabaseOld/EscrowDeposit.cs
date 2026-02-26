using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class EscrowDeposit
    {
        public EscrowDeposit()
        {
            EscrowDepositRealizations = new HashSet<EscrowDepositRealization>();
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal DepositAmount { get; set; }
        public int? StatementId { get; set; }
        public DateTime? AccountingStartDate { get; set; }
        public bool? DepositKept { get; set; }
        public decimal? AmountKept { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee Employee { get; set; } = null!;
        public virtual ICollection<EscrowDepositRealization> EscrowDepositRealizations { get; set; }
    }
}

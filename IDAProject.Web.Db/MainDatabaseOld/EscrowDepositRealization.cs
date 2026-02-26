using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class EscrowDepositRealization
    {
        public int Id { get; set; }
        public int EscrowDepositId { get; set; }
        public int? StatementId { get; set; }
        public bool? KeptPaid { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual EscrowDeposit EscrowDeposit { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class CardRebate
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal RebateAmount { get; set; }
        public int? AccountingPeriodId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AccountingPeriod? AccountingPeriod { get; set; }
        public virtual Card Card { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
    }
}

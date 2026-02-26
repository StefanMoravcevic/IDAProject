using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class DeductionSchedule
    {
        public int Id { get; set; }
        public int DeductionId { get; set; }
        public DateTime? StartDate { get; set; }
        public int? RepeatPeriodDays { get; set; }
        public int? NumberOfRepeats { get; set; }
        public int NumberOfPayments { get; set; }
        public decimal? InstalmentAmount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual Deduction Deduction { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
    }
}

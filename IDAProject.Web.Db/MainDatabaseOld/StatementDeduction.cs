using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class StatementDeduction
    {
        public int Id { get; set; }
        public int StatementId { get; set; }
        public int DeductionId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual Deduction Deduction { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Statement Statement { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class ViolationCalculation
    {
        public int Id { get; set; }
        public int ViolationId { get; set; }
        public int CostIncomeTypeId { get; set; }
        public decimal? ViolationAmount { get; set; }
        public decimal? AccidentAmount { get; set; }
        public decimal? CleanInspectionAmount { get; set; }
        public DateTime? AccountingDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual CostIncomeType CostIncomeType { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Violation Violation { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class GeneralCost
    {
        public int CostId { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int CompanyId { get; set; }
        public int? OrgUnitId { get; set; }
        public int CostIncomeTypeId { get; set; }
        public int AccountingPeriodId { get; set; }
        public decimal? MonthlyCostAmount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AccountingPeriod AccountingPeriod { get; set; } = null!;
        public virtual Company Company { get; set; } = null!;
        public virtual CostIncomeType CostIncomeType { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual OrgUnit? OrgUnit { get; set; }
    }
}

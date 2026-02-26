using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class EmployeeCard
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CardId { get; set; }
        public DateTime ChargeDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public bool? Rebate { get; set; }
        public decimal? RebateAmount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual Card Card { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee Employee { get; set; } = null!;
    }
}

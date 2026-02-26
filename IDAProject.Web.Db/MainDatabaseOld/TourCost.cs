using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class TourCost
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int SubmittedBy { get; set; }
        public string? Note { get; set; }
        public int CostIncomeTypeId { get; set; }
        public bool Accepted { get; set; }
        public int? ChargedSubject { get; set; }
        public string? ReceipeNumber { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual CostIncomeType CostIncomeType { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual AspNetUser SubmittedByNavigation { get; set; } = null!;
        public virtual Tour Tour { get; set; } = null!;
    }
}

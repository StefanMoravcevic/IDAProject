using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class PaymentCondition
    {
        public PaymentCondition()
        {
            Invoices = new HashSet<Invoice>();
            Partners = new HashSet<Partner>();
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; } = null!;
        public int? DueDays { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Partner> Partners { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}

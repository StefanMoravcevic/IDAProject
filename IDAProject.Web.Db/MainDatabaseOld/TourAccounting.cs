using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class TourAccounting
    {
        public int Id { get; set; }
        public DateTime? AccountingDate { get; set; }
        public int? TourId { get; set; }
        public int? AccountingUserId { get; set; }
        public decimal? Amount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? AccountingUser { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Tour? Tour { get; set; }
    }
}

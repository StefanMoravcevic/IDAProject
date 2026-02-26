using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class CollectiveTour
    {
        public int Id { get; set; }
        public decimal? Mileage { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser CreatedByNavigation { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
    }
}

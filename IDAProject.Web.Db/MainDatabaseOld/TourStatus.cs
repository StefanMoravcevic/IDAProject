using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class TourStatus
    {
        public TourStatus()
        {
            TourPoints = new HashSet<TourPoint>();
            Tours = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsPointStatus { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<TourPoint> TourPoints { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}

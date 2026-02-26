using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class TourClaim
    {
        public int Id { get; set; }
        public int? TourId { get; set; }
        public int? PointId { get; set; }
        public string? Note { get; set; }
        public DateTime RecordDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual TourPoint? Point { get; set; }
        public virtual Tour? Tour { get; set; }
    }
}

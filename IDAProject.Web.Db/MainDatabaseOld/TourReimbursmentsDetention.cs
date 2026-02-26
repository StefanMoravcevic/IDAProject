using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class TourReimbursmentsDetention
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public bool? IsReimbursment { get; set; }
        public DateTime? Date { get; set; }
        public bool Confirmed { get; set; }
        public decimal? Value { get; set; }
        public string? Description { get; set; }
        public int? ReimbursmentDetentionTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ReimbursmentDetentionType? ReimbursmentDetentionType { get; set; }
    }
}

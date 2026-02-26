using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class DispetcherDriverCommunication
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public int DispetcherId { get; set; }
        public int InteractionEventId { get; set; }
        public string? Description { get; set; }
        public bool? DriverAssignment { get; set; }
        public bool? Ended { get; set; }
        public string? Comment { get; set; }
        public int? NoticeTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee Dispetcher { get; set; } = null!;
        public virtual Employee Driver { get; set; } = null!;
        public virtual InteractionEvent InteractionEvent { get; set; } = null!;
        public virtual NoticeType? NoticeType { get; set; }
    }
}

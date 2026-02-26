using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class DispetcherDriver
    {
        public int Id { get; set; }
        public int DispetcherId { get; set; }
        public int DriverId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee Dispetcher { get; set; } = null!;
        public virtual Employee Driver { get; set; } = null!;
    }
}

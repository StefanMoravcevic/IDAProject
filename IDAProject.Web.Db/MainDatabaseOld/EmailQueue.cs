using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class EmailQueue
    {
        public int Id { get; set; }
        public string EmailTo { get; set; } = null!;
        public string? EmailCc { get; set; }
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public bool IsBodyHtml { get; set; }
        public DateTime DateQueued { get; set; }
        public DateTime? DateSent { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
    }
}

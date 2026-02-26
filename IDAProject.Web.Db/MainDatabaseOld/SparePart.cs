using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class SparePart
    {
        public SparePart()
        {
            ServiceItems = new HashSet<ServiceItem>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<ServiceItem> ServiceItems { get; set; }
    }
}

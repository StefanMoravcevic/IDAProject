using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class Currency
    {
        public Currency()
        {
            GeneralSettings = new HashSet<GeneralSetting>();
        }

        public int Id { get; set; }
        public string AlphaId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<GeneralSetting> GeneralSettings { get; set; }
    }
}

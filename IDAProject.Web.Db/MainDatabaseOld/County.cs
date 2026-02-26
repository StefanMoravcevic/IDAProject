using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class County
    {
        public County()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }
        public int StateId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual State State { get; set; } = null!;
        public virtual ICollection<City> Cities { get; set; }
    }
}

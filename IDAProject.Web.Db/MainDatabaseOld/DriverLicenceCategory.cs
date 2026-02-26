using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class DriverLicenceCategory
    {
        public int Id { get; set; }
        public int DriverLicenceId { get; set; }
        public int LicenceCategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual DriverLicence DriverLicence { get; set; } = null!;
        public virtual LicenceCategory LicenceCategory { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class DriverLicence
    {
        public DriverLicence()
        {
            DriverLicenceCategories = new HashSet<DriverLicenceCategory>();
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Number { get; set; } = null!;
        public int IssueStateId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee Employee { get; set; } = null!;
        public virtual State IssueState { get; set; } = null!;
        public virtual ICollection<DriverLicenceCategory> DriverLicenceCategories { get; set; }
    }
}

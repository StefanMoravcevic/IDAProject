using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class EmploymentType
    {
        public EmploymentType()
        {
            WorkingExperienceInners = new HashSet<WorkingExperienceInner>();
            WorkingExperienceOutters = new HashSet<WorkingExperienceOutter>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<WorkingExperienceInner> WorkingExperienceInners { get; set; }
        public virtual ICollection<WorkingExperienceOutter> WorkingExperienceOutters { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class WorkPosition
    {
        public WorkPosition()
        {
            WorkingExperienceInners = new HashSet<WorkingExperienceInner>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; } = null!;
        public int? HierarchyLevelId { get; set; }
        public string? WorkDescription { get; set; }
        public int? OrgUnitId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual HierarchyLevel? HierarchyLevel { get; set; }
        public virtual OrgUnit? OrgUnit { get; set; }
        public virtual ICollection<WorkingExperienceInner> WorkingExperienceInners { get; set; }
    }
}

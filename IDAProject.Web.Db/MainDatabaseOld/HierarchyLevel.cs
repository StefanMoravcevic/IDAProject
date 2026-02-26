using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class HierarchyLevel
    {
        public HierarchyLevel()
        {
            WorkPositions = new HashSet<WorkPosition>();
        }

        public int Id { get; set; }
        public int LevelValue { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<WorkPosition> WorkPositions { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class OrgUnit
    {
        public OrgUnit()
        {
            Employees = new HashSet<Employee>();
            GeneralCosts = new HashSet<GeneralCost>();
            InverseParentOrgUnit = new HashSet<OrgUnit>();
            Vehicles = new HashSet<Vehicle>();
            WorkPositions = new HashSet<WorkPosition>();
            WorkingExperienceInners = new HashSet<WorkingExperienceInner>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int CompanyId { get; set; }
        public int? ParentOrgUnitId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual OrgUnit? ParentOrgUnit { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<GeneralCost> GeneralCosts { get; set; }
        public virtual ICollection<OrgUnit> InverseParentOrgUnit { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<WorkPosition> WorkPositions { get; set; }
        public virtual ICollection<WorkingExperienceInner> WorkingExperienceInners { get; set; }
    }
}

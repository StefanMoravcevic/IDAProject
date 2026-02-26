using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class WorkingExperienceOutter
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int EmploymentTypeId { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Years { get; set; }
        public int? Months { get; set; }
        public int? Days { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee Employee { get; set; } = null!;
        public virtual EmploymentType EmploymentType { get; set; } = null!;
    }
}

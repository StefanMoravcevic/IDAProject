using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class SafetyTest
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int EmployeeId { get; set; }
        public int SafetyTypeId { get; set; }
        public string? Comment { get; set; }
        public bool PreEmployment { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee Employee { get; set; } = null!;
        public virtual SafetyType SafetyType { get; set; } = null!;
    }
}

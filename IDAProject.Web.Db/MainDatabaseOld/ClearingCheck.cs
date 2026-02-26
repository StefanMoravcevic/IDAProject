using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class ClearingCheck
    {
        public int Id { get; set; }
        public DateTime CheckDate { get; set; }
        public int ClearingCheckTypeId { get; set; }
        public int EmployeeId { get; set; }
        public int? ClearingResultId { get; set; }
        public string? Comment { get; set; }
        public bool PreEmployment { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual ClearingCheckType ClearingCheckType { get; set; } = null!;
        public virtual ClearingResult? ClearingResult { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee Employee { get; set; } = null!;
    }
}

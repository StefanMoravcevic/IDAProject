using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class SubcontractorFee
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int SubcontractorCompanyId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal Fee { get; set; }
        public DateTime RecordDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual Company? Company { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Partner SubcontractorCompany { get; set; } = null!;
    }
}

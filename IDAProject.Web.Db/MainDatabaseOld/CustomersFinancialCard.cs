using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class CustomersFinancialCard
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int CustomerPartnerId { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal OpenAmount { get; set; }
        public int CardTypeId { get; set; }
        public int? FactoringCompanyId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual Partner CustomerPartner { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Partner? FactoringCompany { get; set; }
    }
}

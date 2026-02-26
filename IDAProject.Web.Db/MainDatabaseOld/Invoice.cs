using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoicePayments = new HashSet<InvoicePayment>();
        }

        public int Id { get; set; }
        public int TourId { get; set; }
        public string? Number { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? Amount { get; set; }
        public int? CompanyId { get; set; }
        public int? FactoringHouseId { get; set; }
        public int? InvoiceStatusId { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? PaymentConditionId { get; set; }
        public bool Registered { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual Company? Company { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Partner? FactoringHouse { get; set; }
        public virtual InvoiceStatus? InvoiceStatus { get; set; }
        public virtual PaymentCondition? PaymentCondition { get; set; }
        public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual Tour Tour { get; set; } = null!;
        public virtual ICollection<InvoicePayment> InvoicePayments { get; set; }
    }
}

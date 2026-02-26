using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleIncome
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int IncomeTypeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int CalculationTypeId { get; set; }
        public int AccountingPeriodId { get; set; }
        public decimal Amount { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? StatementNumber { get; set; }
        public decimal? AmountCharged { get; set; }
        public string? Note { get; set; }
        public int? PayerTypeId { get; set; }
        public int? PayedByLesseePartnerId { get; set; }
        public int? PayedByDriverId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AccountingPeriod AccountingPeriod { get; set; } = null!;
        public virtual VehicleCostAndIncomeCalculationType CalculationType { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual CostIncomeType IncomeType { get; set; } = null!;
        public virtual Employee? PayedByDriver { get; set; }
        public virtual Partner? PayedByLesseePartner { get; set; }
        public virtual VehicleCostPayerType? PayerType { get; set; }
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}

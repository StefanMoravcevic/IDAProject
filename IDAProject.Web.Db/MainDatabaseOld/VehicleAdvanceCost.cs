using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VehicleAdvanceCost
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int PayerTypeId { get; set; }
        public int CostTypeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int CalculationTypeId { get; set; }
        public decimal? InsuranceValue { get; set; }
        public int AccountingPeriodId { get; set; }
        public decimal Amount { get; set; }
        public string? StatementNumber { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Note { get; set; }
        public int? PayedByPartnerId { get; set; }
        public int? PayedByDriverId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AccountingPeriod AccountingPeriod { get; set; } = null!;
        public virtual TourCalculationType CalculationType { get; set; } = null!;
        public virtual VehicleCostAndIncomeCalculationType CalculationTypeNavigation { get; set; } = null!;
        public virtual CostIncomeType CostType { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee? PayedByDriver { get; set; }
        public virtual Partner? PayedByPartner { get; set; }
        public virtual VehicleCostPayerType PayerType { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}

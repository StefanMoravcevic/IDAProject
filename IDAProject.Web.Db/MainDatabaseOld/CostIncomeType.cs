using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class CostIncomeType
    {
        public CostIncomeType()
        {
            Credits = new HashSet<Credit>();
            Deductions = new HashSet<Deduction>();
            GeneralCosts = new HashSet<GeneralCost>();
            Partners = new HashSet<Partner>();
            TourCosts = new HashSet<TourCost>();
            VehicleAdvanceCosts = new HashSet<VehicleAdvanceCost>();
            VehicleIncomes = new HashSet<VehicleIncome>();
            VehicleMaintenanceCosts = new HashSet<VehicleMaintenanceCost>();
            VehicleOptionalCosts = new HashSet<VehicleOptionalCost>();
            ViolationCalculations = new HashSet<ViolationCalculation>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; } = null!;
        public bool CostIncome { get; set; }
        public int AccountSegmentId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AccountSegment AccountSegment { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
        public virtual ICollection<Deduction> Deductions { get; set; }
        public virtual ICollection<GeneralCost> GeneralCosts { get; set; }
        public virtual ICollection<Partner> Partners { get; set; }
        public virtual ICollection<TourCost> TourCosts { get; set; }
        public virtual ICollection<VehicleAdvanceCost> VehicleAdvanceCosts { get; set; }
        public virtual ICollection<VehicleIncome> VehicleIncomes { get; set; }
        public virtual ICollection<VehicleMaintenanceCost> VehicleMaintenanceCosts { get; set; }
        public virtual ICollection<VehicleOptionalCost> VehicleOptionalCosts { get; set; }
        public virtual ICollection<ViolationCalculation> ViolationCalculations { get; set; }
    }
}

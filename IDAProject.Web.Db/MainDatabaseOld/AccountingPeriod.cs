using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class AccountingPeriod
    {
        public AccountingPeriod()
        {
            CardRebates = new HashSet<CardRebate>();
            GeneralCosts = new HashSet<GeneralCost>();
            Statements = new HashSet<Statement>();
            VehicleAdvanceCosts = new HashSet<VehicleAdvanceCost>();
            VehicleIncomes = new HashSet<VehicleIncome>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime FromDate { get; set; }
        public int Days { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<CardRebate> CardRebates { get; set; }
        public virtual ICollection<GeneralCost> GeneralCosts { get; set; }
        public virtual ICollection<Statement> Statements { get; set; }
        public virtual ICollection<VehicleAdvanceCost> VehicleAdvanceCosts { get; set; }
        public virtual ICollection<VehicleIncome> VehicleIncomes { get; set; }
    }
}

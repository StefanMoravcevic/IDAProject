using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class FuelConsumption
    {
        public FuelConsumption()
        {
            StatementFuelConsumptions = new HashSet<StatementFuelConsumption>();
        }

        public int Id { get; set; }
        public int? CardId { get; set; }
        public DateTime? Date { get; set; }
        public int? ZipCodeId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public string? Description { get; set; }
        public string? Product { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Price { get; set; }
        public decimal? Advance { get; set; }
        public decimal? Misc { get; set; }
        public int? EmployeeId { get; set; }
        public int? VehicleId { get; set; }
        public bool IncludedInStatement { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual Card? Card { get; set; }
        public virtual City? City { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual State? State { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public virtual ZipCode? ZipCode { get; set; }
        public virtual ICollection<StatementFuelConsumption> StatementFuelConsumptions { get; set; }
    }
}

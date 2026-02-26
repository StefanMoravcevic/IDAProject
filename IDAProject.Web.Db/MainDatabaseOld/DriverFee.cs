using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class DriverFee
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CalculationTypeId { get; set; }
        /// <summary>
        /// Amount per mileage or percent of the tour
        /// </summary>
        public decimal Fee { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual TourCalculationType CalculationType { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee Employee { get; set; } = null!;
    }
}

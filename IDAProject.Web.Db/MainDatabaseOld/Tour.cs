using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class Tour
    {
        public Tour()
        {
            EmployeeWaitings = new HashSet<EmployeeWaiting>();
            InverseSplittedFromTour = new HashSet<Tour>();
            Invoices = new HashSet<Invoice>();
            StatementTours = new HashSet<StatementTour>();
            TourAccountings = new HashSet<TourAccounting>();
            TourClaims = new HashSet<TourClaim>();
            TourCosts = new HashSet<TourCost>();
            TourCrews = new HashSet<TourCrew>();
            TourPoints = new HashSet<TourPoint>();
        }

        public int Id { get; set; }
        public int StatusId { get; set; }
        /// <summary>
        /// Ko je ugovorio turu (kompanija iz grupe) 
        /// </summary>
        public int CompanyId { get; set; }
        public int CustomerCompanyId { get; set; }
        /// <summary>
        /// Ko će biti nosilac fakture ka kupcu (kompanija iz grupe)
        /// </summary>
        public int InvoiceCompanyId { get; set; }
        public int? FactoringCompanyId { get; set; }
        /// <summary>
        /// Ko je vozio turu (kompanija iz grupe)
        /// </summary>
        public int? TransportationCompanyId { get; set; }
        public DateTime? ContractDate { get; set; }
        public int? SplittedFromTourId { get; set; }
        public string? TourNumber { get; set; }
        public string? ExternalNo { get; set; }
        public int? SubcontractorCompanyId { get; set; }
        public int? BrokerId { get; set; }
        public int? BrokerContactId { get; set; }
        public int? CustomerContactId { get; set; }
        public int PaymentConditionId { get; set; }
        public int? PalletsCount { get; set; }
        public decimal? CargoWeight { get; set; }
        public decimal? CargoLength { get; set; }
        public decimal? CargoHeight { get; set; }
        public decimal? CargoVolume { get; set; }
        public int CalculationTypeId { get; set; }
        public decimal? EstimatedMileage { get; set; }
        public decimal? BillableMileage { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal ContractAmount { get; set; }
        public decimal FreightRate { get; set; }
        /// <summary>
        ///  The amount of the base for calculating the variable part to your own driver (if it is not per mile traveled, which is charged as a % of the tour value)
        /// </summary>
        public decimal? BaseAmountForTheDriver { get; set; }
        /// <summary>
        /// The amount of the tour finding fee and the Amount remaining after the fee is deducted (in cases where a commission is applied to the value of the tour to obtain the amount for the carrier, i.e. the amount retained for the tour finding). A % of the tenant/subcontractor adjustment applies.
        /// </summary>
        public decimal? CommissionRate { get; set; }
        /// <summary>
        /// Cena koja se prijavljuje subcontractory
        /// </summary>
        public decimal? TripAmountSubcontractor { get; set; }
        public decimal? DrivingHoursEstimated { get; set; }
        public bool IncludedInStatement { get; set; }
        public int? CollectiveTourId { get; set; }
        public int? CollectiveIndex { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual Partner? Broker { get; set; }
        public virtual Contact? BrokerContact { get; set; }
        public virtual TourCalculationType CalculationType { get; set; } = null!;
        public virtual Company Company { get; set; } = null!;
        public virtual Partner CustomerCompany { get; set; } = null!;
        public virtual Contact? CustomerContact { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Partner? FactoringCompany { get; set; }
        public virtual Company InvoiceCompany { get; set; } = null!;
        public virtual PaymentCondition PaymentCondition { get; set; } = null!;
        public virtual Tour? SplittedFromTour { get; set; }
        public virtual TourStatus Status { get; set; } = null!;
        public virtual Partner? SubcontractorCompany { get; set; }
        public virtual Company? TransportationCompany { get; set; }
        public virtual ICollection<EmployeeWaiting> EmployeeWaitings { get; set; }
        public virtual ICollection<Tour> InverseSplittedFromTour { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<StatementTour> StatementTours { get; set; }
        public virtual ICollection<TourAccounting> TourAccountings { get; set; }
        public virtual ICollection<TourClaim> TourClaims { get; set; }
        public virtual ICollection<TourCost> TourCosts { get; set; }
        public virtual ICollection<TourCrew> TourCrews { get; set; }
        public virtual ICollection<TourPoint> TourPoints { get; set; }
    }
}

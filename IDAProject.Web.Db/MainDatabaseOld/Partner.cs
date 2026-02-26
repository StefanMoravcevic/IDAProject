using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class Partner
    {
        public Partner()
        {
            Companies = new HashSet<Company>();
            Contacts = new HashSet<Contact>();
            CustomersFinancialCardCustomerPartners = new HashSet<CustomersFinancialCard>();
            CustomersFinancialCardFactoringCompanies = new HashSet<CustomersFinancialCard>();
            Employees = new HashSet<Employee>();
            FactoringFees = new HashSet<FactoringFee>();
            Invoices = new HashSet<Invoice>();
            ServiceDocuments = new HashSet<ServiceDocument>();
            SubcontractorFees = new HashSet<SubcontractorFee>();
            TourBrokers = new HashSet<Tour>();
            TourCustomerCompanies = new HashSet<Tour>();
            TourFactoringCompanies = new HashSet<Tour>();
            TourSubcontractorCompanies = new HashSet<Tour>();
            VehicleAdvanceCosts = new HashSet<VehicleAdvanceCost>();
            VehicleEldHistories = new HashSet<VehicleEldHistory>();
            VehicleIncomes = new HashSet<VehicleIncome>();
            VehicleMaintenanceCosts = new HashSet<VehicleMaintenanceCost>();
            VehicleOptionalCosts = new HashSet<VehicleOptionalCost>();
            VehiclePasses = new HashSet<VehiclePass>();
            VehiclePaymentPlans = new HashSet<VehiclePaymentPlan>();
            VehicleRentContracts = new HashSet<VehicleRentContract>();
            Vehicles = new HashSet<Vehicle>();
            WorkingExperienceInners = new HashSet<WorkingExperienceInner>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? AccountingCode { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public int? ZipCodeId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public int PartnerTypeId { get; set; }
        public string? Ein { get; set; }
        public string? Mc { get; set; }
        public string? Dot { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? ContactPerson { get; set; }
        public int? PaymentConditionId { get; set; }
        public int? PrimaryContactId { get; set; }
        public int? IncomeTypeId { get; set; }
        public bool Blocked { get; set; }
        public string? BlockedComment { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? RautingNumber { get; set; }
        public string? DisclaimerNote { get; set; }
        public int? ContactCompanyId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual City? City { get; set; }
        public virtual Contact? ContactCompany { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual CostIncomeType? IncomeType { get; set; }
        public virtual PartnerType PartnerType { get; set; } = null!;
        public virtual PaymentCondition? PaymentCondition { get; set; }
        public virtual Contact? PrimaryContact { get; set; }
        public virtual State? State { get; set; }
        public virtual ZipCode? ZipCode { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<CustomersFinancialCard> CustomersFinancialCardCustomerPartners { get; set; }
        public virtual ICollection<CustomersFinancialCard> CustomersFinancialCardFactoringCompanies { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<FactoringFee> FactoringFees { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<ServiceDocument> ServiceDocuments { get; set; }
        public virtual ICollection<SubcontractorFee> SubcontractorFees { get; set; }
        public virtual ICollection<Tour> TourBrokers { get; set; }
        public virtual ICollection<Tour> TourCustomerCompanies { get; set; }
        public virtual ICollection<Tour> TourFactoringCompanies { get; set; }
        public virtual ICollection<Tour> TourSubcontractorCompanies { get; set; }
        public virtual ICollection<VehicleAdvanceCost> VehicleAdvanceCosts { get; set; }
        public virtual ICollection<VehicleEldHistory> VehicleEldHistories { get; set; }
        public virtual ICollection<VehicleIncome> VehicleIncomes { get; set; }
        public virtual ICollection<VehicleMaintenanceCost> VehicleMaintenanceCosts { get; set; }
        public virtual ICollection<VehicleOptionalCost> VehicleOptionalCosts { get; set; }
        public virtual ICollection<VehiclePass> VehiclePasses { get; set; }
        public virtual ICollection<VehiclePaymentPlan> VehiclePaymentPlans { get; set; }
        public virtual ICollection<VehicleRentContract> VehicleRentContracts { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<WorkingExperienceInner> WorkingExperienceInners { get; set; }
    }
}

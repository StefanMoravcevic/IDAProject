using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class Company
    {
        public Company()
        {
            AspNetRoles = new HashSet<AspNetRole>();
            CustomersFinancialCards = new HashSet<CustomersFinancialCard>();
            Employees = new HashSet<Employee>();
            FactoringFees = new HashSet<FactoringFee>();
            GeneralCosts = new HashSet<GeneralCost>();
            InverseIdParentCompanyNavigation = new HashSet<Company>();
            Invoices = new HashSet<Invoice>();
            OrgUnits = new HashSet<OrgUnit>();
            Statements = new HashSet<Statement>();
            SubcontractorFees = new HashSet<SubcontractorFee>();
            TourCompanies = new HashSet<Tour>();
            TourInvoiceCompanies = new HashSet<Tour>();
            TourTransportationCompanies = new HashSet<Tour>();
            VehicleEldHistories = new HashSet<VehicleEldHistory>();
            VehiclePasses = new HashSet<VehiclePass>();
            Vehicles = new HashSet<Vehicle>();
            WorkingExperienceInners = new HashSet<WorkingExperienceInner>();
        }

        public int Id { get; set; }
        public int? IdParentCompany { get; set; }
        public string? Logo { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public int? ZipCodeId { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string? Dot { get; set; }
        public string? Mc { get; set; }
        public string? Ein { get; set; }
        public string? ResponsiblePerson { get; set; }
        public string? WebAddress { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public int? FactoringHouseId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Partner? FactoringHouse { get; set; }
        public virtual Company? IdParentCompanyNavigation { get; set; }
        public virtual State State { get; set; } = null!;
        public virtual ZipCode? ZipCode { get; set; }
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
        public virtual ICollection<CustomersFinancialCard> CustomersFinancialCards { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<FactoringFee> FactoringFees { get; set; }
        public virtual ICollection<GeneralCost> GeneralCosts { get; set; }
        public virtual ICollection<Company> InverseIdParentCompanyNavigation { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<OrgUnit> OrgUnits { get; set; }
        public virtual ICollection<Statement> Statements { get; set; }
        public virtual ICollection<SubcontractorFee> SubcontractorFees { get; set; }
        public virtual ICollection<Tour> TourCompanies { get; set; }
        public virtual ICollection<Tour> TourInvoiceCompanies { get; set; }
        public virtual ICollection<Tour> TourTransportationCompanies { get; set; }
        public virtual ICollection<VehicleEldHistory> VehicleEldHistories { get; set; }
        public virtual ICollection<VehiclePass> VehiclePasses { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<WorkingExperienceInner> WorkingExperienceInners { get; set; }
    }
}

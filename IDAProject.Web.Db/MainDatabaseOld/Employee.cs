using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class Employee
    {
        public Employee()
        {
            AspNetUsers = new HashSet<AspNetUser>();
            ClearingChecks = new HashSet<ClearingCheck>();
            Credits = new HashSet<Credit>();
            Deductions = new HashSet<Deduction>();
            DispetcherDriverCommunicationDispetchers = new HashSet<DispetcherDriverCommunication>();
            DispetcherDriverCommunicationDrivers = new HashSet<DispetcherDriverCommunication>();
            DispetcherDriverDispetchers = new HashSet<DispetcherDriver>();
            DispetcherDriverDrivers = new HashSet<DispetcherDriver>();
            DriverFees = new HashSet<DriverFee>();
            DriverLicences = new HashSet<DriverLicence>();
            DriverStopBonuses = new HashSet<DriverStopBonuse>();
            EmployeeAbsences = new HashSet<EmployeeAbsence>();
            EmployeeCards = new HashSet<EmployeeCard>();
            EmployeeCertificates = new HashSet<EmployeeCertificate>();
            EmployeeWaitings = new HashSet<EmployeeWaiting>();
            EscrowDeposits = new HashSet<EscrowDeposit>();
            FamilyMembers = new HashSet<FamilyMember>();
            FuelConsumptions = new HashSet<FuelConsumption>();
            LoanRequests = new HashSet<LoanRequest>();
            Loans = new HashSet<Loan>();
            SafetyTests = new HashSet<SafetyTest>();
            Statements = new HashSet<Statement>();
            Tolls = new HashSet<Toll>();
            TourCrewCoDrivers = new HashSet<TourCrew>();
            TourCrewDispatchers = new HashSet<TourCrew>();
            TourCrewDrivers = new HashSet<TourCrew>();
            VehicleAdvanceCosts = new HashSet<VehicleAdvanceCost>();
            VehicleAssignments = new HashSet<VehicleAssignment>();
            VehicleFmcsainspections = new HashSet<VehicleFmcsainspection>();
            VehicleIncomes = new HashSet<VehicleIncome>();
            VehicleOptionalCosts = new HashSet<VehicleOptionalCost>();
            VehicleRentContracts = new HashSet<VehicleRentContract>();
            Violations = new HashSet<Violation>();
            WaitingBonuses = new HashSet<WaitingBonuse>();
            WorkingExperienceInners = new HashSet<WorkingExperienceInner>();
            WorkingExperienceOutters = new HashSet<WorkingExperienceOutter>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string? Address { get; set; }
        public int? ZipCodeId { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int JobTypeId { get; set; }
        public int CompanyId { get; set; }
        public int? OrgUnitId { get; set; }
        public int? PartnerId { get; set; }
        public bool OwnPartnerCompany { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthPlace { get; set; }
        public string? Citizenship { get; set; }
        public string? PersonalId { get; set; }
        public string? PassportId { get; set; }
        public string? InsuranceNumber { get; set; }
        public string? FederalNumber { get; set; }
        public string? BankAccount { get; set; }
        public string? BankAccountAddition { get; set; }
        public string? RoutingNumber { get; set; }
        public string? HousePhoneNumber { get; set; }
        public string? CellPhoneNumber { get; set; }
        public string Email { get; set; } = null!;
        public int? NoticeTypeId { get; set; }
        public string? ShoeSize { get; set; }
        public string? SuiteSize { get; set; }
        public bool Blocked { get; set; }
        public string? AccountingCode { get; set; }
        public int? GenderId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual City? City { get; set; }
        public virtual Company Company { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Gender? Gender { get; set; }
        public virtual JobType JobType { get; set; } = null!;
        public virtual NoticeType? NoticeType { get; set; }
        public virtual OrgUnit? OrgUnit { get; set; }
        public virtual Partner? Partner { get; set; }
        public virtual State? State { get; set; }
        public virtual ZipCode? ZipCode { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual ICollection<ClearingCheck> ClearingChecks { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
        public virtual ICollection<Deduction> Deductions { get; set; }
        public virtual ICollection<DispetcherDriverCommunication> DispetcherDriverCommunicationDispetchers { get; set; }
        public virtual ICollection<DispetcherDriverCommunication> DispetcherDriverCommunicationDrivers { get; set; }
        public virtual ICollection<DispetcherDriver> DispetcherDriverDispetchers { get; set; }
        public virtual ICollection<DispetcherDriver> DispetcherDriverDrivers { get; set; }
        public virtual ICollection<DriverFee> DriverFees { get; set; }
        public virtual ICollection<DriverLicence> DriverLicences { get; set; }
        public virtual ICollection<DriverStopBonuse> DriverStopBonuses { get; set; }
        public virtual ICollection<EmployeeAbsence> EmployeeAbsences { get; set; }
        public virtual ICollection<EmployeeCard> EmployeeCards { get; set; }
        public virtual ICollection<EmployeeCertificate> EmployeeCertificates { get; set; }
        public virtual ICollection<EmployeeWaiting> EmployeeWaitings { get; set; }
        public virtual ICollection<EscrowDeposit> EscrowDeposits { get; set; }
        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
        public virtual ICollection<FuelConsumption> FuelConsumptions { get; set; }
        public virtual ICollection<LoanRequest> LoanRequests { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
        public virtual ICollection<SafetyTest> SafetyTests { get; set; }
        public virtual ICollection<Statement> Statements { get; set; }
        public virtual ICollection<Toll> Tolls { get; set; }
        public virtual ICollection<TourCrew> TourCrewCoDrivers { get; set; }
        public virtual ICollection<TourCrew> TourCrewDispatchers { get; set; }
        public virtual ICollection<TourCrew> TourCrewDrivers { get; set; }
        public virtual ICollection<VehicleAdvanceCost> VehicleAdvanceCosts { get; set; }
        public virtual ICollection<VehicleAssignment> VehicleAssignments { get; set; }
        public virtual ICollection<VehicleFmcsainspection> VehicleFmcsainspections { get; set; }
        public virtual ICollection<VehicleIncome> VehicleIncomes { get; set; }
        public virtual ICollection<VehicleOptionalCost> VehicleOptionalCosts { get; set; }
        public virtual ICollection<VehicleRentContract> VehicleRentContracts { get; set; }
        public virtual ICollection<Violation> Violations { get; set; }
        public virtual ICollection<WaitingBonuse> WaitingBonuses { get; set; }
        public virtual ICollection<WorkingExperienceInner> WorkingExperienceInners { get; set; }
        public virtual ICollection<WorkingExperienceOutter> WorkingExperienceOutters { get; set; }
    }
}

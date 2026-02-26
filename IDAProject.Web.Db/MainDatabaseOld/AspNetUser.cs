using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            AccountSegments = new HashSet<AccountSegment>();
            AccountingPeriodTypes = new HashSet<AccountingPeriodType>();
            AccountingPeriods = new HashSet<AccountingPeriod>();
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserRoles = new HashSet<AspNetUserRole>();
            AspNetUserTokens = new HashSet<AspNetUserToken>();
            CardRebates = new HashSet<CardRebate>();
            CardTypes = new HashSet<CardType>();
            Cards = new HashSet<Card>();
            CertificateTypes = new HashSet<CertificateType>();
            Cities = new HashSet<City>();
            ClearingCheckTypes = new HashSet<ClearingCheckType>();
            ClearingChecks = new HashSet<ClearingCheck>();
            ClearingResults = new HashSet<ClearingResult>();
            CollectiveTourCreatedByNavigations = new HashSet<CollectiveTour>();
            CollectiveTourDeletedByNavigations = new HashSet<CollectiveTour>();
            Companies = new HashSet<Company>();
            Contacts = new HashSet<Contact>();
            CostIncomeTypes = new HashSet<CostIncomeType>();
            Counties = new HashSet<County>();
            Credits = new HashSet<Credit>();
            Currencies = new HashSet<Currency>();
            CustomersFinancialCards = new HashSet<CustomersFinancialCard>();
            DeductionSchedules = new HashSet<DeductionSchedule>();
            Deductions = new HashSet<Deduction>();
            DefectReasons = new HashSet<DefectReason>();
            DispetcherDriverCommunications = new HashSet<DispetcherDriverCommunication>();
            DispetcherDrivers = new HashSet<DispetcherDriver>();
            DocumentDeletedByNavigations = new HashSet<Document>();
            DocumentSerieTypes = new HashSet<DocumentSerieType>();
            DocumentSeries = new HashSet<DocumentSeries>();
            DocumentTypes = new HashSet<DocumentType>();
            DocumentUploadedByUsers = new HashSet<Document>();
            DriverFees = new HashSet<DriverFee>();
            DriverLicenceCategories = new HashSet<DriverLicenceCategory>();
            DriverLicences = new HashSet<DriverLicence>();
            DriverStopBonuses = new HashSet<DriverStopBonuse>();
            EldDisconnectedNotes = new HashSet<EldDisconnectedNote>();
            EmailQueues = new HashSet<EmailQueue>();
            EmployeeAbsences = new HashSet<EmployeeAbsence>();
            EmployeeCards = new HashSet<EmployeeCard>();
            EmployeeCertificates = new HashSet<EmployeeCertificate>();
            EmployeeWaitings = new HashSet<EmployeeWaiting>();
            Employees = new HashSet<Employee>();
            EmploymentTypes = new HashSet<EmploymentType>();
            Equipment = new HashSet<Equipment>();
            EquipmentTypes = new HashSet<EquipmentType>();
            EscrowDepositRealizations = new HashSet<EscrowDepositRealization>();
            EscrowDeposits = new HashSet<EscrowDeposit>();
            FactoringFees = new HashSet<FactoringFee>();
            FamilyMembers = new HashSet<FamilyMember>();
            FormMannerErrors = new HashSet<FormMannerError>();
            FuelConsumptions = new HashSet<FuelConsumption>();
            Genders = new HashSet<Gender>();
            GeneralCosts = new HashSet<GeneralCost>();
            HierarchyLevels = new HashSet<HierarchyLevel>();
            HosviolationsFixeds = new HashSet<HosviolationsFixed>();
            InactivityReasons = new HashSet<InactivityReason>();
            IncidentTypes = new HashSet<IncidentType>();
            Integrations = new HashSet<Integration>();
            InteractionEvents = new HashSet<InteractionEvent>();
            InvoicePayments = new HashSet<InvoicePayment>();
            InvoiceStatuses = new HashSet<InvoiceStatus>();
            Invoices = new HashSet<Invoice>();
            JobTypes = new HashSet<JobType>();
            LicenceCategories = new HashSet<LicenceCategory>();
            LoanPaybacks = new HashSet<LoanPayback>();
            LoanPayments = new HashSet<LoanPayment>();
            LoanPurposes = new HashSet<LoanPurpose>();
            LoanRequests = new HashSet<LoanRequest>();
            LoanStatuses = new HashSet<LoanStatus>();
            Loans = new HashSet<Loan>();
            MaintenanceGroups = new HashSet<MaintenanceGroup>();
            MaintenancePlans = new HashSet<MaintenancePlan>();
            MaintenanceServices = new HashSet<MaintenanceService>();
            MeasureUnitTypes = new HashSet<MeasureUnitType>();
            MeasureUnits = new HashSet<MeasureUnit>();
            NoticeTypes = new HashSet<NoticeType>();
            OrgUnits = new HashSet<OrgUnit>();
            PalletJacks = new HashSet<PalletJack>();
            PartnerCategories = new HashSet<PartnerCategory>();
            PartnerTypes = new HashSet<PartnerType>();
            Partners = new HashSet<Partner>();
            PassStatuses = new HashSet<PassStatus>();
            PaymentConditions = new HashSet<PaymentCondition>();
            PaymentMethods = new HashSet<PaymentMethod>();
            PtisMissingFixeds = new HashSet<PtisMissingFixed>();
            ReimbursmentDetentionTypes = new HashSet<ReimbursmentDetentionType>();
            Relationships = new HashSet<Relationship>();
            SafetyTests = new HashSet<SafetyTest>();
            SafetyTypes = new HashSet<SafetyType>();
            ServiceDocuments = new HashSet<ServiceDocument>();
            ServiceItems = new HashSet<ServiceItem>();
            ServiceStatuses = new HashSet<ServiceStatus>();
            ServiceTypes = new HashSet<ServiceType>();
            SpareParts = new HashSet<SparePart>();
            StatementAccountingUsers = new HashSet<Statement>();
            StatementCredits = new HashSet<StatementCredit>();
            StatementDeductions = new HashSet<StatementDeduction>();
            StatementDeletedByNavigations = new HashSet<Statement>();
            StatementFuelConsumptions = new HashSet<StatementFuelConsumption>();
            StatementTolls = new HashSet<StatementToll>();
            StatementTours = new HashSet<StatementTour>();
            States = new HashSet<State>();
            SubcontractorFees = new HashSet<SubcontractorFee>();
            TimeZones = new HashSet<TimeZone>();
            Tolls = new HashSet<Toll>();
            TourAccountingAccountingUsers = new HashSet<TourAccounting>();
            TourAccountingDeletedByNavigations = new HashSet<TourAccounting>();
            TourCalculationTypes = new HashSet<TourCalculationType>();
            TourClaims = new HashSet<TourClaim>();
            TourCostDeletedByNavigations = new HashSet<TourCost>();
            TourCostSubmittedByNavigations = new HashSet<TourCost>();
            TourCrewTypes = new HashSet<TourCrewType>();
            TourCrews = new HashSet<TourCrew>();
            TourPointSpecialInstructions = new HashSet<TourPointSpecialInstruction>();
            TourPointTypes = new HashSet<TourPointType>();
            TourPoints = new HashSet<TourPoint>();
            TourReimbursmentsDetentions = new HashSet<TourReimbursmentsDetention>();
            TourStatuses = new HashSet<TourStatus>();
            Tours = new HashSet<Tour>();
            UnidentifiedDrivingsAssigneds = new HashSet<UnidentifiedDrivingsAssigned>();
            UserMessageUserFromNavigations = new HashSet<UserMessage>();
            UserMessageUserToNavigations = new HashSet<UserMessage>();
            UserSettingDeletedByNavigations = new HashSet<UserSetting>();
            UserSettingUsers = new HashSet<UserSetting>();
            VehicleAdvanceCosts = new HashSet<VehicleAdvanceCost>();
            VehicleAndEquipmentHistories = new HashSet<VehicleAndEquipmentHistory>();
            VehicleAndTrailersHistories = new HashSet<VehicleAndTrailersHistory>();
            VehicleAnnualDotinspections = new HashSet<VehicleAnnualDotinspection>();
            VehicleAssignments = new HashSet<VehicleAssignment>();
            VehicleCostAndIncomeCalculationTypes = new HashSet<VehicleCostAndIncomeCalculationType>();
            VehicleCostPayerTypes = new HashSet<VehicleCostPayerType>();
            VehicleEldData = new HashSet<VehicleEldDatum>();
            VehicleEldHistories = new HashSet<VehicleEldHistory>();
            VehicleFmcsaViolations = new HashSet<VehicleFmcsaViolation>();
            VehicleFmcsainspectionPenaltyTypes = new HashSet<VehicleFmcsainspectionPenaltyType>();
            VehicleFmcsainspectionTypes = new HashSet<VehicleFmcsainspectionType>();
            VehicleFmcsainspections = new HashSet<VehicleFmcsainspection>();
            VehicleIftasettings = new HashSet<VehicleIftasetting>();
            VehicleIncomes = new HashSet<VehicleIncome>();
            VehicleMaintenanceCosts = new HashSet<VehicleMaintenanceCost>();
            VehicleManufacturers = new HashSet<VehicleManufacturer>();
            VehicleModels = new HashSet<VehicleModel>();
            VehicleOptionalCosts = new HashSet<VehicleOptionalCost>();
            VehicleOwnershipTypes = new HashSet<VehicleOwnershipType>();
            VehiclePasses = new HashSet<VehiclePass>();
            VehiclePaymentPlans = new HashSet<VehiclePaymentPlan>();
            VehicleRegistrations = new HashSet<VehicleRegistration>();
            VehicleRentContractTypes = new HashSet<VehicleRentContractType>();
            VehicleRentContracts = new HashSet<VehicleRentContract>();
            VehicleServiceStatuses = new HashSet<VehicleServiceStatus>();
            VehicleStatusHistories = new HashSet<VehicleStatusHistory>();
            VehicleTypes = new HashSet<VehicleType>();
            Vehicles = new HashSet<Vehicle>();
            ViolationCalculations = new HashSet<ViolationCalculation>();
            ViolationTypes = new HashSet<ViolationType>();
            Violations = new HashSet<Violation>();
            WaitingBonuses = new HashSet<WaitingBonuse>();
            WorkPositions = new HashSet<WorkPosition>();
            WorkingExperienceInners = new HashSet<WorkingExperienceInner>();
            WorkingExperienceOutters = new HashSet<WorkingExperienceOutter>();
            ZipCodes = new HashSet<ZipCode>();
        }

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string NormalizedUserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NormalizedEmail { get; set; } = null!;
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string SecurityStamp { get; set; } = null!;
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public bool IsActive { get; set; }
        public int EmployeeId { get; set; }
        /// <summary>
        /// FCM registration token
        /// </summary>
        public string? FcmToken { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual ICollection<AccountSegment> AccountSegments { get; set; }
        public virtual ICollection<AccountingPeriodType> AccountingPeriodTypes { get; set; }
        public virtual ICollection<AccountingPeriod> AccountingPeriods { get; set; }
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual ICollection<CardRebate> CardRebates { get; set; }
        public virtual ICollection<CardType> CardTypes { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<CertificateType> CertificateTypes { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<ClearingCheckType> ClearingCheckTypes { get; set; }
        public virtual ICollection<ClearingCheck> ClearingChecks { get; set; }
        public virtual ICollection<ClearingResult> ClearingResults { get; set; }
        public virtual ICollection<CollectiveTour> CollectiveTourCreatedByNavigations { get; set; }
        public virtual ICollection<CollectiveTour> CollectiveTourDeletedByNavigations { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<CostIncomeType> CostIncomeTypes { get; set; }
        public virtual ICollection<County> Counties { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
        public virtual ICollection<Currency> Currencies { get; set; }
        public virtual ICollection<CustomersFinancialCard> CustomersFinancialCards { get; set; }
        public virtual ICollection<DeductionSchedule> DeductionSchedules { get; set; }
        public virtual ICollection<Deduction> Deductions { get; set; }
        public virtual ICollection<DefectReason> DefectReasons { get; set; }
        public virtual ICollection<DispetcherDriverCommunication> DispetcherDriverCommunications { get; set; }
        public virtual ICollection<DispetcherDriver> DispetcherDrivers { get; set; }
        public virtual ICollection<Document> DocumentDeletedByNavigations { get; set; }
        public virtual ICollection<DocumentSerieType> DocumentSerieTypes { get; set; }
        public virtual ICollection<DocumentSeries> DocumentSeries { get; set; }
        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
        public virtual ICollection<Document> DocumentUploadedByUsers { get; set; }
        public virtual ICollection<DriverFee> DriverFees { get; set; }
        public virtual ICollection<DriverLicenceCategory> DriverLicenceCategories { get; set; }
        public virtual ICollection<DriverLicence> DriverLicences { get; set; }
        public virtual ICollection<DriverStopBonuse> DriverStopBonuses { get; set; }
        public virtual ICollection<EldDisconnectedNote> EldDisconnectedNotes { get; set; }
        public virtual ICollection<EmailQueue> EmailQueues { get; set; }
        public virtual ICollection<EmployeeAbsence> EmployeeAbsences { get; set; }
        public virtual ICollection<EmployeeCard> EmployeeCards { get; set; }
        public virtual ICollection<EmployeeCertificate> EmployeeCertificates { get; set; }
        public virtual ICollection<EmployeeWaiting> EmployeeWaitings { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<EmploymentType> EmploymentTypes { get; set; }
        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<EquipmentType> EquipmentTypes { get; set; }
        public virtual ICollection<EscrowDepositRealization> EscrowDepositRealizations { get; set; }
        public virtual ICollection<EscrowDeposit> EscrowDeposits { get; set; }
        public virtual ICollection<FactoringFee> FactoringFees { get; set; }
        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
        public virtual ICollection<FormMannerError> FormMannerErrors { get; set; }
        public virtual ICollection<FuelConsumption> FuelConsumptions { get; set; }
        public virtual ICollection<Gender> Genders { get; set; }
        public virtual ICollection<GeneralCost> GeneralCosts { get; set; }
        public virtual ICollection<HierarchyLevel> HierarchyLevels { get; set; }
        public virtual ICollection<HosviolationsFixed> HosviolationsFixeds { get; set; }
        public virtual ICollection<InactivityReason> InactivityReasons { get; set; }
        public virtual ICollection<IncidentType> IncidentTypes { get; set; }
        public virtual ICollection<Integration> Integrations { get; set; }
        public virtual ICollection<InteractionEvent> InteractionEvents { get; set; }
        public virtual ICollection<InvoicePayment> InvoicePayments { get; set; }
        public virtual ICollection<InvoiceStatus> InvoiceStatuses { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<JobType> JobTypes { get; set; }
        public virtual ICollection<LicenceCategory> LicenceCategories { get; set; }
        public virtual ICollection<LoanPayback> LoanPaybacks { get; set; }
        public virtual ICollection<LoanPayment> LoanPayments { get; set; }
        public virtual ICollection<LoanPurpose> LoanPurposes { get; set; }
        public virtual ICollection<LoanRequest> LoanRequests { get; set; }
        public virtual ICollection<LoanStatus> LoanStatuses { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
        public virtual ICollection<MaintenanceGroup> MaintenanceGroups { get; set; }
        public virtual ICollection<MaintenancePlan> MaintenancePlans { get; set; }
        public virtual ICollection<MaintenanceService> MaintenanceServices { get; set; }
        public virtual ICollection<MeasureUnitType> MeasureUnitTypes { get; set; }
        public virtual ICollection<MeasureUnit> MeasureUnits { get; set; }
        public virtual ICollection<NoticeType> NoticeTypes { get; set; }
        public virtual ICollection<OrgUnit> OrgUnits { get; set; }
        public virtual ICollection<PalletJack> PalletJacks { get; set; }
        public virtual ICollection<PartnerCategory> PartnerCategories { get; set; }
        public virtual ICollection<PartnerType> PartnerTypes { get; set; }
        public virtual ICollection<Partner> Partners { get; set; }
        public virtual ICollection<PassStatus> PassStatuses { get; set; }
        public virtual ICollection<PaymentCondition> PaymentConditions { get; set; }
        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }
        public virtual ICollection<PtisMissingFixed> PtisMissingFixeds { get; set; }
        public virtual ICollection<ReimbursmentDetentionType> ReimbursmentDetentionTypes { get; set; }
        public virtual ICollection<Relationship> Relationships { get; set; }
        public virtual ICollection<SafetyTest> SafetyTests { get; set; }
        public virtual ICollection<SafetyType> SafetyTypes { get; set; }
        public virtual ICollection<ServiceDocument> ServiceDocuments { get; set; }
        public virtual ICollection<ServiceItem> ServiceItems { get; set; }
        public virtual ICollection<ServiceStatus> ServiceStatuses { get; set; }
        public virtual ICollection<ServiceType> ServiceTypes { get; set; }
        public virtual ICollection<SparePart> SpareParts { get; set; }
        public virtual ICollection<Statement> StatementAccountingUsers { get; set; }
        public virtual ICollection<StatementCredit> StatementCredits { get; set; }
        public virtual ICollection<StatementDeduction> StatementDeductions { get; set; }
        public virtual ICollection<Statement> StatementDeletedByNavigations { get; set; }
        public virtual ICollection<StatementFuelConsumption> StatementFuelConsumptions { get; set; }
        public virtual ICollection<StatementToll> StatementTolls { get; set; }
        public virtual ICollection<StatementTour> StatementTours { get; set; }
        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<SubcontractorFee> SubcontractorFees { get; set; }
        public virtual ICollection<TimeZone> TimeZones { get; set; }
        public virtual ICollection<Toll> Tolls { get; set; }
        public virtual ICollection<TourAccounting> TourAccountingAccountingUsers { get; set; }
        public virtual ICollection<TourAccounting> TourAccountingDeletedByNavigations { get; set; }
        public virtual ICollection<TourCalculationType> TourCalculationTypes { get; set; }
        public virtual ICollection<TourClaim> TourClaims { get; set; }
        public virtual ICollection<TourCost> TourCostDeletedByNavigations { get; set; }
        public virtual ICollection<TourCost> TourCostSubmittedByNavigations { get; set; }
        public virtual ICollection<TourCrewType> TourCrewTypes { get; set; }
        public virtual ICollection<TourCrew> TourCrews { get; set; }
        public virtual ICollection<TourPointSpecialInstruction> TourPointSpecialInstructions { get; set; }
        public virtual ICollection<TourPointType> TourPointTypes { get; set; }
        public virtual ICollection<TourPoint> TourPoints { get; set; }
        public virtual ICollection<TourReimbursmentsDetention> TourReimbursmentsDetentions { get; set; }
        public virtual ICollection<TourStatus> TourStatuses { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
        public virtual ICollection<UnidentifiedDrivingsAssigned> UnidentifiedDrivingsAssigneds { get; set; }
        public virtual ICollection<UserMessage> UserMessageUserFromNavigations { get; set; }
        public virtual ICollection<UserMessage> UserMessageUserToNavigations { get; set; }
        public virtual ICollection<UserSetting> UserSettingDeletedByNavigations { get; set; }
        public virtual ICollection<UserSetting> UserSettingUsers { get; set; }
        public virtual ICollection<VehicleAdvanceCost> VehicleAdvanceCosts { get; set; }
        public virtual ICollection<VehicleAndEquipmentHistory> VehicleAndEquipmentHistories { get; set; }
        public virtual ICollection<VehicleAndTrailersHistory> VehicleAndTrailersHistories { get; set; }
        public virtual ICollection<VehicleAnnualDotinspection> VehicleAnnualDotinspections { get; set; }
        public virtual ICollection<VehicleAssignment> VehicleAssignments { get; set; }
        public virtual ICollection<VehicleCostAndIncomeCalculationType> VehicleCostAndIncomeCalculationTypes { get; set; }
        public virtual ICollection<VehicleCostPayerType> VehicleCostPayerTypes { get; set; }
        public virtual ICollection<VehicleEldDatum> VehicleEldData { get; set; }
        public virtual ICollection<VehicleEldHistory> VehicleEldHistories { get; set; }
        public virtual ICollection<VehicleFmcsaViolation> VehicleFmcsaViolations { get; set; }
        public virtual ICollection<VehicleFmcsainspectionPenaltyType> VehicleFmcsainspectionPenaltyTypes { get; set; }
        public virtual ICollection<VehicleFmcsainspectionType> VehicleFmcsainspectionTypes { get; set; }
        public virtual ICollection<VehicleFmcsainspection> VehicleFmcsainspections { get; set; }
        public virtual ICollection<VehicleIftasetting> VehicleIftasettings { get; set; }
        public virtual ICollection<VehicleIncome> VehicleIncomes { get; set; }
        public virtual ICollection<VehicleMaintenanceCost> VehicleMaintenanceCosts { get; set; }
        public virtual ICollection<VehicleManufacturer> VehicleManufacturers { get; set; }
        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
        public virtual ICollection<VehicleOptionalCost> VehicleOptionalCosts { get; set; }
        public virtual ICollection<VehicleOwnershipType> VehicleOwnershipTypes { get; set; }
        public virtual ICollection<VehiclePass> VehiclePasses { get; set; }
        public virtual ICollection<VehiclePaymentPlan> VehiclePaymentPlans { get; set; }
        public virtual ICollection<VehicleRegistration> VehicleRegistrations { get; set; }
        public virtual ICollection<VehicleRentContractType> VehicleRentContractTypes { get; set; }
        public virtual ICollection<VehicleRentContract> VehicleRentContracts { get; set; }
        public virtual ICollection<VehicleServiceStatus> VehicleServiceStatuses { get; set; }
        public virtual ICollection<VehicleStatusHistory> VehicleStatusHistories { get; set; }
        public virtual ICollection<VehicleType> VehicleTypes { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<ViolationCalculation> ViolationCalculations { get; set; }
        public virtual ICollection<ViolationType> ViolationTypes { get; set; }
        public virtual ICollection<Violation> Violations { get; set; }
        public virtual ICollection<WaitingBonuse> WaitingBonuses { get; set; }
        public virtual ICollection<WorkPosition> WorkPositions { get; set; }
        public virtual ICollection<WorkingExperienceInner> WorkingExperienceInners { get; set; }
        public virtual ICollection<WorkingExperienceOutter> WorkingExperienceOutters { get; set; }
        public virtual ICollection<ZipCode> ZipCodes { get; set; }
    }
}

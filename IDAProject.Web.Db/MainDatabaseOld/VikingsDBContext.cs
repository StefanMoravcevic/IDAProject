using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class VikingsDBContext : DbContext
    {
        public VikingsDBContext()
        {
        }

        public VikingsDBContext(DbContextOptions<VikingsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountSegment> AccountSegments { get; set; } = null!;
        public virtual DbSet<AccountingPeriod> AccountingPeriods { get; set; } = null!;
        public virtual DbSet<AccountingPeriodType> AccountingPeriodTypes { get; set; } = null!;
        public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; } = null!;
        public virtual DbSet<AspNetFeature> AspNetFeatures { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetRoleFeature> AspNetRoleFeatures { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<CardRebate> CardRebates { get; set; } = null!;
        public virtual DbSet<CardType> CardTypes { get; set; } = null!;
        public virtual DbSet<CertificateType> CertificateTypes { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<ClearingCheck> ClearingChecks { get; set; } = null!;
        public virtual DbSet<ClearingCheckType> ClearingCheckTypes { get; set; } = null!;
        public virtual DbSet<ClearingResult> ClearingResults { get; set; } = null!;
        public virtual DbSet<CollectiveTour> CollectiveTours { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<CostIncomeType> CostIncomeTypes { get; set; } = null!;
        public virtual DbSet<Counter> Counters { get; set; } = null!;
        public virtual DbSet<County> Counties { get; set; } = null!;
        public virtual DbSet<Credit> Credits { get; set; } = null!;
        public virtual DbSet<CronNotification> CronNotifications { get; set; } = null!;
        public virtual DbSet<CronNotificationType> CronNotificationTypes { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<CustomersFinancialCard> CustomersFinancialCards { get; set; } = null!;
        public virtual DbSet<Deduction> Deductions { get; set; } = null!;
        public virtual DbSet<DeductionSchedule> DeductionSchedules { get; set; } = null!;
        public virtual DbSet<DefectReason> DefectReasons { get; set; } = null!;
        public virtual DbSet<DispetcherDriver> DispetcherDrivers { get; set; } = null!;
        public virtual DbSet<DispetcherDriverCommunication> DispetcherDriverCommunications { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<DocumentSerieType> DocumentSerieTypes { get; set; } = null!;
        public virtual DbSet<DocumentSeries> DocumentSeries { get; set; } = null!;
        public virtual DbSet<DocumentType> DocumentTypes { get; set; } = null!;
        public virtual DbSet<DocumentsSource> DocumentsSources { get; set; } = null!;
        public virtual DbSet<DriverFee> DriverFees { get; set; } = null!;
        public virtual DbSet<DriverLicence> DriverLicences { get; set; } = null!;
        public virtual DbSet<DriverLicenceCategory> DriverLicenceCategories { get; set; } = null!;
        public virtual DbSet<DriverStopBonuse> DriverStopBonuses { get; set; } = null!;
        public virtual DbSet<EldDisconnectedNote> EldDisconnectedNotes { get; set; } = null!;
        public virtual DbSet<EmailQueue> EmailQueues { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeAbsence> EmployeeAbsences { get; set; } = null!;
        public virtual DbSet<EmployeeCard> EmployeeCards { get; set; } = null!;
        public virtual DbSet<EmployeeCertificate> EmployeeCertificates { get; set; } = null!;
        public virtual DbSet<EmployeeWaiting> EmployeeWaitings { get; set; } = null!;
        public virtual DbSet<EmploymentType> EmploymentTypes { get; set; } = null!;
        public virtual DbSet<Equipment> Equipments { get; set; } = null!;
        public virtual DbSet<EquipmentType> EquipmentTypes { get; set; } = null!;
        public virtual DbSet<EscrowDeposit> EscrowDeposits { get; set; } = null!;
        public virtual DbSet<EscrowDepositRealization> EscrowDepositRealizations { get; set; } = null!;
        public virtual DbSet<FactoringFee> FactoringFees { get; set; } = null!;
        public virtual DbSet<FamilyMember> FamilyMembers { get; set; } = null!;
        public virtual DbSet<FormMannerError> FormMannerErrors { get; set; } = null!;
        public virtual DbSet<FuelConsumption> FuelConsumptions { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<GeneralCost> GeneralCosts { get; set; } = null!;
        public virtual DbSet<GeneralSetting> GeneralSettings { get; set; } = null!;
        public virtual DbSet<Hash> Hashes { get; set; } = null!;
        public virtual DbSet<HierarchyLevel> HierarchyLevels { get; set; } = null!;
        public virtual DbSet<HosviolationsFixed> HosviolationsFixeds { get; set; } = null!;
        public virtual DbSet<InactivityReason> InactivityReasons { get; set; } = null!;
        public virtual DbSet<IncidentType> IncidentTypes { get; set; } = null!;
        public virtual DbSet<Integration> Integrations { get; set; } = null!;
        public virtual DbSet<InteractionEvent> InteractionEvents { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoicePayment> InvoicePayments { get; set; } = null!;
        public virtual DbSet<InvoiceStatus> InvoiceStatuses { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<JobParameter> JobParameters { get; set; } = null!;
        public virtual DbSet<JobQueue> JobQueues { get; set; } = null!;
        public virtual DbSet<JobType> JobTypes { get; set; } = null!;
        public virtual DbSet<LicenceCategory> LicenceCategories { get; set; } = null!;
        public virtual DbSet<List> Lists { get; set; } = null!;
        public virtual DbSet<Loan> Loans { get; set; } = null!;
        public virtual DbSet<LoanPayback> LoanPaybacks { get; set; } = null!;
        public virtual DbSet<LoanPayment> LoanPayments { get; set; } = null!;
        public virtual DbSet<LoanPurpose> LoanPurposes { get; set; } = null!;
        public virtual DbSet<LoanRequest> LoanRequests { get; set; } = null!;
        public virtual DbSet<LoanStatus> LoanStatuses { get; set; } = null!;
        public virtual DbSet<MaintenanceGroup> MaintenanceGroups { get; set; } = null!;
        public virtual DbSet<MaintenancePlan> MaintenancePlans { get; set; } = null!;
        public virtual DbSet<MaintenanceService> MaintenanceServices { get; set; } = null!;
        public virtual DbSet<MeasureUnit> MeasureUnits { get; set; } = null!;
        public virtual DbSet<MeasureUnitType> MeasureUnitTypes { get; set; } = null!;
        public virtual DbSet<NoticeType> NoticeTypes { get; set; } = null!;
        public virtual DbSet<OrgUnit> OrgUnits { get; set; } = null!;
        public virtual DbSet<PalletJack> PalletJacks { get; set; } = null!;
        public virtual DbSet<Partner> Partners { get; set; } = null!;
        public virtual DbSet<PartnerCategory> PartnerCategories { get; set; } = null!;
        public virtual DbSet<PartnerType> PartnerTypes { get; set; } = null!;
        public virtual DbSet<PassStatus> PassStatuses { get; set; } = null!;
        public virtual DbSet<PaymentCondition> PaymentConditions { get; set; } = null!;
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public virtual DbSet<PtisMissingFixed> PtisMissingFixeds { get; set; } = null!;
        public virtual DbSet<ReimbursmentDetentionType> ReimbursmentDetentionTypes { get; set; } = null!;
        public virtual DbSet<Relationship> Relationships { get; set; } = null!;
        public virtual DbSet<SafetyTest> SafetyTests { get; set; } = null!;
        public virtual DbSet<SafetyType> SafetyTypes { get; set; } = null!;
        public virtual DbSet<Schema> Schemas { get; set; } = null!;
        public virtual DbSet<Server> Servers { get; set; } = null!;
        public virtual DbSet<ServiceDocument> ServiceDocuments { get; set; } = null!;
        public virtual DbSet<ServiceItem> ServiceItems { get; set; } = null!;
        public virtual DbSet<ServiceStatus> ServiceStatuses { get; set; } = null!;
        public virtual DbSet<ServiceType> ServiceTypes { get; set; } = null!;
        public virtual DbSet<Set> Sets { get; set; } = null!;
        public virtual DbSet<SparePart> SpareParts { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;
        public virtual DbSet<State1> States1 { get; set; } = null!;
        public virtual DbSet<Statement> Statements { get; set; } = null!;
        public virtual DbSet<StatementCredit> StatementCredits { get; set; } = null!;
        public virtual DbSet<StatementDeduction> StatementDeductions { get; set; } = null!;
        public virtual DbSet<StatementFuelConsumption> StatementFuelConsumptions { get; set; } = null!;
        public virtual DbSet<StatementToll> StatementTolls { get; set; } = null!;
        public virtual DbSet<StatementTour> StatementTours { get; set; } = null!;
        public virtual DbSet<SubcontractorFee> SubcontractorFees { get; set; } = null!;
        public virtual DbSet<TimeZone> TimeZones { get; set; } = null!;
        public virtual DbSet<Toll> Tolls { get; set; } = null!;
        public virtual DbSet<Tour> Tours { get; set; } = null!;
        public virtual DbSet<TourAccounting> TourAccountings { get; set; } = null!;
        public virtual DbSet<TourCalculationType> TourCalculationTypes { get; set; } = null!;
        public virtual DbSet<TourClaim> TourClaims { get; set; } = null!;
        public virtual DbSet<TourCost> TourCosts { get; set; } = null!;
        public virtual DbSet<TourCrew> TourCrews { get; set; } = null!;
        public virtual DbSet<TourCrewType> TourCrewTypes { get; set; } = null!;
        public virtual DbSet<TourPoint> TourPoints { get; set; } = null!;
        public virtual DbSet<TourPointSpecialInstruction> TourPointSpecialInstructions { get; set; } = null!;
        public virtual DbSet<TourPointType> TourPointTypes { get; set; } = null!;
        public virtual DbSet<TourReimbursmentsDetention> TourReimbursmentsDetentions { get; set; } = null!;
        public virtual DbSet<TourStatus> TourStatuses { get; set; } = null!;
        public virtual DbSet<UnidentifiedDrivingsAssigned> UnidentifiedDrivingsAssigneds { get; set; } = null!;
        public virtual DbSet<UserMessage> UserMessages { get; set; } = null!;
        public virtual DbSet<UserSetting> UserSettings { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<VehicleAdvanceCost> VehicleAdvanceCosts { get; set; } = null!;
        public virtual DbSet<VehicleAndEquipmentHistory> VehicleAndEquipmentHistories { get; set; } = null!;
        public virtual DbSet<VehicleAndTrailersHistory> VehicleAndTrailersHistories { get; set; } = null!;
        public virtual DbSet<VehicleAnnualDotinspection> VehicleAnnualDotinspections { get; set; } = null!;
        public virtual DbSet<VehicleAssignment> VehicleAssignments { get; set; } = null!;
        public virtual DbSet<VehicleCostAndIncomeCalculationType> VehicleCostAndIncomeCalculationTypes { get; set; } = null!;
        public virtual DbSet<VehicleCostPayerType> VehicleCostPayerTypes { get; set; } = null!;
        public virtual DbSet<VehicleEldDatum> VehicleEldData { get; set; } = null!;
        public virtual DbSet<VehicleEldHistory> VehicleEldHistories { get; set; } = null!;
        public virtual DbSet<VehicleFmcsaViolation> VehicleFmcsaViolations { get; set; } = null!;
        public virtual DbSet<VehicleFmcsainspection> VehicleFmcsainspections { get; set; } = null!;
        public virtual DbSet<VehicleFmcsainspectionPenaltyType> VehicleFmcsainspectionPenaltyTypes { get; set; } = null!;
        public virtual DbSet<VehicleFmcsainspectionType> VehicleFmcsainspectionTypes { get; set; } = null!;
        public virtual DbSet<VehicleIftasetting> VehicleIftasettings { get; set; } = null!;
        public virtual DbSet<VehicleIncome> VehicleIncomes { get; set; } = null!;
        public virtual DbSet<VehicleMaintenanceCost> VehicleMaintenanceCosts { get; set; } = null!;
        public virtual DbSet<VehicleManufacturer> VehicleManufacturers { get; set; } = null!;
        public virtual DbSet<VehicleModel> VehicleModels { get; set; } = null!;
        public virtual DbSet<VehicleOptionalCost> VehicleOptionalCosts { get; set; } = null!;
        public virtual DbSet<VehicleOwnershipType> VehicleOwnershipTypes { get; set; } = null!;
        public virtual DbSet<VehiclePass> VehiclePasses { get; set; } = null!;
        public virtual DbSet<VehiclePassType> VehiclePassTypes { get; set; } = null!;
        public virtual DbSet<VehiclePaymentPlan> VehiclePaymentPlans { get; set; } = null!;
        public virtual DbSet<VehicleRegistration> VehicleRegistrations { get; set; } = null!;
        public virtual DbSet<VehicleRentContract> VehicleRentContracts { get; set; } = null!;
        public virtual DbSet<VehicleRentContractType> VehicleRentContractTypes { get; set; } = null!;
        public virtual DbSet<VehicleServiceStatus> VehicleServiceStatuses { get; set; } = null!;
        public virtual DbSet<VehicleStatusHistory> VehicleStatusHistories { get; set; } = null!;
        public virtual DbSet<VehicleType> VehicleTypes { get; set; } = null!;
        public virtual DbSet<Violation> Violations { get; set; } = null!;
        public virtual DbSet<ViolationCalculation> ViolationCalculations { get; set; } = null!;
        public virtual DbSet<ViolationType> ViolationTypes { get; set; } = null!;
        public virtual DbSet<WaitingBonuse> WaitingBonuses { get; set; } = null!;
        public virtual DbSet<WorkPosition> WorkPositions { get; set; } = null!;
        public virtual DbSet<WorkingExperienceInner> WorkingExperienceInners { get; set; } = null!;
        public virtual DbSet<WorkingExperienceOutter> WorkingExperienceOutters { get; set; } = null!;
        public virtual DbSet<ZipCode> ZipCodes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountSegment>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.AccountSegments)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_AccountSegments_AspNetUsers");
            });

            modelBuilder.Entity<AccountingPeriod>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.AccountingPeriods)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_AccountingPeriods_AspNetUsers");
            });

            modelBuilder.Entity<AccountingPeriodType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(64);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.AccountingPeriodTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_AccountingPeriodTypes_AspNetUsers");
            });

            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK_HangFire_CounterAggregated");

                entity.ToTable("AggregatedCounter", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_AggregatedCounter_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<AspNetFeature>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.AspNetRoles)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_AspNetRoles_Companies");
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoleFeature>(entity =>
            {
                entity.HasOne(d => d.AspNetFeature)
                    .WithMany(p => p.AspNetRoleFeatures)
                    .HasForeignKey(d => d.AspNetFeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AspNetRoleFeatures_AspNetFeatures");

                entity.HasOne(d => d.AspNetRole)
                    .WithMany(p => p.AspNetRoleFeatures)
                    .HasForeignKey(d => d.AspNetRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AspNetRoleFeatures_AspNetRoles");
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FcmToken)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasComment("FCM registration token");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AspNetUsers_Employees");
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasIndex(e => new { e.RoleId, e.UserId }, "IX_AspNetUserRoles_Relation")
                    .IsUnique();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserTokens_AspNetUsers");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(e => e.CardNumber).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.Pin).HasColumnName("PIN");

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.HasOne(d => d.CardType)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.CardTypeId)
                    .HasConstraintName("FK_Card_CardType");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Cards_AspNetUsers");
            });

            modelBuilder.Entity<CardRebate>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.RebateAmount).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.AccountingPeriod)
                    .WithMany(p => p.CardRebates)
                    .HasForeignKey(d => d.AccountingPeriodId)
                    .HasConstraintName("FK_CardRebates_AccountingPeriods");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.CardRebates)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CardRebates_Cards");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.CardRebates)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_CardRebates_AspNetUsers");
            });

            modelBuilder.Entity<CardType>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.CardTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_CardTypes_AspNetUsers");
            });

            modelBuilder.Entity<CertificateType>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.CertificateTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_CertificateTypes_AspNetUsers");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.County)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_County");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Cities_AspNetUsers");
            });

            modelBuilder.Entity<ClearingCheck>(entity =>
            {
                entity.Property(e => e.CheckDate).HasColumnType("datetime");

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.HasOne(d => d.ClearingCheckType)
                    .WithMany(p => p.ClearingChecks)
                    .HasForeignKey(d => d.ClearingCheckTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClearingChecks_ClearingCheckTypes");

                entity.HasOne(d => d.ClearingResult)
                    .WithMany(p => p.ClearingChecks)
                    .HasForeignKey(d => d.ClearingResultId)
                    .HasConstraintName("FK_ClearingChecks_ClearingResults");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.ClearingChecks)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_ClearingChecks_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ClearingChecks)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClearingChecks_Employees");
            });

            modelBuilder.Entity<ClearingCheckType>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.ClearingCheckTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_ClearingCheckTypes_AspNetUsers");
            });

            modelBuilder.Entity<ClearingResult>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.ClearingResults)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_ClearingResults_AspNetUsers");
            });

            modelBuilder.Entity<CollectiveTour>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Mileage).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CollectiveTourCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CollectiveTours_CreatedBy");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.CollectiveTourDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_CollectiveTours_DeletedBy");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Dot)
                    .HasMaxLength(50)
                    .HasColumnName("DOT");

                entity.Property(e => e.Ein)
                    .HasMaxLength(50)
                    .HasColumnName("EIN");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("EMail");

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.Mc)
                    .HasMaxLength(50)
                    .HasColumnName("MC");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.ResponsiblePerson).HasMaxLength(100);

                entity.Property(e => e.WebAddress).HasMaxLength(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_City");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Companies_AspNetUsers");

                entity.HasOne(d => d.FactoringHouse)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.FactoringHouseId)
                    .HasConstraintName("FK_Companies_Partners");

                entity.HasOne(d => d.IdParentCompanyNavigation)
                    .WithMany(p => p.InverseIdParentCompanyNavigation)
                    .HasForeignKey(d => d.IdParentCompany)
                    .HasConstraintName("FK_Companies_Companies");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_State");

                entity.HasOne(d => d.ZipCode)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.ZipCodeId)
                    .HasConstraintName("FK_Company_ZipCode");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.CompanyId).HasComment("The company (tentant) where the contact belongs to");

                entity.Property(e => e.CompanyName).HasMaxLength(100);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Dot)
                    .HasMaxLength(50)
                    .HasColumnName("DOT");

                entity.Property(e => e.Ein)
                    .HasMaxLength(50)
                    .HasColumnName("EIN");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Mc)
                    .HasMaxLength(50)
                    .HasColumnName("MC");

                entity.Property(e => e.MethodOfCommunication).HasComment("Preferred method of communication (1-phone, 2-mobile, 3-mail)");

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.PartnerId).HasComment("The partner tied to this contact");

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Contacts_Cities");

                entity.HasOne(d => d.ContactCompany)
                    .WithMany(p => p.InverseContactCompany)
                    .HasForeignKey(d => d.ContactCompanyId)
                    .HasConstraintName("FK_Contacts_Contacts");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Contacts_AspNetUsers");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK_Contacts_Genders");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.PartnerId)
                    .HasConstraintName("FK_Contacts_Partners");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Contacts_States");

                entity.HasOne(d => d.ZipCode)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ZipCodeId)
                    .HasConstraintName("FK_Contacts_ZipCode");
            });

            modelBuilder.Entity<CostIncomeType>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CostIncome).HasColumnName("Cost_Income");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.AccountSegment)
                    .WithMany(p => p.CostIncomeTypes)
                    .HasForeignKey(d => d.AccountSegmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CostIncomeType_AccountSegment");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.CostIncomeTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_CostIncomeTypes_AspNetUsers");
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Counter", "HangFire");

                entity.HasIndex(e => e.Key, "CX_HangFire_Counter")
                    .IsClustered();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key).HasMaxLength(100);
            });

            modelBuilder.Entity<County>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Counties)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Counties_AspNetUsers");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Counties)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_County_State");
            });

            modelBuilder.Entity<Credit>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.HasOne(d => d.CostIncomeType)
                    .WithMany(p => p.Credits)
                    .HasForeignKey(d => d.CostIncomeTypeId)
                    .HasConstraintName("FK_Credits_CostIncomeTypes");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Credits)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Credits_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Credits)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Credits_Employees");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Credits)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Credits_Vehicles");
            });

            modelBuilder.Entity<CronNotification>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.FinishedDate).HasColumnType("datetime");

                entity.Property(e => e.RetryCount).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.CronNotifications)
                    .HasForeignKey(d => d.NotificationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CronNotifications_CronNotificationType");
            });

            modelBuilder.Entity<CronNotificationType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(64);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.AlphaId).HasMaxLength(3);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Currencies)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Currencies_AspNetUsers");
            });

            modelBuilder.Entity<CustomersFinancialCard>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceNumber).HasMaxLength(50);

                entity.Property(e => e.MaturityDate).HasColumnType("datetime");

                entity.Property(e => e.OpenAmount).HasColumnType("money");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CustomersFinancialCards)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomersFinancialCards_Company");

                entity.HasOne(d => d.CustomerPartner)
                    .WithMany(p => p.CustomersFinancialCardCustomerPartners)
                    .HasForeignKey(d => d.CustomerPartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomersFinancialCards_Partners");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.CustomersFinancialCards)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_CustomersFinancialCards_AspNetUsers");

                entity.HasOne(d => d.FactoringCompany)
                    .WithMany(p => p.CustomersFinancialCardFactoringCompanies)
                    .HasForeignKey(d => d.FactoringCompanyId)
                    .HasConstraintName("FK_CustomersFinancialCards_FactoringPartners");
            });

            modelBuilder.Entity<Deduction>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.HasOne(d => d.CostIncomeType)
                    .WithMany(p => p.Deductions)
                    .HasForeignKey(d => d.CostIncomeTypeId)
                    .HasConstraintName("FK_Deductions_CostIncomeTypes");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Deductions)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Deductions_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Deductions)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Deductions_Employees");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Deductions)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Deductions_Vehicles");
            });

            modelBuilder.Entity<DeductionSchedule>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.InstalmentAmount).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Deduction)
                    .WithMany(p => p.DeductionSchedules)
                    .HasForeignKey(d => d.DeductionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeductionSchedules_Deductions");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.DeductionSchedules)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_DeductionSchedules_AspNetUsers");
            });

            modelBuilder.Entity<DefectReason>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.DefectReasons)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_DefectReasons_AspNetUsers");
            });

            modelBuilder.Entity<DispetcherDriver>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.DispetcherDrivers)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_DispetcherDrivers_AspNetUsers");

                entity.HasOne(d => d.Dispetcher)
                    .WithMany(p => p.DispetcherDriverDispetchers)
                    .HasForeignKey(d => d.DispetcherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DispetcherDrivers_Employees");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.DispetcherDriverDrivers)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DispetcherDrivers_Driver");
            });

            modelBuilder.Entity<DispetcherDriverCommunication>(entity =>
            {
                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.DispetcherDriverCommunications)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_DispetcherDriverCommunications_AspNetUsers");

                entity.HasOne(d => d.Dispetcher)
                    .WithMany(p => p.DispetcherDriverCommunicationDispetchers)
                    .HasForeignKey(d => d.DispetcherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DispetcherDriverCommunications_Employees1");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.DispetcherDriverCommunicationDrivers)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DispetcherDriverCommunications_Employees");

                entity.HasOne(d => d.InteractionEvent)
                    .WithMany(p => p.DispetcherDriverCommunications)
                    .HasForeignKey(d => d.InteractionEventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DispetcherDriverCommunications_InteractionEvents");

                entity.HasOne(d => d.NoticeType)
                    .WithMany(p => p.DispetcherDriverCommunications)
                    .HasForeignKey(d => d.NoticeTypeId)
                    .HasConstraintName("FK_DispetcherDriverCommunications_NoticeTypes");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DownloadFileName).HasMaxLength(200);

                entity.Property(e => e.RelativeFilePath)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UploadedDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.DocumentDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Documents_AspNetUsers");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Documents_DocumentTypes");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.SourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Documents_DocumentsSource");

                entity.HasOne(d => d.UploadedByUser)
                    .WithMany(p => p.DocumentUploadedByUsers)
                    .HasForeignKey(d => d.UploadedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Documents_AspNetUser_UploadedBy");
            });

            modelBuilder.Entity<DocumentSerieType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.DocumentSerieTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_DocumentSerieTypes_AspNetUsers");
            });

            modelBuilder.Entity<DocumentSeries>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.IncrementSeed).HasDefaultValueSql("((1))");

                entity.Property(e => e.Pattern).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.DocumentSeries)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_DocumentSeries_AspNetUsers");

                entity.HasOne(d => d.DocumentSerieType)
                    .WithMany(p => p.DocumentSeries)
                    .HasForeignKey(d => d.DocumentSerieTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentSeries_DocumentSerieTypes");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.DocumentTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_DocumentTypes_AspNetUsers");
            });

            modelBuilder.Entity<DocumentsSource>(entity =>
            {
                entity.ToTable("DocumentsSource");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(64);
            });

            modelBuilder.Entity<DriverFee>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Fee)
                    .HasColumnType("money")
                    .HasComment("Amount per mileage or percent of the tour");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.CalculationType)
                    .WithMany(p => p.DriverFees)
                    .HasForeignKey(d => d.CalculationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DriverFees_CalculationTypes");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.DriverFees)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_DriverFees_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.DriverFees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DriverFees_Employees");
            });

            modelBuilder.Entity<DriverLicence>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.Number).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.DriverLicences)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_DriverLicences_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.DriverLicences)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DriverLicences_Employees");

                entity.HasOne(d => d.IssueState)
                    .WithMany(p => p.DriverLicences)
                    .HasForeignKey(d => d.IssueStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DriverLicences_States");
            });

            modelBuilder.Entity<DriverLicenceCategory>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.DriverLicenceCategories)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_DriverLicenceCategories_AspNetUsers");

                entity.HasOne(d => d.DriverLicence)
                    .WithMany(p => p.DriverLicenceCategories)
                    .HasForeignKey(d => d.DriverLicenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DriverLicenceCategories_DriverLicences");

                entity.HasOne(d => d.LicenceCategory)
                    .WithMany(p => p.DriverLicenceCategories)
                    .HasForeignKey(d => d.LicenceCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DriverLicenceCategories_LicenceCategories");
            });

            modelBuilder.Entity<DriverStopBonuse>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.DriverStopBonuses)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_DriverStopBonuses_AspNetUsers");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.DriverStopBonuses)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DriverStopBonuses_Employees");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.DriverStopBonuses)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DriverStopBonuses_Vehicles");
            });

            modelBuilder.Entity<EldDisconnectedNote>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.EldDisconnectedNotes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_ELDDisconnectedNotes_AspNetUsers");
            });

            modelBuilder.Entity<EmailQueue>(entity =>
            {
                entity.ToTable("EmailQueue");

                entity.Property(e => e.DateQueued).HasColumnType("datetime");

                entity.Property(e => e.DateSent).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailCc)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.EmailTo)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Subject)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.EmailQueues)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_EmailQueue_AspNetUsers");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.AccountingCode).HasMaxLength(50);

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.BankAccount).HasMaxLength(50);

                entity.Property(e => e.BankAccountAddition).HasMaxLength(50);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.BirthPlace).HasMaxLength(50);

                entity.Property(e => e.CellPhoneNumber).HasMaxLength(50);

                entity.Property(e => e.Citizenship).HasMaxLength(50);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("EMail");

                entity.Property(e => e.FederalNumber).HasMaxLength(50);

                entity.Property(e => e.HousePhoneNumber).HasMaxLength(50);

                entity.Property(e => e.InsuranceNumber).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PassportId)
                    .HasMaxLength(50)
                    .HasColumnName("PassportID");

                entity.Property(e => e.PersonalId)
                    .HasMaxLength(50)
                    .HasColumnName("PersonalID");

                entity.Property(e => e.RoutingNumber).HasMaxLength(50);

                entity.Property(e => e.ShoeSize).HasMaxLength(10);

                entity.Property(e => e.SuiteSize).HasMaxLength(10);

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Employees_Cities");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Company");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Employees_AspNetUsers");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK_Employees_Genders");

                entity.HasOne(d => d.JobType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_JobTypes");

                entity.HasOne(d => d.NoticeType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.NoticeTypeId)
                    .HasConstraintName("FK_Employees_NoticeTypes");

                entity.HasOne(d => d.OrgUnit)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.OrgUnitId)
                    .HasConstraintName("FK_Employees_OrgUnits");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PartnerId)
                    .HasConstraintName("FK_Employees_Partners");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Employees_States");

                entity.HasOne(d => d.ZipCode)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ZipCodeId)
                    .HasConstraintName("FK_Employees_ZipCodes");
            });

            modelBuilder.Entity<EmployeeAbsence>(entity =>
            {
                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.EmployeeAbsences)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_EmployeeAbsences_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeAbsences)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeAbsences_Employees");

                entity.HasOne(d => d.InactivityReason)
                    .WithMany(p => p.EmployeeAbsences)
                    .HasForeignKey(d => d.InactivityReasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeAbsences_InactivityReasons");
            });

            modelBuilder.Entity<EmployeeCard>(entity =>
            {
                entity.Property(e => e.ChargeDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.RebateAmount).HasColumnType("money");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.EmployeeCards)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeCards_Cards");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.EmployeeCards)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_EmployeeCards_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeCards)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeCards_Employees");
            });

            modelBuilder.Entity<EmployeeCertificate>(entity =>
            {
                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.CertificateType)
                    .WithMany(p => p.EmployeeCertificates)
                    .HasForeignKey(d => d.CertificateTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeCertificates_CertificateTypes");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.EmployeeCertificates)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_EmployeeCertificates_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeCertificates)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeCertificates_Employees");
            });

            modelBuilder.Entity<EmployeeWaiting>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.EmployeeWaitings)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_EmployeeWaitings_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeWaitings)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeWaitings_Employees");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.EmployeeWaitings)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK_EmployeeWaitings_Tours");
            });

            modelBuilder.Entity<EmploymentType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.EmploymentTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_EmploymentTypes_AspNetUsers");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.SerialNumber).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Equipments_AspNetUsers");

                entity.HasOne(d => d.EquipmentType)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.EquipmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Equipments_EquipmentType");
            });

            modelBuilder.Entity<EquipmentType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.EquipmentTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_EquipmentTypes_AspNetUsers");
            });

            modelBuilder.Entity<EscrowDeposit>(entity =>
            {
                entity.Property(e => e.AccountingStartDate).HasColumnType("datetime");

                entity.Property(e => e.AmountKept).HasColumnType("money");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DepositAmount).HasColumnType("money");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.EscrowDeposits)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_EscrowDeposits_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EscrowDeposits)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EscrowDeposits_Employees");
            });

            modelBuilder.Entity<EscrowDepositRealization>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.EscrowDepositRealizations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_EscrowDepositRealizations_AspNetUsers");

                entity.HasOne(d => d.EscrowDeposit)
                    .WithMany(p => p.EscrowDepositRealizations)
                    .HasForeignKey(d => d.EscrowDepositId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EscrowDepositRealizations_EscrowDeposits");
            });

            modelBuilder.Entity<FactoringFee>(entity =>
            {
                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Fee).HasColumnType("smallmoney");

                entity.Property(e => e.RecordDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.FactoringFees)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_FactoringFees_Company");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.FactoringFees)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_FactoringFees_AspNetUsers");

                entity.HasOne(d => d.FactoringCompany)
                    .WithMany(p => p.FactoringFees)
                    .HasForeignKey(d => d.FactoringCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FactoringFees_FactoringPartners");
            });

            modelBuilder.Entity<FamilyMember>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("EMail");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.FamilyMembers)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_FamilyMembers_Cities");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.FamilyMembers)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_FamilyMembers_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.FamilyMembers)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FamilyMembers_Employees");

                entity.HasOne(d => d.Relationship)
                    .WithMany(p => p.FamilyMembers)
                    .HasForeignKey(d => d.RelationshipId)
                    .HasConstraintName("FK_FamilyMembers_Relationships");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.FamilyMembers)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_FamilyMembers_States");
            });

            modelBuilder.Entity<FormMannerError>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.FormMannerErrors)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_FormMannerErrors_AspNetUsers");
            });

            modelBuilder.Entity<FuelConsumption>(entity =>
            {
                entity.Property(e => e.Advance).HasColumnType("money");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Misc).HasColumnType("money");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Product).HasMaxLength(200);

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.FuelConsumptions)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("FK_FuelConsumptions_Cards");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.FuelConsumptions)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_FuelConsumptions_Cities");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.FuelConsumptions)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_FuelConsumptions_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.FuelConsumptions)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_FuelConsumptions_Employees");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.FuelConsumptions)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_FuelConsumptions_States");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.FuelConsumptions)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_FuelConsumptions_Vehicles");

                entity.HasOne(d => d.ZipCode)
                    .WithMany(p => p.FuelConsumptions)
                    .HasForeignKey(d => d.ZipCodeId)
                    .HasConstraintName("FK_FuelConsumptions_ZipCodes");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Genders)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Genders_AspNetUsers");
            });

            modelBuilder.Entity<GeneralCost>(entity =>
            {
                entity.HasKey(e => e.CostId)
                    .HasName("PK_Cost");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.MonthlyCostAmount).HasColumnType("money");

                entity.HasOne(d => d.AccountingPeriod)
                    .WithMany(p => p.GeneralCosts)
                    .HasForeignKey(d => d.AccountingPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cost_AccountingPeriod");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.GeneralCosts)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cost_Company");

                entity.HasOne(d => d.CostIncomeType)
                    .WithMany(p => p.GeneralCosts)
                    .HasForeignKey(d => d.CostIncomeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cost_CostIncomeType");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.GeneralCosts)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_GeneralCosts_AspNetUsers");

                entity.HasOne(d => d.OrgUnit)
                    .WithMany(p => p.GeneralCosts)
                    .HasForeignKey(d => d.OrgUnitId)
                    .HasConstraintName("FK_Cost_OrgUnit");
            });

            modelBuilder.Entity<GeneralSetting>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateFormat).HasMaxLength(50);

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.GeneralSettings)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GeneralSettings_Currencies");

                entity.HasOne(d => d.MeasureFuel)
                    .WithMany(p => p.GeneralSettingMeasureFuels)
                    .HasForeignKey(d => d.MeasureFuelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GeneralSettings_MeasureUnits_Fuel");

                entity.HasOne(d => d.MeasureTraveledWay)
                    .WithMany(p => p.GeneralSettingMeasureTraveledWays)
                    .HasForeignKey(d => d.MeasureTraveledWayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GeneralSettings_MeasureUnits_TraveledWay");

                entity.HasOne(d => d.MeasureVehicleLength)
                    .WithMany(p => p.GeneralSettingMeasureVehicleLengths)
                    .HasForeignKey(d => d.MeasureVehicleLengthId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GeneralSettings_MeasureUnits_VehicleLength");

                entity.HasOne(d => d.MeasureVehicleWeight)
                    .WithMany(p => p.GeneralSettingMeasureVehicleWeights)
                    .HasForeignKey(d => d.MeasureVehicleWeightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GeneralSettings_MeasureUnits_VehicleWeight");
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Field })
                    .HasName("PK_HangFire_Hash");

                entity.ToTable("Hash", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Hash_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Field).HasMaxLength(100);
            });

            modelBuilder.Entity<HierarchyLevel>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.HierarchyLevels)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_HierarchyLevels_AspNetUsers");
            });

            modelBuilder.Entity<HosviolationsFixed>(entity =>
            {
                entity.ToTable("HOSViolationsFixeds");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.HosviolationsFixeds)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_HOSViolationsFixed_AspNetUsers");
            });

            modelBuilder.Entity<InactivityReason>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DriverVehicle).HasColumnName("Driver_Vehicle");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.InactivityReasons)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_InactivityReasons_AspNetUsers");
            });

            modelBuilder.Entity<IncidentType>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.IncidentTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_IncidentTypes_AspNetUsers");
            });

            modelBuilder.Entity<Integration>(entity =>
            {
                entity.HasKey(e => e.ApiKey);

                entity.Property(e => e.ApiKey).ValueGeneratedNever();

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Integrations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Integrations_AspNetUsers");
            });

            modelBuilder.Entity<InteractionEvent>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.InteractionEvents)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_InteractionEvents_AspNetUsers");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceStatusId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Number).HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Invoices_Companies");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Invoices_AspNetUsers");

                entity.HasOne(d => d.FactoringHouse)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.FactoringHouseId)
                    .HasConstraintName("FK_Invoices_Partners");

                entity.HasOne(d => d.InvoiceStatus)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.InvoiceStatusId)
                    .HasConstraintName("FK_Invoices_InvoiceStatuses");

                entity.HasOne(d => d.PaymentCondition)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.PaymentConditionId)
                    .HasConstraintName("FK_Invoices_PaymentConditions");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK_Invoices_PaymentMethods");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoices_Tours");
            });

            modelBuilder.Entity<InvoicePayment>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.InvoicePayments)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_InvoicePayments_AspNetUsers");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoicePayments)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoicePayments_Invoices");
            });

            modelBuilder.Entity<InvoiceStatus>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.InvoiceStatuses)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_InvoiceStatuses_AspNetUsers");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Job_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.HasIndex(e => e.StateName, "IX_HangFire_Job_StateName")
                    .HasFilter("([StateName] IS NOT NULL)");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Name })
                    .HasName("PK_HangFire_JobParameter");

                entity.ToTable("JobParameter", "HangFire");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobParameters)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_JobParameter_Job");
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.HasKey(e => new { e.Queue, e.Id })
                    .HasName("PK_HangFire_JobQueue");

                entity.ToTable("JobQueue", "HangFire");

                entity.Property(e => e.Queue).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<JobType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.JobTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_JobTypes_AspNetUsers");
            });

            modelBuilder.Entity<LicenceCategory>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(5);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.LicenceCategories)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_LicenceCategories_AspNetUsers");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Id })
                    .HasName("PK_HangFire_List");

                entity.ToTable("List", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_List_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.Property(e => e.AmountGranted).HasColumnType("money");

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.InstalmentAmount).HasColumnType("money");

                entity.Property(e => e.InterestRate).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.PaybackDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentAmount).HasColumnType("money");

                entity.Property(e => e.RealPaymentDate).HasColumnType("datetime");

                entity.Property(e => e.StatusChangeDate).HasColumnType("datetime");

                entity.Property(e => e.WayOfPay).HasMaxLength(50);

                entity.HasOne(d => d.DecisionEmployee)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.DecisionEmployeeId)
                    .HasConstraintName("FK_Loans_Employees");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Loans_AspNetUsers");

                entity.HasOne(d => d.LoanPayback)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.LoanPaybackId)
                    .HasConstraintName("FK_Loans_LoanPaybacks");

                entity.HasOne(d => d.LoanRequest)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.LoanRequestId)
                    .HasConstraintName("FK_Loans_LoanRequests");

                entity.HasOne(d => d.LoanStatus)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.LoanStatusId)
                    .HasConstraintName("FK_Loans_LoanStatuses");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Loans_Vehicles");
            });

            modelBuilder.Entity<LoanPayback>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.LoanPaybacks)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_LoanPaybacks_AspNetUsers");
            });

            modelBuilder.Entity<LoanPayment>(entity =>
            {
                entity.Property(e => e.Ammount).HasColumnType("money");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.LoanPayments)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_LoanPayments_AspNetUsers");

                entity.HasOne(d => d.Loan)
                    .WithMany(p => p.LoanPayments)
                    .HasForeignKey(d => d.LoanId)
                    .HasConstraintName("FK_LoanPayments_Loans");
            });

            modelBuilder.Entity<LoanPurpose>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.LoanPurposes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_LoanPurposes_AspNetUsers");
            });

            modelBuilder.Entity<LoanRequest>(entity =>
            {
                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestAmount).HasColumnType("money");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.LoanRequests)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_LoanRequests_DeletedBy_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LoanRequests)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoanRequests_Employees");

                entity.HasOne(d => d.LoanPurpose)
                    .WithMany(p => p.LoanRequests)
                    .HasForeignKey(d => d.LoanPurposeId)
                    .HasConstraintName("FK_LoanRequests_LoanPurposes");
            });

            modelBuilder.Entity<LoanStatus>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.LoanStatuses)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_LoanStatuses_AspNetUsers");
            });

            modelBuilder.Entity<MaintenanceGroup>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MaintenanceGroups)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_MaintenanceGroups_AspNetUsers");
            });

            modelBuilder.Entity<MaintenancePlan>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.NextServiceDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MaintenancePlans)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_MaintenancePlans_AspNetUsers");

                entity.HasOne(d => d.MaintenanceGroup)
                    .WithMany(p => p.MaintenancePlans)
                    .HasForeignKey(d => d.MaintenanceGroupId)
                    .HasConstraintName("FK_MaintenancePlans_MaintenanceGroups");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.MaintenancePlans)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .HasConstraintName("FK_MaintenancePlans_ServiceTypes");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.MaintenancePlans)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaintenancePlans_Vehicles");
            });

            modelBuilder.Entity<MaintenanceService>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MaintenanceServices)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_MaintenanceServices_AspNetUsers");

                entity.HasOne(d => d.MeasureUnit)
                    .WithMany(p => p.MaintenanceServices)
                    .HasForeignKey(d => d.MeasureUnitId)
                    .HasConstraintName("FK_MaintenanceServices_MeasureUnits");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.MaintenanceServices)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .HasConstraintName("FK_MaintenanceServices_ServiceTypes");
            });

            modelBuilder.Entity<MeasureUnit>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Sign).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MeasureUnits)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_MeasureUnits_AspNetUsers");

                entity.HasOne(d => d.MeasureUnitType)
                    .WithMany(p => p.MeasureUnits)
                    .HasForeignKey(d => d.MeasureUnitTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Measure_MeasureType1");
            });

            modelBuilder.Entity<MeasureUnitType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.MeasureUnitTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_MeasureUnitTypes_AspNetUsers");
            });

            modelBuilder.Entity<NoticeType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.NoticeTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_NoticeTypes_AspNetUsers");
            });

            modelBuilder.Entity<OrgUnit>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.OrgUnits)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrgUnit_Company");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.OrgUnits)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_OrgUnits_AspNetUsers");

                entity.HasOne(d => d.ParentOrgUnit)
                    .WithMany(p => p.InverseParentOrgUnit)
                    .HasForeignKey(d => d.ParentOrgUnitId)
                    .HasConstraintName("FK_OrgUnit_OrgUnit");
            });

            modelBuilder.Entity<PalletJack>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.ProductionYear).HasColumnType("date");

                entity.Property(e => e.SerialNumber).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.PalletJacks)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_PalletJacks_AspNetUsers");
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.Property(e => e.AccountingCode).HasMaxLength(50);

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.BankAccountNumber).HasMaxLength(50);

                entity.Property(e => e.BlockedComment).HasMaxLength(500);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.ContactPerson).HasMaxLength(100);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DisclaimerNote).HasMaxLength(2000);

                entity.Property(e => e.Dot)
                    .HasMaxLength(50)
                    .HasColumnName("DOT");

                entity.Property(e => e.Ein)
                    .HasMaxLength(50)
                    .HasColumnName("EIN");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("EMail");

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.Mc)
                    .HasMaxLength(50)
                    .HasColumnName("MC");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.RautingNumber).HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Partner_City");

                entity.HasOne(d => d.ContactCompany)
                    .WithMany(p => p.PartnerContactCompanies)
                    .HasForeignKey(d => d.ContactCompanyId)
                    .HasConstraintName("FK_Partners_Contacts1");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Partners_AspNetUsers");

                entity.HasOne(d => d.IncomeType)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.IncomeTypeId)
                    .HasConstraintName("FK_Partners_CostIncomeTypes");

                entity.HasOne(d => d.PartnerType)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.PartnerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Partner_PartnerType");

                entity.HasOne(d => d.PaymentCondition)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.PaymentConditionId)
                    .HasConstraintName("FK_Partner_PaymentCondition");

                entity.HasOne(d => d.PrimaryContact)
                    .WithMany(p => p.PartnerPrimaryContacts)
                    .HasForeignKey(d => d.PrimaryContactId)
                    .HasConstraintName("FK_Partners_Contacts");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Partner_State");

                entity.HasOne(d => d.ZipCode)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.ZipCodeId)
                    .HasConstraintName("FK_Partner_ZipCode");
            });

            modelBuilder.Entity<PartnerCategory>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.PartnerCategories)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_PartnerCategories_AspNetUsers");
            });

            modelBuilder.Entity<PartnerType>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.PartnerTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_PartnerTypes_AspNetUsers");

                entity.HasOne(d => d.PartnerCategory)
                    .WithMany(p => p.PartnerTypes)
                    .HasForeignKey(d => d.PartnerCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartnerType_PartnerCategory");
            });

            modelBuilder.Entity<PassStatus>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.PassStatuses)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_IPassStatuses_AspNetUsers1");
            });

            modelBuilder.Entity<PaymentCondition>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.PaymentConditions)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_PaymentConditions_AspNetUsers");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.PaymentMethods)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_PaymentMethods_AspNetUsers");
            });

            modelBuilder.Entity<PtisMissingFixed>(entity =>
            {
                entity.ToTable("PTIsMissingFixeds");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.PtisMissingFixeds)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_PTIsMissingFixed_AspNetUsers");
            });

            modelBuilder.Entity<ReimbursmentDetentionType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.ReimbursmentDetentionTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_ReimbursmentDetentionTypes_AspNetUsers");
            });

            modelBuilder.Entity<Relationship>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Relationships)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Relationships_AspNetUsers");
            });

            modelBuilder.Entity<SafetyTest>(entity =>
            {
                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.SafetyTests)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_SafetyTests_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SafetyTests)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SafetyTests_Employees");

                entity.HasOne(d => d.SafetyType)
                    .WithMany(p => p.SafetyTests)
                    .HasForeignKey(d => d.SafetyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SafetyTests_SafetyTypes");
            });

            modelBuilder.Entity<SafetyType>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.SafetyTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_SafetyTypes_AspNetUsers");
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK_HangFire_Schema");

                entity.ToTable("Schema", "HangFire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "HangFire");

                entity.HasIndex(e => e.LastHeartbeat, "IX_HangFire_Server_LastHeartbeat");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<ServiceDocument>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DotinspectionId).HasColumnName("DOTInspectionId");

                entity.Property(e => e.ExternalNumber).HasMaxLength(50);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.Number).HasMaxLength(50);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.ServiceDocuments)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_ServiceDocuments_AspNetUsers");

                entity.HasOne(d => d.Dotinspection)
                    .WithMany(p => p.ServiceDocuments)
                    .HasForeignKey(d => d.DotinspectionId)
                    .HasConstraintName("FK_ServiceDocuments_VehicleAnnualDOTInspections");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.ServiceDocuments)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .HasConstraintName("FK_ServiceDocuments_PaymentMethods");

                entity.HasOne(d => d.ServiceStatus)
                    .WithMany(p => p.ServiceDocuments)
                    .HasForeignKey(d => d.ServiceStatusId)
                    .HasConstraintName("FK_ServiceDocuments_ServiceStatuses");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ServiceDocuments)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_ServiceDocuments_Partners");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.ServiceDocuments)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceDocuments_Vehicles");
            });

            modelBuilder.Entity<ServiceItem>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.ServicePart).HasColumnName("Service_Part");

                entity.Property(e => e.SinglePrice).HasColumnType("money");

                entity.Property(e => e.Value).HasColumnType("money");

                entity.HasOne(d => d.DefectReason)
                    .WithMany(p => p.ServiceItems)
                    .HasForeignKey(d => d.DefectReasonId)
                    .HasConstraintName("FK_ServiceItems_DefectReasons");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.ServiceItems)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_ServiceItems_AspNetUsers");

                entity.HasOne(d => d.MaintenanceGroup)
                    .WithMany(p => p.ServiceItems)
                    .HasForeignKey(d => d.MaintenanceGroupId)
                    .HasConstraintName("FK_ServiceItems_MaintenanceGroups");

                entity.HasOne(d => d.MaintenanceService)
                    .WithMany(p => p.ServiceItems)
                    .HasForeignKey(d => d.MaintenanceServiceId)
                    .HasConstraintName("FK_ServiceItems_MaintenanceServices");

                entity.HasOne(d => d.MeasureUnit)
                    .WithMany(p => p.ServiceItems)
                    .HasForeignKey(d => d.MeasureUnitId)
                    .HasConstraintName("FK_ServiceItems_MeasureUnits");

                entity.HasOne(d => d.ServiceDocument)
                    .WithMany(p => p.ServiceItems)
                    .HasForeignKey(d => d.ServiceDocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceItems_ServiceDocuments");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.ServiceItems)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .HasConstraintName("FK_ServiceItems_ServiceTypes");

                entity.HasOne(d => d.SparePart)
                    .WithMany(p => p.ServiceItems)
                    .HasForeignKey(d => d.SparePartId)
                    .HasConstraintName("FK_ServiceItems_SpareParts");
            });

            modelBuilder.Entity<ServiceStatus>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.ServiceStatuses)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_ServiceStatuses_AspNetUsers");
            });

            modelBuilder.Entity<ServiceType>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.ServiceTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_ServiceTypes_AspNetUsers");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Value })
                    .HasName("PK_HangFire_Set");

                entity.ToTable("Set", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Set_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.HasIndex(e => new { e.Key, e.Score }, "IX_HangFire_Set_Score");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(256);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<SparePart>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.SpareParts)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_SpareParts_AspNetUsers");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShortName)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_States_AspNetUsers");
            });

            modelBuilder.Entity<State1>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Id })
                    .HasName("PK_HangFire_State");

                entity.ToTable("State", "HangFire");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.State1s)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_State_Job");
            });

            modelBuilder.Entity<Statement>(entity =>
            {
                entity.Property(e => e.AccountingDate).HasColumnType("datetime");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.StatementNumber).HasMaxLength(50);

                entity.HasOne(d => d.AccountingPeriod)
                    .WithMany(p => p.Statements)
                    .HasForeignKey(d => d.AccountingPeriodId)
                    .HasConstraintName("FK_Statements_AccountingPeriods");

                entity.HasOne(d => d.AccountingUser)
                    .WithMany(p => p.StatementAccountingUsers)
                    .HasForeignKey(d => d.AccountingUserId)
                    .HasConstraintName("FK_Statements_AspNetUsers");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Statements)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Statements_Companies");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.StatementDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Statements_AspNetUsers1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Statements)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Statements_Employees");

                entity.HasOne(d => d.StatementStatus)
                    .WithMany(p => p.Statements)
                    .HasForeignKey(d => d.StatementStatusId)
                    .HasConstraintName("FK_Statements_InvoiceStatuses");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Statements)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Statements_Vehicles");
            });

            modelBuilder.Entity<StatementCredit>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Credit)
                    .WithMany(p => p.StatementCredits)
                    .HasForeignKey(d => d.CreditId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementCredits_Credits");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.StatementCredits)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_StatementCredits_AspNetUsers");

                entity.HasOne(d => d.Statement)
                    .WithMany(p => p.StatementCredits)
                    .HasForeignKey(d => d.StatementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementCredits_Statements");
            });

            modelBuilder.Entity<StatementDeduction>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Deduction)
                    .WithMany(p => p.StatementDeductions)
                    .HasForeignKey(d => d.DeductionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementDeductions_Deductions");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.StatementDeductions)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_StatementDeductions_AspNetUsers");

                entity.HasOne(d => d.Statement)
                    .WithMany(p => p.StatementDeductions)
                    .HasForeignKey(d => d.StatementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementDeductions_Statements");
            });

            modelBuilder.Entity<StatementFuelConsumption>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.StatementFuelConsumptions)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_StatementFuelConsumptions_AspNetUsers");

                entity.HasOne(d => d.FuelConsumption)
                    .WithMany(p => p.StatementFuelConsumptions)
                    .HasForeignKey(d => d.FuelConsumptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementFuelConsumptions_FuelConsumptions");

                entity.HasOne(d => d.Statement)
                    .WithMany(p => p.StatementFuelConsumptions)
                    .HasForeignKey(d => d.StatementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementFuelConsumptions_Statements");
            });

            modelBuilder.Entity<StatementToll>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.StatementTolls)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_StatementTolls_AspNetUsers");

                entity.HasOne(d => d.Statement)
                    .WithMany(p => p.StatementTolls)
                    .HasForeignKey(d => d.StatementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementTolls_Statements");

                entity.HasOne(d => d.Toll)
                    .WithMany(p => p.StatementTolls)
                    .HasForeignKey(d => d.TollId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementTolls_Tolls");
            });

            modelBuilder.Entity<StatementTour>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.StatementTours)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_StatementTours_AspNetUsers");

                entity.HasOne(d => d.Statement)
                    .WithMany(p => p.StatementTours)
                    .HasForeignKey(d => d.StatementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementTours_Statements");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.StatementTours)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementTours_Tours");
            });

            modelBuilder.Entity<SubcontractorFee>(entity =>
            {
                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Fee).HasColumnType("smallmoney");

                entity.Property(e => e.RecordDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.SubcontractorFees)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_SubcontractorFees_Company");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.SubcontractorFees)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_SubcontractorFees_AspNetUsers");

                entity.HasOne(d => d.SubcontractorCompany)
                    .WithMany(p => p.SubcontractorFees)
                    .HasForeignKey(d => d.SubcontractorCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubcontractorFees_SubcontractorPartners");
            });

            modelBuilder.Entity<TimeZone>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ShortName)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TimeZones)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_TimeZones_DeletedBy");
            });

            modelBuilder.Entity<Toll>(entity =>
            {
                entity.Property(e => e.Agency).HasMaxLength(200);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(200);

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.TransactionType).HasMaxLength(100);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Tolls)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Tolls_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Tolls)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Tolls_Employees");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Tolls)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Tolls_Vehicles");

                entity.HasOne(d => d.VehiclePass)
                    .WithMany(p => p.Tolls)
                    .HasForeignKey(d => d.VehiclePassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tolls_VehiclePasses");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.Property(e => e.BaseAmountForTheDriver)
                    .HasColumnType("money")
                    .HasComment(" The amount of the base for calculating the variable part to your own driver (if it is not per mile traveled, which is charged as a % of the tour value)");

                entity.Property(e => e.BillableMileage).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CargoHeight).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CargoLength).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CargoVolume).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CargoWeight).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CommissionRate)
                    .HasColumnType("money")
                    .HasComment("The amount of the tour finding fee and the Amount remaining after the fee is deducted (in cases where a commission is applied to the value of the tour to obtain the amount for the carrier, i.e. the amount retained for the tour finding). A % of the tenant/subcontractor adjustment applies.");

                entity.Property(e => e.CompanyId).HasComment("Ko je ugovorio turu (kompanija iz grupe) ");

                entity.Property(e => e.ContractAmount).HasColumnType("money");

                entity.Property(e => e.ContractDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DrivingHoursEstimated).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.EstimatedMileage).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ExternalNo).HasMaxLength(50);

                entity.Property(e => e.FreightRate).HasColumnType("money");

                entity.Property(e => e.InvoiceAmount).HasColumnType("money");

                entity.Property(e => e.InvoiceCompanyId).HasComment("Ko će biti nosilac fakture ka kupcu (kompanija iz grupe)");

                entity.Property(e => e.TourNumber).HasMaxLength(50);

                entity.Property(e => e.TransportationCompanyId).HasComment("Ko je vozio turu (kompanija iz grupe)");

                entity.Property(e => e.TripAmountSubcontractor)
                    .HasColumnType("money")
                    .HasComment("Cena koja se prijavljuje subcontractory");

                entity.HasOne(d => d.BrokerContact)
                    .WithMany(p => p.TourBrokerContacts)
                    .HasForeignKey(d => d.BrokerContactId)
                    .HasConstraintName("FK_Tours_BrokerContacts");

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.TourBrokers)
                    .HasForeignKey(d => d.BrokerId)
                    .HasConstraintName("FK_Tour_BrokerPartners");

                entity.HasOne(d => d.CalculationType)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.CalculationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tour_TourCalculationType");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.TourCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tours_Company");

                entity.HasOne(d => d.CustomerCompany)
                    .WithMany(p => p.TourCustomerCompanies)
                    .HasForeignKey(d => d.CustomerCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tour_CustomerPartners");

                entity.HasOne(d => d.CustomerContact)
                    .WithMany(p => p.TourCustomerContacts)
                    .HasForeignKey(d => d.CustomerContactId)
                    .HasConstraintName("FK_Tour_CustomerContacts");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Tours_AspNetUsers");

                entity.HasOne(d => d.FactoringCompany)
                    .WithMany(p => p.TourFactoringCompanies)
                    .HasForeignKey(d => d.FactoringCompanyId)
                    .HasConstraintName("FK_Tour_FactoringPartners");

                entity.HasOne(d => d.InvoiceCompany)
                    .WithMany(p => p.TourInvoiceCompanies)
                    .HasForeignKey(d => d.InvoiceCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tours_InvoiceCompany");

                entity.HasOne(d => d.PaymentCondition)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.PaymentConditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tour_PaymentConditions");

                entity.HasOne(d => d.SplittedFromTour)
                    .WithMany(p => p.InverseSplittedFromTour)
                    .HasForeignKey(d => d.SplittedFromTourId)
                    .HasConstraintName("FK_Tours_SplitTour");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tour_TourStatus");

                entity.HasOne(d => d.SubcontractorCompany)
                    .WithMany(p => p.TourSubcontractorCompanies)
                    .HasForeignKey(d => d.SubcontractorCompanyId)
                    .HasConstraintName("FK_Tour_SubcontractorPartners");

                entity.HasOne(d => d.TransportationCompany)
                    .WithMany(p => p.TourTransportationCompanies)
                    .HasForeignKey(d => d.TransportationCompanyId)
                    .HasConstraintName("FK_Tours_TransportationCompany");
            });

            modelBuilder.Entity<TourAccounting>(entity =>
            {
                entity.Property(e => e.AccountingDate).HasColumnType("datetime");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.HasOne(d => d.AccountingUser)
                    .WithMany(p => p.TourAccountingAccountingUsers)
                    .HasForeignKey(d => d.AccountingUserId)
                    .HasConstraintName("FK_TourAccountings_AspNetUsers");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TourAccountingDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_TourAccountings_AspNetUsers1");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourAccountings)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK_TourAccountings_TourAccountings");
            });

            modelBuilder.Entity<TourCalculationType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TourCalculationTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_TourCalculationTypes_AspNetUsers");
            });

            modelBuilder.Entity<TourClaim>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.Property(e => e.RecordDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TourClaims)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_TourClaims_DeletedBy");

                entity.HasOne(d => d.Point)
                    .WithMany(p => p.TourClaims)
                    .HasForeignKey(d => d.PointId)
                    .HasConstraintName("FK_TourClaims_TourPoints");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourClaims)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK_TourClaims_Tours");
            });

            modelBuilder.Entity<TourCost>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.ReceipeNumber).HasMaxLength(50);

                entity.HasOne(d => d.CostIncomeType)
                    .WithMany(p => p.TourCosts)
                    .HasForeignKey(d => d.CostIncomeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourCosts_CostIncomeTypes");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TourCostDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_TourCosts_DeletedBy_AspNetUsers");

                entity.HasOne(d => d.SubmittedByNavigation)
                    .WithMany(p => p.TourCostSubmittedByNavigations)
                    .HasForeignKey(d => d.SubmittedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourCosts_SubmittedBy_AspNetUsers");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourCosts)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourCosts_Tours");
            });

            modelBuilder.Entity<TourCrew>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.EmptyMiles).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.LoadedMiles).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.CoDriver)
                    .WithMany(p => p.TourCrewCoDrivers)
                    .HasForeignKey(d => d.CoDriverId)
                    .HasConstraintName("FK_TourCrews_CoDriver");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TourCrews)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_TourCrews_AspNetUsers");

                entity.HasOne(d => d.Dispatcher)
                    .WithMany(p => p.TourCrewDispatchers)
                    .HasForeignKey(d => d.DispatcherId)
                    .HasConstraintName("FK_TourCrews_Dispatcher");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.TourCrewDrivers)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourCrews_Driver");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourCrews)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourCrews_Tour");

                entity.HasOne(d => d.Trailer)
                    .WithMany(p => p.TourCrewTrailers)
                    .HasForeignKey(d => d.TrailerId)
                    .HasConstraintName("FK_TourCrews_Vehicles");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TourCrews)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourCrews_TourCrewTypes");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.TourCrewVehicles)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourCrews_Vehicle");
            });

            modelBuilder.Entity<TourCrewType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TourCrewTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_TourCrewTypes_AspNetUsers");
            });

            modelBuilder.Entity<TourPoint>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.CompanyName).HasMaxLength(64);

                entity.Property(e => e.ContactName).HasMaxLength(128);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RecordDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TimeFrom).HasColumnType("datetime");

                entity.Property(e => e.TimeTo).HasColumnType("datetime");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.TourPoints)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourPoints_Cities");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TourPoints)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_TourPoints_AspNetUsers");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.TourPoints)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourPoints_States");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TourPoints)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourPoints_TourStatuses");

                entity.HasOne(d => d.TimeZone)
                    .WithMany(p => p.TourPoints)
                    .HasForeignKey(d => d.TimeZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourPoints_TimeZones");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourPoints)
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourPoints_Tours");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TourPoints)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourPoints_TourPointTypes");
            });

            modelBuilder.Entity<TourPointSpecialInstruction>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Instruction).HasMaxLength(512);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TourPointSpecialInstructions)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_TourPointSpecialInstructions_DeletedBy");

                entity.HasOne(d => d.TourPoint)
                    .WithMany(p => p.TourPointSpecialInstructions)
                    .HasForeignKey(d => d.TourPointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TourPointSpecialInstructions_TourPoints");
            });

            modelBuilder.Entity<TourPointType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TourPointTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_TourPointTypes_AspNetUsers");
            });

            modelBuilder.Entity<TourReimbursmentsDetention>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Value).HasColumnType("money");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TourReimbursmentsDetentions)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_TourReimbursmentsDetentions_AspNetUsers");

                entity.HasOne(d => d.ReimbursmentDetentionType)
                    .WithMany(p => p.TourReimbursmentsDetentions)
                    .HasForeignKey(d => d.ReimbursmentDetentionTypeId)
                    .HasConstraintName("FK_TourReimbursmentsDetentions_ReimbursmentDetentionTypes");
            });

            modelBuilder.Entity<TourStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.TourStatuses)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_TourStatuses_AspNetUsers");
            });

            modelBuilder.Entity<UnidentifiedDrivingsAssigned>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.UnidentifiedDrivingsAssigneds)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_UnidentifiedDrivingsAssigned_AspNetUsers");
            });

            modelBuilder.Entity<UserMessage>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Message).HasMaxLength(4000);

                entity.HasOne(d => d.UserFromNavigation)
                    .WithMany(p => p.UserMessageUserFromNavigations)
                    .HasForeignKey(d => d.UserFrom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserMessages_AspNetUsersFrom");

                entity.HasOne(d => d.UserToNavigation)
                    .WithMany(p => p.UserMessageUserToNavigations)
                    .HasForeignKey(d => d.UserTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserMessages_AspNetUsersTo");
            });

            modelBuilder.Entity<UserSetting>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SettingsKey });

                entity.Property(e => e.SettingsKey)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.StringValue).HasMaxLength(2048);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.UserSettingDeletedByNavigations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_UserSettings_DeletedBy_AspNetUsers");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSettingUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSettings_AspNetUsers");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasIndex(e => e.InternalNumber, "IX_Vehicle_InternalNumber")
                    .IsUnique();

                entity.Property(e => e.AcquisitionDate).HasColumnType("date");

                entity.Property(e => e.BoxSize).HasMaxLength(100);

                entity.Property(e => e.ChassisNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Color).HasMaxLength(20);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.EngineType).HasMaxLength(50);

                entity.Property(e => e.FirstRegistrationYear).HasColumnType("date");

                entity.Property(e => e.FuelType).HasComment("1 - Diesel, 2 - Petrol");

                entity.Property(e => e.InternalNumber).HasMaxLength(50);

                entity.Property(e => e.LicensePlate).HasMaxLength(20);

                entity.Property(e => e.OilType).HasMaxLength(50);

                entity.Property(e => e.PlatesExpirationDate).HasColumnType("date");

                entity.Property(e => e.SaleDate).HasColumnType("date");

                entity.Property(e => e.TyreSizeDriveAxel).HasMaxLength(50);

                entity.Property(e => e.TyreSizeSteeringAxel).HasMaxLength(50);

                entity.Property(e => e.TyreSizeTrailerAxel).HasMaxLength(50);

                entity.Property(e => e.VehicleNote).HasMaxLength(2000);

                entity.Property(e => e.VehicleYear).HasColumnType("date");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_Companies");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Vehicles_AspNetUsers");

                entity.HasOne(d => d.OrgUnit)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.OrgUnitId)
                    .HasConstraintName("FK_Vehicles_OrgUnits");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_Vehicles_Partners");

                entity.HasOne(d => d.OwnershipType)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.OwnershipTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_VehicleOwnershipTypes");

                entity.HasOne(d => d.VehicleModel)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicles_VehicleModels");
            });

            modelBuilder.Entity<VehicleAdvanceCost>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DateFrom).HasColumnType("date");

                entity.Property(e => e.DateTo).HasColumnType("date");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.InsuranceValue).HasColumnType("money");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.StatementNumber).HasMaxLength(50);

                entity.HasOne(d => d.AccountingPeriod)
                    .WithMany(p => p.VehicleAdvanceCosts)
                    .HasForeignKey(d => d.AccountingPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAdvanceCosts_AccountingPeriods");

                entity.HasOne(d => d.CalculationType)
                    .WithMany(p => p.VehicleAdvanceCosts)
                    .HasForeignKey(d => d.CalculationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAdvanceCosts_TourCalculationTypes");

                entity.HasOne(d => d.CalculationTypeNavigation)
                    .WithMany(p => p.VehicleAdvanceCosts)
                    .HasForeignKey(d => d.CalculationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAdvanceCosts_VehicleCostAndIncomeCalculationTypes");

                entity.HasOne(d => d.CostType)
                    .WithMany(p => p.VehicleAdvanceCosts)
                    .HasForeignKey(d => d.CostTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAdvanceCosts_CostIncomeTypes");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleAdvanceCosts)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleAdvanceCosts_AspNetUsers");

                entity.HasOne(d => d.PayedByDriver)
                    .WithMany(p => p.VehicleAdvanceCosts)
                    .HasForeignKey(d => d.PayedByDriverId)
                    .HasConstraintName("FK_VehicleAdvanceCosts_Employees");

                entity.HasOne(d => d.PayedByPartner)
                    .WithMany(p => p.VehicleAdvanceCosts)
                    .HasForeignKey(d => d.PayedByPartnerId)
                    .HasConstraintName("FK_VehicleAdvanceCosts_Partners");

                entity.HasOne(d => d.PayerType)
                    .WithMany(p => p.VehicleAdvanceCosts)
                    .HasForeignKey(d => d.PayerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAdvanceCosts_VehicleCostPayerTypes");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleAdvanceCosts)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAdvanceCosts_Vehicles");
            });

            modelBuilder.Entity<VehicleAndEquipmentHistory>(entity =>
            {
                entity.ToTable("VehicleAndEquipmentHistory");

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleAndEquipmentHistories)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleAndPalletJacksHistory_AspNetUsers");

                entity.HasOne(d => d.EquipmentType)
                    .WithMany(p => p.VehicleAndEquipmentHistories)
                    .HasForeignKey(d => d.EquipmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAndEquipmentHistory_EquipmentTypes");

                entity.HasOne(d => d.GenericEquipment)
                    .WithMany(p => p.VehicleAndEquipmentHistories)
                    .HasForeignKey(d => d.GenericEquipmentId)
                    .HasConstraintName("FK_VehicleAndEquipmentHistory_Equipments");

                entity.HasOne(d => d.PalletJack)
                    .WithMany(p => p.VehicleAndEquipmentHistories)
                    .HasForeignKey(d => d.PalletJackId)
                    .HasConstraintName("FK_VehicleAndPalletJacksHistory_PalletJacks");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleAndEquipmentHistories)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAndPalletJacksHistory_Vehicles");
            });

            modelBuilder.Entity<VehicleAndTrailersHistory>(entity =>
            {
                entity.ToTable("VehicleAndTrailersHistory");

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleAndTrailersHistories)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleAndTrailersHistory_AspNetUsers");

                entity.HasOne(d => d.Trailer)
                    .WithMany(p => p.VehicleAndTrailersHistoryTrailers)
                    .HasForeignKey(d => d.TrailerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAndTrailersHistory_Vehicles_Trailers");

                entity.HasOne(d => d.Truck)
                    .WithMany(p => p.VehicleAndTrailersHistoryTrucks)
                    .HasForeignKey(d => d.TruckId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAndTrailersHistory_Vehicles_Trucks");
            });

            modelBuilder.Entity<VehicleAnnualDotinspection>(entity =>
            {
                entity.ToTable("VehicleAnnualDOTInspections");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.InspectionDate).HasColumnType("datetime");

                entity.Property(e => e.NextInspectionDate).HasColumnType("datetime");

                entity.Property(e => e.NextPreventiveControlDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleAnnualDotinspections)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleAnnualDOTInspections_AspNetUsers");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleAnnualDotinspections)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAnnualDOTInspections_Vehicles");
            });

            modelBuilder.Entity<VehicleAssignment>(entity =>
            {
                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleAssignments)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VVehicleAssignments_DeletedBy");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.VehicleAssignments)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAssignments_Employees");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleAssignments)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAssignments_Vehicles");
            });

            modelBuilder.Entity<VehicleCostAndIncomeCalculationType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleCostAndIncomeCalculationTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleCostAndIncomeCalculationTypes_AspNetUsers");
            });

            modelBuilder.Entity<VehicleCostPayerType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleCostPayerTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleCostPayerTypes_AspNetUsers");
            });

            modelBuilder.Entity<VehicleEldDatum>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Odometer).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.VehicleEldhistoryId).HasColumnName("VehicleELDHistoryId");

                entity.Property(e => e.WorkingHours).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleEldData)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleELDData_AspNetUsers");

                entity.HasOne(d => d.VehicleEldhistory)
                    .WithMany(p => p.VehicleEldData)
                    .HasForeignKey(d => d.VehicleEldhistoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleELDData_VehicleELDHistory");
            });

            modelBuilder.Entity<VehicleEldHistory>(entity =>
            {
                entity.ToTable("VehicleEldHistory");

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Eldnumber)
                    .HasMaxLength(50)
                    .HasColumnName("ELDNumber");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.VehicleEldHistories)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_VehicleELDHistory_Companies");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleEldHistories)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleELDHistory_AspNetUsers");

                entity.HasOne(d => d.EldNote)
                    .WithMany(p => p.VehicleEldHistories)
                    .HasForeignKey(d => d.EldNoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleELDHistory_ELDDisconnectedNotes");

                entity.HasOne(d => d.EldPartner)
                    .WithMany(p => p.VehicleEldHistories)
                    .HasForeignKey(d => d.EldPartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleELDHistory_Partners");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleEldHistories)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleELDHistory_Vehicles");
            });

            modelBuilder.Entity<VehicleFmcsaViolation>(entity =>
            {
                entity.HasIndex(e => e.Code, "IX_VehicleFmcsaViolations")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.GroupDescription)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SubGroupDescription)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleFmcsaViolations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleFmcsaViolations_AspNetUsers");
            });

            modelBuilder.Entity<VehicleFmcsainspection>(entity =>
            {
                entity.ToTable("VehicleFMCSAInspections");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.Property(e => e.PenaltyAmount).HasColumnType("money");

                entity.Property(e => e.ReportNumber).HasMaxLength(50);

                entity.Property(e => e.SentDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleFmcsainspections)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleFMCSAInspections_AspNetUsers");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.VehicleFmcsainspections)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleFMCSAInspections_Driver");

                entity.HasOne(d => d.InspectionType)
                    .WithMany(p => p.VehicleFmcsainspections)
                    .HasForeignKey(d => d.InspectionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleFMCSAInspections_VehicleFMCSAInspectionTypes");

                entity.HasOne(d => d.PenaltyType)
                    .WithMany(p => p.VehicleFmcsainspections)
                    .HasForeignKey(d => d.PenaltyTypeId)
                    .HasConstraintName("FK_VehicleFMCSAInspections_VehicleFMCSAInspectionPenaltyTypes");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleFmcsainspections)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleFMCSAInspections_Vehicles");

                entity.HasOne(d => d.Violation)
                    .WithMany(p => p.VehicleFmcsainspections)
                    .HasForeignKey(d => d.ViolationId)
                    .HasConstraintName("FK_VehicleFMCSAInspections_VehicleFmcsaViolations");
            });

            modelBuilder.Entity<VehicleFmcsainspectionPenaltyType>(entity =>
            {
                entity.ToTable("VehicleFMCSAInspectionPenaltyTypes");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleFmcsainspectionPenaltyTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleFMCSAInspectionPenaltyTypes_AspNetUsers");
            });

            modelBuilder.Entity<VehicleFmcsainspectionType>(entity =>
            {
                entity.ToTable("VehicleFMCSAInspectionTypes");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleFmcsainspectionTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleFMCSAInspectionTypes_AspNetUsers");
            });

            modelBuilder.Entity<VehicleIftasetting>(entity =>
            {
                entity.ToTable("VehicleIFTASettings");

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.DecalNo).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.LicenseNo).HasMaxLength(50);

                entity.Property(e => e.QuarterlyAmount).HasColumnType("money");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleIftasettings)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleIFTASettings_AspNetUsers");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleIftasettings)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleIFTASettings_Vehicles");
            });

            modelBuilder.Entity<VehicleIncome>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.AmountCharged).HasColumnType("money");

                entity.Property(e => e.DateFrom).HasColumnType("date");

                entity.Property(e => e.DateTo).HasColumnType("date");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceNumber).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.Property(e => e.StatementNumber).HasMaxLength(50);

                entity.HasOne(d => d.AccountingPeriod)
                    .WithMany(p => p.VehicleIncomes)
                    .HasForeignKey(d => d.AccountingPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleIncomes_VehicleIncomes");

                entity.HasOne(d => d.CalculationType)
                    .WithMany(p => p.VehicleIncomes)
                    .HasForeignKey(d => d.CalculationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleIncomes_VehicleCostAndIncomeCalculationTypes");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleIncomes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleIncomes_AspNetUsers");

                entity.HasOne(d => d.IncomeType)
                    .WithMany(p => p.VehicleIncomes)
                    .HasForeignKey(d => d.IncomeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleIncomes_CostIncomeTypes");

                entity.HasOne(d => d.PayedByDriver)
                    .WithMany(p => p.VehicleIncomes)
                    .HasForeignKey(d => d.PayedByDriverId)
                    .HasConstraintName("FK_VehicleIncomes_Employees");

                entity.HasOne(d => d.PayedByLesseePartner)
                    .WithMany(p => p.VehicleIncomes)
                    .HasForeignKey(d => d.PayedByLesseePartnerId)
                    .HasConstraintName("FK_VehicleIncomes_Partners");

                entity.HasOne(d => d.PayerType)
                    .WithMany(p => p.VehicleIncomes)
                    .HasForeignKey(d => d.PayerTypeId)
                    .HasConstraintName("FK_VehicleIncomes_VehicleCostPayerTypes");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleIncomes)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleIncomes_Vehicles");
            });

            modelBuilder.Entity<VehicleMaintenanceCost>(entity =>
            {
                entity.Property(e => e.CostInterest).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.HasOne(d => d.Cost)
                    .WithMany(p => p.VehicleMaintenanceCosts)
                    .HasForeignKey(d => d.CostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleMaintenanceCosts_CostIncomeTypes");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleMaintenanceCosts)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleMaintenanceCosts_AspNetUsers");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.VehicleMaintenanceCosts)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleMaintenanceCosts_Partners");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleMaintenanceCosts)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleMaintenanceCosts_Vehicles");
            });

            modelBuilder.Entity<VehicleManufacturer>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleManufacturers)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleManufacturers_AspNetUsers");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.VehicleManufacturers)
                    .HasForeignKey(d => d.VehicleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleManufacturers_VehicleTypes");
            });

            modelBuilder.Entity<VehicleModel>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.Model).HasMaxLength(100);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleModels)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleModels_AspNetUsers");

                entity.HasOne(d => d.VehicleManufacturer)
                    .WithMany(p => p.VehicleModels)
                    .HasForeignKey(d => d.VehicleManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleModel_VehicleManufacturer");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.VehicleModels)
                    .HasForeignKey(d => d.VehicleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleModel_VehicleType");
            });

            modelBuilder.Entity<VehicleOptionalCost>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DateFrom).HasColumnType("date");

                entity.Property(e => e.DateTo).HasColumnType("date");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.StatementNumber).HasMaxLength(50);

                entity.HasOne(d => d.AccountingPeriod)
                    .WithMany(p => p.VehicleOptionalCosts)
                    .HasForeignKey(d => d.AccountingPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleOptionalCosts_AccountingPeriodTypes");

                entity.HasOne(d => d.CalculationType)
                    .WithMany(p => p.VehicleOptionalCosts)
                    .HasForeignKey(d => d.CalculationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleOptionalCosts_CalculationTypes");

                entity.HasOne(d => d.CostType)
                    .WithMany(p => p.VehicleOptionalCosts)
                    .HasForeignKey(d => d.CostTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleOptionalCosts_CostIncomeTypes");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleOptionalCosts)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleOptionalCosts_AspNetUsers");

                entity.HasOne(d => d.PayedByDriver)
                    .WithMany(p => p.VehicleOptionalCosts)
                    .HasForeignKey(d => d.PayedByDriverId)
                    .HasConstraintName("FK_VehicleOptionalCosts_Employees");

                entity.HasOne(d => d.PayedByPartner)
                    .WithMany(p => p.VehicleOptionalCosts)
                    .HasForeignKey(d => d.PayedByPartnerId)
                    .HasConstraintName("FK_VehicleOptionalCosts_Partners");

                entity.HasOne(d => d.PayerType)
                    .WithMany(p => p.VehicleOptionalCosts)
                    .HasForeignKey(d => d.PayerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleOptionalCosts_VehicleCostPayerTypes");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleOptionalCosts)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleOptionalCosts_Vehicles");
            });

            modelBuilder.Entity<VehicleOwnershipType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleOwnershipTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleOwnershipTypes_AspNetUsers");
            });

            modelBuilder.Entity<VehiclePass>(entity =>
            {
                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.Property(e => e.PaidBy).HasComment("");

                entity.Property(e => e.SerialNumber).HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.VehiclePasses)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_VehiclePasses_Companies");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehiclePasses)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehiclePasses_AspNetUsers");

                entity.HasOne(d => d.PaidByNavigation)
                    .WithMany(p => p.VehiclePasses)
                    .HasForeignKey(d => d.PaidBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehiclePasses_VehicleCostPayerTypes");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.VehiclePasses)
                    .HasForeignKey(d => d.PartnerId)
                    .HasConstraintName("FK_VehiclePasses_Partners");

                entity.HasOne(d => d.PassStatus)
                    .WithMany(p => p.VehiclePasses)
                    .HasForeignKey(d => d.PassStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehiclePasses_PassStatuses");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.VehiclePasses)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehiclePasses_VehiclePassTypes");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehiclePasses)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehiclePasses_Vehicles");
            });

            modelBuilder.Entity<VehiclePassType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<VehiclePaymentPlan>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.InstallmentDueDate).HasColumnType("date");

                entity.Property(e => e.Interset).HasColumnType("money");

                entity.Property(e => e.LeasedEndDate).HasColumnType("date");

                entity.Property(e => e.LeasedStartDate).HasColumnType("date");

                entity.Property(e => e.PaymentAmount).HasColumnType("money");

                entity.Property(e => e.PaymentDate).HasColumnType("date");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehiclePaymentPlans)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehiclePaymentPlans_AspNetUsers");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.VehiclePaymentPlans)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehiclePaymentPlans_Partners");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehiclePaymentPlans)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehiclePaymentPlans_Vehicles");
            });

            modelBuilder.Entity<VehicleRegistration>(entity =>
            {
                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleRegistrations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleRegistrations_AspNetUsers");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleRegistrations)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleRegistrations_Vehicles");
            });

            modelBuilder.Entity<VehicleRentContract>(entity =>
            {
                entity.Property(e => e.DateFrom).HasColumnType("date");

                entity.Property(e => e.DateTo).HasColumnType("date");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.HasOne(d => d.ContractType)
                    .WithMany(p => p.VehicleRentContracts)
                    .HasForeignKey(d => d.ContractTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleRentContracts_VehicleRentContractTypes");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleRentContracts)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleRentContracts_AspNetUsers");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.VehicleRentContracts)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleRentContracts_Employees");

                entity.HasOne(d => d.Subcontractor)
                    .WithMany(p => p.VehicleRentContracts)
                    .HasForeignKey(d => d.SubcontractorId)
                    .HasConstraintName("FK_VehicleRentContracts_Partners");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleRentContracts)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleRentContracts_Vehicles");
            });

            modelBuilder.Entity<VehicleRentContractType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleRentContractTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleRentContractTypes_AspNetUsers");
            });

            modelBuilder.Entity<VehicleServiceStatus>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleServiceStatuses)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleServiceStatuses_AspNetUsers");

                entity.HasOne(d => d.ServiceStatus)
                    .WithMany(p => p.VehicleServiceStatuses)
                    .HasForeignKey(d => d.ServiceStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleServiceStatuses_ServiceStatuses");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleServiceStatuses)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleServiceStatuses_Vehicles");
            });

            modelBuilder.Entity<VehicleStatusHistory>(entity =>
            {
                entity.ToTable("VehicleStatusHistory");

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleStatusHistories)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleStatusHistory_AspNetUsers");

                entity.HasOne(d => d.InactivityReason)
                    .WithMany(p => p.VehicleStatusHistories)
                    .HasForeignKey(d => d.InactivityReasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleStatusHistory_InactivityReasons");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleStatusHistories)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleStatusHistory_Vehicles");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.VehicleTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_VehicleTypes_AspNetUsers");

                entity.HasOne(d => d.IdParentNavigation)
                    .WithMany(p => p.InverseIdParentNavigation)
                    .HasForeignKey(d => d.IdParent)
                    .HasConstraintName("FK_VehicleTypes_VehicleTypes");
            });

            modelBuilder.Entity<Violation>(entity =>
            {
                entity.Property(e => e.AdditionalFee).HasColumnType("money");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DotstateId).HasColumnName("DOTStateId");

                entity.Property(e => e.ElddisconectedNoteId).HasColumnName("ELDDisconectedNoteId");

                entity.Property(e => e.Highway).HasMaxLength(100);

                entity.Property(e => e.HosviolationFixedId).HasColumnName("HOSViolationFixedId");

                entity.Property(e => e.Hosviolations).HasColumnName("HOSViolations");

                entity.Property(e => e.LegalCost).HasColumnType("money");

                entity.Property(e => e.Location).HasMaxLength(200);

                entity.Property(e => e.PersonalConveyanceDuration).HasColumnType("time(0)");

                entity.Property(e => e.PtimissingFixedId).HasColumnName("PTIMissingFixedId");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Violations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Violations_AspNetUsers");

                entity.HasOne(d => d.Dotstate)
                    .WithMany(p => p.ViolationDotstates)
                    .HasForeignKey(d => d.DotstateId)
                    .HasConstraintName("FK_Violations_States");

                entity.HasOne(d => d.ElddisconectedNote)
                    .WithMany(p => p.Violations)
                    .HasForeignKey(d => d.ElddisconectedNoteId)
                    .HasConstraintName("FK_Violations_ELDDisconnectedNotes");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Violations)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Violations_Employees");

                entity.HasOne(d => d.FormMannerError)
                    .WithMany(p => p.Violations)
                    .HasForeignKey(d => d.FormMannerErrorId)
                    .HasConstraintName("FK_Violations_FormMannerErrors");

                entity.HasOne(d => d.HosviolationFixed)
                    .WithMany(p => p.Violations)
                    .HasForeignKey(d => d.HosviolationFixedId)
                    .HasConstraintName("FK_Violations_HOSViolationsFixed");

                entity.HasOne(d => d.IncidentType)
                    .WithMany(p => p.Violations)
                    .HasForeignKey(d => d.IncidentTypeId)
                    .HasConstraintName("FK_Violations_IncidentTypes");

                entity.HasOne(d => d.PtimissingFixed)
                    .WithMany(p => p.Violations)
                    .HasForeignKey(d => d.PtimissingFixedId)
                    .HasConstraintName("FK_Violations_PTIsMissingFixed");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.ViolationStates)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Violations_States1");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Violations)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Violations_Vehicles");

                entity.HasOne(d => d.ViolationType)
                    .WithMany(p => p.Violations)
                    .HasForeignKey(d => d.ViolationTypeId)
                    .HasConstraintName("FK_Violations_ViolationTypes");
            });

            modelBuilder.Entity<ViolationCalculation>(entity =>
            {
                entity.Property(e => e.AccidentAmount).HasColumnType("money");

                entity.Property(e => e.AccountingDate).HasColumnType("datetime");

                entity.Property(e => e.CleanInspectionAmount).HasColumnType("money");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.ViolationAmount).HasColumnType("money");

                entity.HasOne(d => d.CostIncomeType)
                    .WithMany(p => p.ViolationCalculations)
                    .HasForeignKey(d => d.CostIncomeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ViolationCalculations_CostIncomeTypes");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.ViolationCalculations)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_ViolationCalculations_AspNetUsers");

                entity.HasOne(d => d.Violation)
                    .WithMany(p => p.ViolationCalculations)
                    .HasForeignKey(d => d.ViolationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ViolationCalculations_Violations");
            });

            modelBuilder.Entity<ViolationType>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.ViolationTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_ViolationTypes_AspNetUsers");
            });

            modelBuilder.Entity<WaitingBonuse>(entity =>
            {
                entity.Property(e => e.AmountPerDay).HasColumnType("money");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.WaitingBonuses)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_WaitingBonuses_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.WaitingBonuses)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WaitBonuses_Employees");
            });

            modelBuilder.Entity<WorkPosition>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.WorkDescription).HasMaxLength(200);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.WorkPositions)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_WorkPositions_AspNetUsers");

                entity.HasOne(d => d.HierarchyLevel)
                    .WithMany(p => p.WorkPositions)
                    .HasForeignKey(d => d.HierarchyLevelId)
                    .HasConstraintName("FK_WorkPosition_HierarchyLevel");

                entity.HasOne(d => d.OrgUnit)
                    .WithMany(p => p.WorkPositions)
                    .HasForeignKey(d => d.OrgUnitId)
                    .HasConstraintName("FK_WorkPosition_OrgUnit");
            });

            modelBuilder.Entity<WorkingExperienceInner>(entity =>
            {
                entity.ToTable("WorkingExperienceInner");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.EmploymentDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.WorkingExperienceInners)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkingExperienceInner_Companies");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.WorkingExperienceInners)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_WorkingExperienceInner_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.WorkingExperienceInners)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkingExperienceInner_Employees");

                entity.HasOne(d => d.EmploymentType)
                    .WithMany(p => p.WorkingExperienceInners)
                    .HasForeignKey(d => d.EmploymentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkingExperienceInner_EmploymentTypes");

                entity.HasOne(d => d.OrgUnit)
                    .WithMany(p => p.WorkingExperienceInners)
                    .HasForeignKey(d => d.OrgUnitId)
                    .HasConstraintName("FK_WorkingExperienceInner_OrgUnits");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.WorkingExperienceInners)
                    .HasForeignKey(d => d.PartnerId)
                    .HasConstraintName("FK_WorkingExperienceInner_Partners");

                entity.HasOne(d => d.WorkPosition)
                    .WithMany(p => p.WorkingExperienceInners)
                    .HasForeignKey(d => d.WorkPositionId)
                    .HasConstraintName("FK_WorkingExperienceInner_WorkPositions");
            });

            modelBuilder.Entity<WorkingExperienceOutter>(entity =>
            {
                entity.ToTable("WorkingExperienceOutter");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EmploymentDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.WorkingExperienceOutters)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_WorkingExperienceOutter_AspNetUsers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.WorkingExperienceOutters)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkingExperienceOutter_Employees");

                entity.HasOne(d => d.EmploymentType)
                    .WithMany(p => p.WorkingExperienceOutters)
                    .HasForeignKey(d => d.EmploymentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkingExperienceOutter_EmploymentTypes");
            });

            modelBuilder.Entity<ZipCode>(entity =>
            {
                entity.Property(e => e.AreaCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CityType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CountyFips)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Dst)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MsaCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StateFips)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TimeZone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode1)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ZipCode")
                    .IsFixedLength();

                entity.Property(e => e.ZipCodeType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.City)
                    .WithMany(p => p.ZipCodes)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZipCode_City");

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.ZipCodes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_ZipCodes_AspNetUsers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

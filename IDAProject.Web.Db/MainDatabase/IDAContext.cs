using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IDAProject.Web.Db.MainDatabase;

public partial class IdaContext : DbContext
{
    public IdaContext()
    {
    }

    public IdaContext(DbContextOptions<IdaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AbsenceType> AbsenceTypes { get; set; }

    public virtual DbSet<ActivityType> ActivityTypes { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; }

    public virtual DbSet<AspNetFeature> AspNetFeatures { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetRoleFeature> AspNetRoleFeatures { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserOrgUnit> AspNetUserOrgUnits { get; set; }

    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Counter> Counters { get; set; }

    public virtual DbSet<CronNotification> CronNotifications { get; set; }

    public virtual DbSet<CronNotificationType> CronNotificationTypes { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentSerieType> DocumentSerieTypes { get; set; }

    public virtual DbSet<DocumentSeries> DocumentSeries { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<DocumentsSource> DocumentsSources { get; set; }

    public virtual DbSet<EmailJobSetting> EmailJobSettings { get; set; }

    public virtual DbSet<EmailJobType> EmailJobTypes { get; set; }

    public virtual DbSet<EmailNotificationType> EmailNotificationTypes { get; set; }

    public virtual DbSet<EmailQueue> EmailQueues { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeAbsence> EmployeeAbsences { get; set; }

    public virtual DbSet<EmploymentType> EmploymentTypes { get; set; }

    public virtual DbSet<ExchangeRate> ExchangeRates { get; set; }

    public virtual DbSet<GeneralSetting> GeneralSettings { get; set; }

    public virtual DbSet<Hash> Hashes { get; set; }

    public virtual DbSet<HierarchyLevel> HierarchyLevels { get; set; }

    public virtual DbSet<IdaTask> IdaTasks { get; set; }

    public virtual DbSet<Integration> Integrations { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobParameter> JobParameters { get; set; }

    public virtual DbSet<JobQueue> JobQueues { get; set; }

    public virtual DbSet<JobType> JobTypes { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<MeasureUnit> MeasureUnits { get; set; }

    public virtual DbSet<MeasureUnitType> MeasureUnitTypes { get; set; }

    public virtual DbSet<NoticeType> NoticeTypes { get; set; }

    public virtual DbSet<OrgUnit> OrgUnits { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerCategory> PartnerCategories { get; set; }

    public virtual DbSet<PartnerType> PartnerTypes { get; set; }

    public virtual DbSet<Period> Periods { get; set; }

    public virtual DbSet<PlanStatus> PlanStatuses { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<RegularActivity> RegularActivities { get; set; }

    public virtual DbSet<Relationship> Relationships { get; set; }

    public virtual DbSet<Schema> Schemas { get; set; }

    public virtual DbSet<Sector> Sectors { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<State1> States1 { get; set; }

    public virtual DbSet<TasksPlanning> TasksPlannings { get; set; }

    public virtual DbSet<TasksPlanningComment> TasksPlanningComments { get; set; }

    public virtual DbSet<TasksRealization> TasksRealizations { get; set; }

    public virtual DbSet<TasksRealizationComment> TasksRealizationComments { get; set; }

    public virtual DbSet<UserLog> UserLogs { get; set; }

    public virtual DbSet<UserMessage> UserMessages { get; set; }

    public virtual DbSet<UserNotification> UserNotifications { get; set; }

    public virtual DbSet<UserSetting> UserSettings { get; set; }

    public virtual DbSet<ZipCode> ZipCodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultDatabase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AbsenceType>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ActivityType>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.ActivityTypes)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_ActivityTypes_AspNetUsers");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.StreetName).HasMaxLength(200);
            entity.Property(e => e.StreetNumber).HasMaxLength(50);
            entity.Property(e => e.ValidFrom).HasColumnType("datetime");
            entity.Property(e => e.ValidUntil).HasColumnType("datetime");

            entity.HasOne(d => d.City).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Addresses_Cities");

            entity.HasOne(d => d.Company).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_Addresses_Companies");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Addresses_AspNetUsers");

            entity.HasOne(d => d.Partner).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.PartnerId)
                .HasConstraintName("FK_Addresses_Partners");

            entity.HasOne(d => d.State).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_Addresses_States");

            entity.HasOne(d => d.ZipCode).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.ZipCodeId)
                .HasConstraintName("FK_Addresses_ZipCodes");
        });

        modelBuilder.Entity<AggregatedCounter>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("PK_HangFire_CounterAggregated");

            entity.ToTable("AggregatedCounter", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_AggregatedCounter_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

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

            entity.HasOne(d => d.Company).WithMany(p => p.AspNetRoles)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_AspNetRoles_Companies");
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetRoleFeature>(entity =>
        {
            entity.HasOne(d => d.AspNetFeature).WithMany(p => p.AspNetRoleFeatures)
                .HasForeignKey(d => d.AspNetFeatureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AspNetRoleFeatures_AspNetFeatures");

            entity.HasOne(d => d.AspNetRole).WithMany(p => p.AspNetRoleFeatures)
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
            entity.Property(e => e.UserCulture).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.Employee).WithMany(p => p.AspNetUsers)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_AspNetUsers_Employees");

            entity.HasOne(d => d.Org).WithMany(p => p.AspNetUsers)
                .HasForeignKey(d => d.OrgId)
                .HasConstraintName("FK_AspNetUsers_OrgUnits");

            entity.HasOne(d => d.Partner).WithMany(p => p.AspNetUsers)
                .HasForeignKey(d => d.PartnerId)
                .HasConstraintName("FK_AspNetUsers_Partners");
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserOrgUnit>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.AspNetUserOrgUnitAspNetUsers)
                .HasForeignKey(d => d.AspNetUserId)
                .HasConstraintName("FK_AspNetUserOrgUnits_AspNetUsers1");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.AspNetUserOrgUnitDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_AspNetUserOrgUnits_AspNetUsers");

            entity.HasOne(d => d.OrgUnit).WithMany(p => p.AspNetUserOrgUnits)
                .HasForeignKey(d => d.OrgUnitId)
                .HasConstraintName("FK_AspNetUserOrgUnits_OrgUnits");
        });

        modelBuilder.Entity<AspNetUserRole>(entity =>
        {
            entity.HasIndex(e => new { e.RoleId, e.UserId }, "IX_AspNetUserRoles_Relation").IsUnique();

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetUserRoles).HasForeignKey(d => d.RoleId);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserRoles).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_AspNetUserTokens_AspNetUsers");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_City");

            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .IsUnicode(false);

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_Cities_States");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Preduzeca");

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

            entity.HasOne(d => d.City).WithMany(p => p.Companies)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Companies_Cities");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.Companies)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Companies_AspNetUsers");

            entity.HasOne(d => d.FactoringHouse).WithMany(p => p.Companies)
                .HasForeignKey(d => d.FactoringHouseId)
                .HasConstraintName("FK_Companies_Partners");

            entity.HasOne(d => d.IdParentCompanyNavigation).WithMany(p => p.InverseIdParentCompanyNavigation)
                .HasForeignKey(d => d.IdParentCompany)
                .HasConstraintName("FK_Companies_Companies");

            entity.HasOne(d => d.State).WithMany(p => p.Companies)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_Company_State");

            entity.HasOne(d => d.ZipCode).WithMany(p => p.Companies)
                .HasForeignKey(d => d.ZipCodeId)
                .HasConstraintName("FK_Company_ZipCode");
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Counter", "HangFire");

            entity.HasIndex(e => e.Key, "CX_HangFire_Counter").IsClustered();

            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            entity.Property(e => e.Key).HasMaxLength(100);
        });

        modelBuilder.Entity<CronNotification>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.FinishedDate).HasColumnType("datetime");
            entity.Property(e => e.RetryCount).HasDefaultValue(1);

            entity.HasOne(d => d.NotificationType).WithMany(p => p.CronNotifications)
                .HasForeignKey(d => d.NotificationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CronNotifications_CronNotificationType");
        });

        modelBuilder.Entity<CronNotificationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CronNotificationType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(64);
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Valute");

            entity.Property(e => e.AlphaId).HasMaxLength(3);
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.Currencies)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Currencies_AspNetUsers");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.DownloadFileName).HasMaxLength(200);
            entity.Property(e => e.RelativeFilePath)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UploadedDate).HasColumnType("datetime");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.DocumentDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Documents_AspNetUsers");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.Documents)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Documents_DocumentTypes");

            entity.HasOne(d => d.Source).WithMany(p => p.Documents)
                .HasForeignKey(d => d.SourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Documents_DocumentsSource");

            entity.HasOne(d => d.UploadedByUser).WithMany(p => p.DocumentUploadedByUsers)
                .HasForeignKey(d => d.UploadedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Documents_AspNetUser_UploadedBy");
        });

        modelBuilder.Entity<DocumentSerieType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DocumentSerieType");

            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.DocumentSerieTypes)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_DocumentSerieTypes_AspNetUsers");
        });

        modelBuilder.Entity<DocumentSeries>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IncrementSeed).HasDefaultValue(1);
            entity.Property(e => e.Pattern).HasMaxLength(50);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.DocumentSeries)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_DocumentSeries_AspNetUsers");

            entity.HasOne(d => d.DocumentSerieType).WithMany(p => p.DocumentSeries)
                .HasForeignKey(d => d.DocumentSerieTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentSeries_DocumentSerieTypes");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.DocumentTypes)
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

        modelBuilder.Entity<EmailJobSetting>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Email1).HasMaxLength(100);
            entity.Property(e => e.Email10).HasMaxLength(100);
            entity.Property(e => e.Email2).HasMaxLength(100);
            entity.Property(e => e.Email3).HasMaxLength(100);
            entity.Property(e => e.Email4).HasMaxLength(100);
            entity.Property(e => e.Email5).HasMaxLength(100);
            entity.Property(e => e.Email6).HasMaxLength(100);
            entity.Property(e => e.Email7).HasMaxLength(100);
            entity.Property(e => e.Email8).HasMaxLength(100);
            entity.Property(e => e.Email9).HasMaxLength(100);
            entity.Property(e => e.Enabled).HasDefaultValue(true);
            entity.Property(e => e.Note).HasMaxLength(500);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.EmailJobSettings)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_EmailJobSettings_AspNetUsers");

            entity.HasOne(d => d.EmailJobType).WithMany(p => p.EmailJobSettings)
                .HasForeignKey(d => d.EmailJobTypeId)
                .HasConstraintName("FK_EmailJobSettings_EmailJobTypes");
        });

        modelBuilder.Entity<EmailJobType>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<EmailNotificationType>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.EmailNotificationTypes)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_EmailNotificationTypes_AspNetUsers");
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

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.EmailQueues)
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
            entity.Property(e => e.EmployeeNumber).HasMaxLength(50);
            entity.Property(e => e.FederalNumber).HasMaxLength(50);
            entity.Property(e => e.HousePhoneNumber).HasMaxLength(50);
            entity.Property(e => e.InsuranceNumber).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.OwnPartnerCompany).HasDefaultValue(false);
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

            entity.HasOne(d => d.City).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Employees_Cities");

            entity.HasOne(d => d.Company).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_Employees_Company");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Employees_AspNetUsers");

            entity.HasOne(d => d.JobType).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobTypeId)
                .HasConstraintName("FK_Employees_JobTypes");

            entity.HasOne(d => d.NoticeType).WithMany(p => p.Employees)
                .HasForeignKey(d => d.NoticeTypeId)
                .HasConstraintName("FK_Employees_NoticeTypes");

            entity.HasOne(d => d.OrgUnit).WithMany(p => p.Employees)
                .HasForeignKey(d => d.OrgUnitId)
                .HasConstraintName("FK_Employees_OrgUnits");

            entity.HasOne(d => d.Partner).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PartnerId)
                .HasConstraintName("FK_Employees_Partners");

            entity.HasOne(d => d.Sector).WithMany(p => p.Employees)
                .HasForeignKey(d => d.SectorId)
                .HasConstraintName("FK_Employees_Sectors");

            entity.HasOne(d => d.State).WithMany(p => p.Employees)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_Employees_States");

            entity.HasOne(d => d.ZipCode).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ZipCodeId)
                .HasConstraintName("FK_Employees_ZipCodes");
        });

        modelBuilder.Entity<EmployeeAbsence>(entity =>
        {
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");

            entity.HasOne(d => d.AbsenceType).WithMany(p => p.EmployeeAbsences)
                .HasForeignKey(d => d.AbsenceTypeId)
                .HasConstraintName("FK_EmployeeAbsences_AbsenceTypes");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.EmployeeAbsences)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_EmployeeAbsences_AspNetUsers");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeAbsences)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_EmployeeAbsences_Employees");
        });

        modelBuilder.Entity<EmploymentType>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.EmploymentTypes)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_EmploymentTypes_AspNetUsers");
        });

        modelBuilder.Entity<ExchangeRate>(entity =>
        {
            entity.Property(e => e.CurrencyDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.ExchangeRate1)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("ExchangeRate");

            entity.HasOne(d => d.Currency).WithMany(p => p.ExchangeRates)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExchangeRates_Currencies");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.ExchangeRates)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_ExchangeRates_AspNetUsers");
        });

        modelBuilder.Entity<GeneralSetting>(entity =>
        {
            entity.Property(e => e.DateFormat).HasMaxLength(50);
            entity.Property(e => e.LocationCode).HasMaxLength(50);
            entity.Property(e => e.MessageOfTheDay).HasMaxLength(500);

            entity.HasOne(d => d.Currency).WithMany(p => p.GeneralSettings)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GeneralSettings_Currencies");

            entity.HasOne(d => d.MeasureFuel).WithMany(p => p.GeneralSettingMeasureFuels)
                .HasForeignKey(d => d.MeasureFuelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GeneralSettings_MeasureUnits_Fuel");

            entity.HasOne(d => d.MeasureTraveledWay).WithMany(p => p.GeneralSettingMeasureTraveledWays)
                .HasForeignKey(d => d.MeasureTraveledWayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GeneralSettings_MeasureUnits_TraveledWay");

            entity.HasOne(d => d.MeasureVehicleLength).WithMany(p => p.GeneralSettingMeasureVehicleLengths)
                .HasForeignKey(d => d.MeasureVehicleLengthId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GeneralSettings_MeasureUnits_VehicleLength");

            entity.HasOne(d => d.MeasureVehicleWeight).WithMany(p => p.GeneralSettingMeasureVehicleWeights)
                .HasForeignKey(d => d.MeasureVehicleWeightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GeneralSettings_MeasureUnits_VehicleWeight");
        });

        modelBuilder.Entity<Hash>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Field }).HasName("PK_HangFire_Hash");

            entity.ToTable("Hash", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Hash_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Field).HasMaxLength(100);
        });

        modelBuilder.Entity<HierarchyLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HierarchyLevel");

            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.HierarchyLevels)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_HierarchyLevels_AspNetUsers");
        });

        modelBuilder.Entity<IdaTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Tasks");

            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.IdaTasks)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_IdaTasks_AspNetUsers");

            entity.HasOne(d => d.Project).WithMany(p => p.IdaTasks)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_IdaTasks_Projects");
        });

        modelBuilder.Entity<Integration>(entity =>
        {
            entity.HasKey(e => e.ApiKey);

            entity.Property(e => e.ApiKey).ValueGeneratedNever();
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.Integrations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Integrations_AspNetUsers");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.Property(e => e.BarCode).HasMaxLength(20);
            entity.Property(e => e.CaptionOnDeclaration).HasMaxLength(50);
            entity.Property(e => e.CompanyAdress).HasMaxLength(50);
            entity.Property(e => e.CompanyCity).HasMaxLength(30);
            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.CompanyPhoneNo).HasMaxLength(20);
            entity.Property(e => e.CountryRegionCode).HasMaxLength(10);
            entity.Property(e => e.CountryRegionName).HasMaxLength(50);
            entity.Property(e => e.CrossReferenceNo).HasMaxLength(200);
            entity.Property(e => e.Description2).HasMaxLength(50);
            entity.Property(e => e.IsoDescription).HasMaxLength(50);
            entity.Property(e => e.IsoStandardCode).HasMaxLength(10);
            entity.Property(e => e.ItemDescription).HasMaxLength(80);
            entity.Property(e => e.ItemNo).HasMaxLength(20);
            entity.Property(e => e.ItemTypeCode).HasMaxLength(10);
            entity.Property(e => e.ItemVehicleTypeCode).HasMaxLength(20);
            entity.Property(e => e.ItemVehicleTypeDescription).HasMaxLength(50);
            entity.Property(e => e.ItemVendorBrandCode).HasMaxLength(50);
            entity.Property(e => e.QuantityPerDeclaration).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.VendorName).HasMaxLength(50);
            entity.Property(e => e.VendorNo).HasMaxLength(20);
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HangFire_Job");

            entity.ToTable("Job", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Job_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.HasIndex(e => e.StateName, "IX_HangFire_Job_StateName").HasFilter("([StateName] IS NOT NULL)");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            entity.Property(e => e.StateName).HasMaxLength(20);
        });

        modelBuilder.Entity<JobParameter>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.Name }).HasName("PK_HangFire_JobParameter");

            entity.ToTable("JobParameter", "HangFire");

            entity.Property(e => e.Name).HasMaxLength(40);

            entity.HasOne(d => d.Job).WithMany(p => p.JobParameters)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK_HangFire_JobParameter_Job");
        });

        modelBuilder.Entity<JobQueue>(entity =>
        {
            entity.HasKey(e => new { e.Queue, e.Id }).HasName("PK_HangFire_JobQueue");

            entity.ToTable("JobQueue", "HangFire");

            entity.Property(e => e.Queue).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.FetchedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<JobType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_JobType");

            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.JobTypes)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_JobTypes_AspNetUsers");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.Property(e => e.CultureCode).HasMaxLength(50);
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Id }).HasName("PK_HangFire_List");

            entity.ToTable("List", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_List_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<MeasureUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_JediniceMera");

            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Sign).HasMaxLength(50);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.MeasureUnits)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_MeasureUnits_AspNetUsers");

            entity.HasOne(d => d.MeasureUnitType).WithMany(p => p.MeasureUnits)
                .HasForeignKey(d => d.MeasureUnitTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Measure_MeasureType1");
        });

        modelBuilder.Entity<MeasureUnitType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_VrsteMera");

            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.MeasureUnitTypes)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_MeasureUnitTypes_AspNetUsers");
        });

        modelBuilder.Entity<NoticeType>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.NoticeTypes)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_NoticeTypes_AspNetUsers");
        });

        modelBuilder.Entity<OrgUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OrgUnit");

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);

            entity.HasOne(d => d.Company).WithMany(p => p.OrgUnits)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrgUnits_Companies");

            entity.HasOne(d => d.ParentOrgUnit).WithMany(p => p.InverseParentOrgUnit)
                .HasForeignKey(d => d.ParentOrgUnitId)
                .HasConstraintName("FK_OrgUnits_OrgUnits");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Partner");

            entity.Property(e => e.AccountingCode).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.BankAccountNumber).HasMaxLength(50);
            entity.Property(e => e.BlockedComment).HasMaxLength(500);
            entity.Property(e => e.BussinesNameInAforeignLanguage)
                .HasMaxLength(50)
                .HasColumnName("BussinesNameInAForeignLanguage");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.ContactPerson).HasMaxLength(100);
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.DisclaimerNote).HasMaxLength(2000);
            entity.Property(e => e.Dot)
                .HasMaxLength(50)
                .HasColumnName("DOT");
            entity.Property(e => e.Duration).HasColumnType("datetime");
            entity.Property(e => e.Ein)
                .HasMaxLength(50)
                .HasColumnName("EIN");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMail");
            entity.Property(e => e.Fax).HasMaxLength(50);
            entity.Property(e => e.IdentificationNumber)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Mc)
                .HasMaxLength(50)
                .HasColumnName("MC");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Notes).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Pib)
                .HasMaxLength(50)
                .HasColumnName("PIB");
            entity.Property(e => e.RautingNumber).HasMaxLength(50);
            entity.Property(e => e.ShortenedName).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.SwiftCode).HasMaxLength(50);
            entity.Property(e => e.TimeDuration).HasColumnType("datetime");

            entity.HasOne(d => d.City).WithMany(p => p.Partners)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Partners_Cities");

            entity.HasOne(d => d.ContactCompany).WithMany(p => p.Partners)
                .HasForeignKey(d => d.ContactCompanyId)
                .HasConstraintName("FK_Partners_Companies");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.Partners)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Partners_AspNetUsers");

            entity.HasOne(d => d.PartnerType).WithMany(p => p.Partners)
                .HasForeignKey(d => d.PartnerTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partner_PartnerType");

            entity.HasOne(d => d.State).WithMany(p => p.Partners)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_Partner_State");

            entity.HasOne(d => d.ZipCode).WithMany(p => p.Partners)
                .HasForeignKey(d => d.ZipCodeId)
                .HasConstraintName("FK_Partner_ZipCode");
        });

        modelBuilder.Entity<PartnerCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PartnerCategory");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.PartnerCategories)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_PartnerCategories_AspNetUsers");
        });

        modelBuilder.Entity<PartnerType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PartnerType");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.PartnerTypes)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_PartnerTypes_AspNetUsers");

            entity.HasOne(d => d.PartnerCategory).WithMany(p => p.PartnerTypes)
                .HasForeignKey(d => d.PartnerCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartnerType_PartnerCategory");
        });

        modelBuilder.Entity<Period>(entity =>
        {
            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.Periods)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Periods_AspNetUsers");
        });

        modelBuilder.Entity<PlanStatus>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(500);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.PlanStatuses)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_PlanStatuses_AspNetUsers");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Projects_AspNetUsers");
        });

        modelBuilder.Entity<RegularActivity>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(500);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.RegularActivities)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_RegularActivities_AspNetUsers");
        });

        modelBuilder.Entity<Relationship>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.Relationships)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Relationships_AspNetUsers");
        });

        modelBuilder.Entity<Schema>(entity =>
        {
            entity.HasKey(e => e.Version).HasName("PK_HangFire_Schema");

            entity.ToTable("Schema", "HangFire");

            entity.Property(e => e.Version).ValueGeneratedNever();
        });

        modelBuilder.Entity<Sector>(entity =>
        {
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(500);
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HangFire_Server");

            entity.ToTable("Server", "HangFire");

            entity.HasIndex(e => e.LastHeartbeat, "IX_HangFire_Server_LastHeartbeat");

            entity.Property(e => e.Id).HasMaxLength(200);
            entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Value }).HasName("PK_HangFire_Set");

            entity.ToTable("Set", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Set_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.HasIndex(e => new { e.Key, e.Score }, "IX_HangFire_Set_Score");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Value).HasMaxLength(256);
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_State");

            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ShortName)
                .HasMaxLength(2)
                .IsUnicode(false);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.States)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_States_AspNetUsers");
        });

        modelBuilder.Entity<State1>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.Id }).HasName("PK_HangFire_State");

            entity.ToTable("State", "HangFire");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Reason).HasMaxLength(100);

            entity.HasOne(d => d.Job).WithMany(p => p.State1s)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK_HangFire_State_Job");
        });

        modelBuilder.Entity<TasksPlanning>(entity =>
        {
            entity.ToTable("TasksPlanning");

            entity.Property(e => e.ActivityName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");

            entity.HasOne(d => d.ActivityType).WithMany(p => p.TasksPlannings)
                .HasForeignKey(d => d.ActivityTypeId)
                .HasConstraintName("FK_TasksPlanning_ActivityTypes");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.TasksPlanningDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_TasksPlanning_AspNetUsers");

            entity.HasOne(d => d.Employee).WithMany(p => p.TasksPlannings)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_TasksPlanning_Employees");

            entity.HasOne(d => d.PlanStatus).WithMany(p => p.TasksPlannings)
                .HasForeignKey(d => d.PlanStatusId)
                .HasConstraintName("FK_TasksPlanning_PlanStatuses");

            entity.HasOne(d => d.Project).WithMany(p => p.TasksPlannings)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TasksPlanning_Projects");

            entity.HasOne(d => d.RegularActivity).WithMany(p => p.TasksPlannings)
                .HasForeignKey(d => d.RegularActivityId)
                .HasConstraintName("FK_TasksPlanning_RegularActivities");

            entity.HasOne(d => d.Task).WithMany(p => p.TasksPlannings)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK_TasksPlanning_IdaTasks");

            entity.HasOne(d => d.User).WithMany(p => p.TasksPlanningUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TasksPlanning_AspNetUsers1");
        });

        modelBuilder.Entity<TasksPlanningComment>(entity =>
        {
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.TasksPlanningCommentDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_TasksPlanningComments_AspNetUsers");

            entity.HasOne(d => d.ParentTaskPlanningComment).WithMany(p => p.InverseParentTaskPlanningComment)
                .HasForeignKey(d => d.ParentTaskPlanningCommentId)
                .HasConstraintName("FK_TasksPlanningComments_TasksPlanningComments1");

            entity.HasOne(d => d.TaskPlanning).WithMany(p => p.TasksPlanningComments)
                .HasForeignKey(d => d.TaskPlanningId)
                .HasConstraintName("FK_TasksPlanningComments_TasksPlanning");

            entity.HasOne(d => d.User).WithMany(p => p.TasksPlanningCommentUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TasksPlanningComments_AspNetUsers1");
        });

        modelBuilder.Entity<TasksRealization>(entity =>
        {
            entity.ToTable("TasksRealization");

            entity.Property(e => e.Activity).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Report).HasMaxLength(100);

            entity.HasOne(d => d.ActivityType).WithMany(p => p.TasksRealizations)
                .HasForeignKey(d => d.ActivityTypeId)
                .HasConstraintName("FK_TasksRealization_ActivityTypes");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.TasksRealizationDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_TasksRealization_AspNetUsers");

            entity.HasOne(d => d.IdaTask).WithMany(p => p.TasksRealizations)
                .HasForeignKey(d => d.IdaTaskId)
                .HasConstraintName("FK_TasksRealization_IdaTasks");

            entity.HasOne(d => d.Project).WithMany(p => p.TasksRealizations)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_TasksRealization_Projects");

            entity.HasOne(d => d.RegularActivity).WithMany(p => p.TasksRealizations)
                .HasForeignKey(d => d.RegularActivityId)
                .HasConstraintName("FK_TasksRealization_RegularActivities");

            entity.HasOne(d => d.TasksPlanning).WithMany(p => p.TasksRealizations)
                .HasForeignKey(d => d.TasksPlanningId)
                .HasConstraintName("FK_TasksRealization_TasksPlanning");

            entity.HasOne(d => d.User).WithMany(p => p.TasksRealizationUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TasksRealization_AspNetUsers1");
        });

        modelBuilder.Entity<TasksRealizationComment>(entity =>
        {
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.TasksRealizationCommentDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_TasksRealizationComments_AspNetUsers");

            entity.HasOne(d => d.ParentTaskRealizationComment).WithMany(p => p.InverseParentTaskRealizationComment)
                .HasForeignKey(d => d.ParentTaskRealizationCommentId)
                .HasConstraintName("FK_TasksRealizationComments_TasksRealizationComments1");

            entity.HasOne(d => d.TaskRealization).WithMany(p => p.TasksRealizationComments)
                .HasForeignKey(d => d.TaskRealizationId)
                .HasConstraintName("FK_TasksRealizationComments_TasksRealization");

            entity.HasOne(d => d.User).WithMany(p => p.TasksRealizationCommentUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_TasksRealizationComments_AspNetUsers1");
        });

        modelBuilder.Entity<UserLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UsersLogs");

            entity.Property(e => e.LocalIp)
                .HasMaxLength(50)
                .HasColumnName("LocalIP");
            entity.Property(e => e.LoginDateTime).HasColumnType("datetime");
            entity.Property(e => e.LogoutDateTime).HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.PublicIp)
                .HasMaxLength(50)
                .HasColumnName("PublicIP");
            entity.Property(e => e.RemoteIp)
                .HasMaxLength(50)
                .HasColumnName("RemoteIP");
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.WindowsUserName).HasMaxLength(50);

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.UserLogs)
                .HasForeignKey(d => d.AspNetUserId)
                .HasConstraintName("FK_UsersLogs_AspNetUsers");
        });

        modelBuilder.Entity<UserMessage>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Message).HasMaxLength(4000);

            entity.HasOne(d => d.UserFromNavigation).WithMany(p => p.UserMessageUserFromNavigations)
                .HasForeignKey(d => d.UserFrom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserMessages_AspNetUsersFrom");

            entity.HasOne(d => d.UserToNavigation).WithMany(p => p.UserMessageUserToNavigations)
                .HasForeignKey(d => d.UserTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserMessages_AspNetUsersTo");
        });

        modelBuilder.Entity<UserNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Notifications");

            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(500);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.UserNotifications)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Notifications_AspNetUsers");

            entity.HasOne(d => d.Sector).WithMany(p => p.UserNotifications)
                .HasForeignKey(d => d.SectorId)
                .HasConstraintName("FK_Notifications_Sectors");
        });

        modelBuilder.Entity<UserSetting>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.SettingsKey });

            entity.Property(e => e.SettingsKey)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.StringValue).HasMaxLength(2048);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.UserSettingDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_UserSettings_DeletedBy_AspNetUsers");

            entity.HasOne(d => d.User).WithMany(p => p.UserSettingUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserSettings_AspNetUsers");
        });

        modelBuilder.Entity<ZipCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ZipCode");

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
                .IsFixedLength()
                .HasColumnName("ZipCode");
            entity.Property(e => e.ZipCodeType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.ZipCodes)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_ZipCodes_AspNetUsers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

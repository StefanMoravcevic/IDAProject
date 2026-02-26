using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class SiriusCoreContext : DbContext
    {
        public SiriusCoreContext()
        {
        }

        public SiriusCoreContext(DbContextOptions<SiriusCoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; } = null!;
        public virtual DbSet<AspNetFeature> AspNetFeatures { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetRoleFeature> AspNetRoleFeatures { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<CostIncomeType> CostIncomeTypes { get; set; } = null!;
        public virtual DbSet<Counter> Counters { get; set; } = null!;
        public virtual DbSet<CronNotification> CronNotifications { get; set; } = null!;
        public virtual DbSet<CronNotificationType> CronNotificationTypes { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<DocumentSerieType> DocumentSerieTypes { get; set; } = null!;
        public virtual DbSet<DocumentSeries> DocumentSeries { get; set; } = null!;
        public virtual DbSet<DocumentType> DocumentTypes { get; set; } = null!;
        public virtual DbSet<DocumentsSource> DocumentsSources { get; set; } = null!;
        public virtual DbSet<EmailQueue> EmailQueues { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmploymentType> EmploymentTypes { get; set; } = null!;
        public virtual DbSet<FamilyMember> FamilyMembers { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<GeneralSetting> GeneralSettings { get; set; } = null!;
        public virtual DbSet<Hash> Hashes { get; set; } = null!;
        public virtual DbSet<HierarchyLevel> HierarchyLevels { get; set; } = null!;
        public virtual DbSet<Integration> Integrations { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<JobParameter> JobParameters { get; set; } = null!;
        public virtual DbSet<JobQueue> JobQueues { get; set; } = null!;
        public virtual DbSet<JobType> JobTypes { get; set; } = null!;
        public virtual DbSet<List> Lists { get; set; } = null!;
        public virtual DbSet<MeasureUnit> MeasureUnits { get; set; } = null!;
        public virtual DbSet<MeasureUnitType> MeasureUnitTypes { get; set; } = null!;
        public virtual DbSet<NoticeType> NoticeTypes { get; set; } = null!;
        public virtual DbSet<OrgUnit> OrgUnits { get; set; } = null!;
        public virtual DbSet<Partner> Partners { get; set; } = null!;
        public virtual DbSet<PartnerCategory> PartnerCategories { get; set; } = null!;
        public virtual DbSet<PartnerType> PartnerTypes { get; set; } = null!;
        public virtual DbSet<Relationship> Relationships { get; set; } = null!;
        public virtual DbSet<Schema> Schemas { get; set; } = null!;
        public virtual DbSet<Server> Servers { get; set; } = null!;
        public virtual DbSet<Set> Sets { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;
        public virtual DbSet<State1> States1 { get; set; } = null!;
        public virtual DbSet<UserMessage> UserMessages { get; set; } = null!;
        public virtual DbSet<UserSetting> UserSettings { get; set; } = null!;
        public virtual DbSet<WorkPosition> WorkPositions { get; set; } = null!;
        public virtual DbSet<WorkingExperienceInner> WorkingExperienceInners { get; set; } = null!;
        public virtual DbSet<WorkingExperienceOutter> WorkingExperienceOutters { get; set; } = null!;
        public virtual DbSet<ZipCode> ZipCodes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=192.168.100.100;Initial Catalog=SiriusCore;User Id=vikings_user;Password=VIKINGS1102;;ApplicationIntent=ReadWrite;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Cities_AspNetUsers");
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

            modelBuilder.Entity<EmploymentType>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.EmploymentTypes)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_EmploymentTypes_AspNetUsers");
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

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Genders)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Genders_AspNetUsers");
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

            modelBuilder.Entity<Relationship>(entity =>
            {
                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DeletedByNavigation)
                    .WithMany(p => p.Relationships)
                    .HasForeignKey(d => d.DeletedBy)
                    .HasConstraintName("FK_Relationships_AspNetUsers");
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

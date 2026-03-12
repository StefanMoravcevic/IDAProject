using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class AspNetUser
{
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

    public int? EmployeeId { get; set; }

    /// <summary>
    /// FCM registration token
    /// </summary>
    public string? FcmToken { get; set; }

    public string? UserCulture { get; set; }

    public int? PartnerId { get; set; }

    public int? OrgId { get; set; }

    public virtual ICollection<ActivityType> ActivityTypes { get; set; } = new List<ActivityType>();

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserOrgUnit> AspNetUserOrgUnitAspNetUsers { get; set; } = new List<AspNetUserOrgUnit>();

    public virtual ICollection<AspNetUserOrgUnit> AspNetUserOrgUnitDeletedByNavigations { get; set; } = new List<AspNetUserOrgUnit>();

    public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; } = new List<AspNetUserRole>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual ICollection<Currency> Currencies { get; set; } = new List<Currency>();

    public virtual ICollection<Document> DocumentDeletedByNavigations { get; set; } = new List<Document>();

    public virtual ICollection<DocumentSerieType> DocumentSerieTypes { get; set; } = new List<DocumentSerieType>();

    public virtual ICollection<DocumentSeries> DocumentSeries { get; set; } = new List<DocumentSeries>();

    public virtual ICollection<DocumentType> DocumentTypes { get; set; } = new List<DocumentType>();

    public virtual ICollection<Document> DocumentUploadedByUsers { get; set; } = new List<Document>();

    public virtual ICollection<EmailJobSetting> EmailJobSettings { get; set; } = new List<EmailJobSetting>();

    public virtual ICollection<EmailNotificationType> EmailNotificationTypes { get; set; } = new List<EmailNotificationType>();

    public virtual ICollection<EmailQueue> EmailQueues { get; set; } = new List<EmailQueue>();

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<EmployeeAbsence> EmployeeAbsences { get; set; } = new List<EmployeeAbsence>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<EmploymentType> EmploymentTypes { get; set; } = new List<EmploymentType>();

    public virtual ICollection<ExchangeRate> ExchangeRates { get; set; } = new List<ExchangeRate>();

    public virtual ICollection<HierarchyLevel> HierarchyLevels { get; set; } = new List<HierarchyLevel>();

    public virtual ICollection<IdaTask> IdaTasks { get; set; } = new List<IdaTask>();

    public virtual ICollection<Integration> Integrations { get; set; } = new List<Integration>();

    public virtual ICollection<JobType> JobTypes { get; set; } = new List<JobType>();

    public virtual ICollection<MeasureUnitType> MeasureUnitTypes { get; set; } = new List<MeasureUnitType>();

    public virtual ICollection<MeasureUnit> MeasureUnits { get; set; } = new List<MeasureUnit>();

    public virtual ICollection<NoticeType> NoticeTypes { get; set; } = new List<NoticeType>();

    public virtual OrgUnit? Org { get; set; }

    public virtual Partner? Partner { get; set; }

    public virtual ICollection<PartnerCategory> PartnerCategories { get; set; } = new List<PartnerCategory>();

    public virtual ICollection<PartnerType> PartnerTypes { get; set; } = new List<PartnerType>();

    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();

    public virtual ICollection<Period> Periods { get; set; } = new List<Period>();

    public virtual ICollection<PlanStatus> PlanStatuses { get; set; } = new List<PlanStatus>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<RegularActivity> RegularActivities { get; set; } = new List<RegularActivity>();

    public virtual ICollection<Relationship> Relationships { get; set; } = new List<Relationship>();

    public virtual ICollection<State> States { get; set; } = new List<State>();

    public virtual ICollection<TasksPlanningComment> TasksPlanningCommentDeletedByNavigations { get; set; } = new List<TasksPlanningComment>();

    public virtual ICollection<TasksPlanningComment> TasksPlanningCommentUsers { get; set; } = new List<TasksPlanningComment>();

    public virtual ICollection<TasksPlanning> TasksPlanningDeletedByNavigations { get; set; } = new List<TasksPlanning>();

    public virtual ICollection<TasksPlanning> TasksPlanningUsers { get; set; } = new List<TasksPlanning>();

    public virtual ICollection<TasksRealizationComment> TasksRealizationCommentDeletedByNavigations { get; set; } = new List<TasksRealizationComment>();

    public virtual ICollection<TasksRealizationComment> TasksRealizationCommentUsers { get; set; } = new List<TasksRealizationComment>();

    public virtual ICollection<TasksRealization> TasksRealizationDeletedByNavigations { get; set; } = new List<TasksRealization>();

    public virtual ICollection<TasksRealization> TasksRealizationUsers { get; set; } = new List<TasksRealization>();

    public virtual ICollection<UserLog> UserLogs { get; set; } = new List<UserLog>();

    public virtual ICollection<UserMessage> UserMessageUserFromNavigations { get; set; } = new List<UserMessage>();

    public virtual ICollection<UserMessage> UserMessageUserToNavigations { get; set; } = new List<UserMessage>();

    public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();

    public virtual ICollection<UserSetting> UserSettingDeletedByNavigations { get; set; } = new List<UserSetting>();

    public virtual ICollection<UserSetting> UserSettingUsers { get; set; } = new List<UserSetting>();

    public virtual ICollection<ZipCode> ZipCodes { get; set; } = new List<ZipCode>();
}

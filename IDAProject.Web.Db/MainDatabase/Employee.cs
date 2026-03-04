using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class Employee
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? Address { get; set; }

    public int? ZipCodeId { get; set; }

    public int? CityId { get; set; }

    public int? StateId { get; set; }

    public int? JobTypeId { get; set; }

    public int? CompanyId { get; set; }

    public int? OrgUnitId { get; set; }

    public int? PartnerId { get; set; }

    public bool? OwnPartnerCompany { get; set; }

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

    public string? Email { get; set; }

    public int? NoticeTypeId { get; set; }

    public string? ShoeSize { get; set; }

    public string? SuiteSize { get; set; }

    public bool Blocked { get; set; }

    public string? AccountingCode { get; set; }

    public int? GenderId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public string? EmployeeNumber { get; set; }

    public string? Photo { get; set; }

    public int? SectorId { get; set; }

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();

    public virtual City? City { get; set; }

    public virtual Company? Company { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual ICollection<EmployeeAbsence> EmployeeAbsences { get; set; } = new List<EmployeeAbsence>();

    public virtual JobType? JobType { get; set; }

    public virtual NoticeType? NoticeType { get; set; }

    public virtual OrgUnit? OrgUnit { get; set; }

    public virtual Partner? Partner { get; set; }

    public virtual Sector? Sector { get; set; }

    public virtual State? State { get; set; }

    public virtual ICollection<TasksPlanning> TasksPlannings { get; set; } = new List<TasksPlanning>();

    public virtual ZipCode? ZipCode { get; set; }
}

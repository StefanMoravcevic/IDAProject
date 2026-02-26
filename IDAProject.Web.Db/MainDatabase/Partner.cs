using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class Partner
{
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

    public string? Pib { get; set; }

    public string? IdentificationNumber { get; set; }

    public string? ShortenedName { get; set; }

    public string? Notes { get; set; }

    public DateTime? Duration { get; set; }

    public string? Status { get; set; }

    public string? BussinesNameInAforeignLanguage { get; set; }

    public DateTime? TimeDuration { get; set; }

    public string? SwiftCode { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();

    public virtual City? City { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual Company? ContactCompany { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual PartnerType PartnerType { get; set; } = null!;

    public virtual State? State { get; set; }

    public virtual ZipCode? ZipCode { get; set; }
}

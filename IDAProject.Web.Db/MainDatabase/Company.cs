using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class Company
{
    public int Id { get; set; }

    public int? IdParentCompany { get; set; }

    public string? Logo { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public int? ZipCodeId { get; set; }

    public int? CityId { get; set; }

    public int? StateId { get; set; }

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

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<AspNetRole> AspNetRoles { get; set; } = new List<AspNetRole>();

    public virtual City? City { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Partner? FactoringHouse { get; set; }

    public virtual Company? IdParentCompanyNavigation { get; set; }

    public virtual ICollection<Company> InverseIdParentCompanyNavigation { get; set; } = new List<Company>();

    public virtual ICollection<OrgUnit> OrgUnits { get; set; } = new List<OrgUnit>();

    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();

    public virtual State? State { get; set; }

    public virtual ZipCode? ZipCode { get; set; }
}

using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class ZipCode
{
    public int Id { get; set; }

    public string ZipCode1 { get; set; } = null!;

    public string ZipCodeType { get; set; } = null!;

    public int CityId { get; set; }

    public string CityType { get; set; } = null!;

    public string CountyFips { get; set; } = null!;

    public string StateFips { get; set; } = null!;

    public string MsaCode { get; set; } = null!;

    public string AreaCode { get; set; } = null!;

    public string TimeZone { get; set; } = null!;

    public int Utc { get; set; }

    public string Dst { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();
}

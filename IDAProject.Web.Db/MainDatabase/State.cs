using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class State
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ShortName { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();
}

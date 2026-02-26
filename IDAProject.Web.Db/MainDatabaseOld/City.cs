using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class City
    {
        public City()
        {
            Companies = new HashSet<Company>();
            Contacts = new HashSet<Contact>();
            Employees = new HashSet<Employee>();
            FamilyMembers = new HashSet<FamilyMember>();
            FuelConsumptions = new HashSet<FuelConsumption>();
            Partners = new HashSet<Partner>();
            TourPoints = new HashSet<TourPoint>();
            ZipCodes = new HashSet<ZipCode>();
        }

        public int Id { get; set; }
        public int CountyId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual County County { get; set; } = null!;
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
        public virtual ICollection<FuelConsumption> FuelConsumptions { get; set; }
        public virtual ICollection<Partner> Partners { get; set; }
        public virtual ICollection<TourPoint> TourPoints { get; set; }
        public virtual ICollection<ZipCode> ZipCodes { get; set; }
    }
}

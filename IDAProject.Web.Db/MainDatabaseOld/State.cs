using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class State
    {
        public State()
        {
            Companies = new HashSet<Company>();
            Contacts = new HashSet<Contact>();
            Counties = new HashSet<County>();
            DriverLicences = new HashSet<DriverLicence>();
            Employees = new HashSet<Employee>();
            FamilyMembers = new HashSet<FamilyMember>();
            FuelConsumptions = new HashSet<FuelConsumption>();
            Partners = new HashSet<Partner>();
            TourPoints = new HashSet<TourPoint>();
            ViolationDotstates = new HashSet<Violation>();
            ViolationStates = new HashSet<Violation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<County> Counties { get; set; }
        public virtual ICollection<DriverLicence> DriverLicences { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
        public virtual ICollection<FuelConsumption> FuelConsumptions { get; set; }
        public virtual ICollection<Partner> Partners { get; set; }
        public virtual ICollection<TourPoint> TourPoints { get; set; }
        public virtual ICollection<Violation> ViolationDotstates { get; set; }
        public virtual ICollection<Violation> ViolationStates { get; set; }
    }
}

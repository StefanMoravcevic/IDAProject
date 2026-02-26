using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class FamilyMember
    {
        public int Id { get; set; }
        public int? RelationshipId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Address { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? YearOfBirth { get; set; }
        public bool? EmergencyContact { get; set; }
        public int EmployeeId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual City? City { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Employee Employee { get; set; } = null!;
        public virtual Relationship? Relationship { get; set; }
        public virtual State? State { get; set; }
    }
}

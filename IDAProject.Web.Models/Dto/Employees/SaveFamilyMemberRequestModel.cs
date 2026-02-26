namespace IDAProject.Web.Models.Dto.Employees
{
    public class SaveFamilyMemberRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; } = null!;
        public bool? EmergencyContact { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int? RelationshipId { get; set; }
        public int? YearOfBirth { get; set; }
        public int EmployeeId { get; set; }
    }
}

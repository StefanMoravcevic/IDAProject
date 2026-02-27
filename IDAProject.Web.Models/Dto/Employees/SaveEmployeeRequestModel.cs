namespace IDAProject.Web.Models.Dto.Employees
{
    public class SaveEmployeeRequestModel
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? MiddleName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string? PersonalId { get; set; }
        public string? PassportId { get; set; }
        public string? InsuranceNumber { get; set; }
        public string? FederalNumber { get; set; }
        public string? HousePhoneNumber { get; set; }
        public string? CellPhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool Blocked { get; set; }
        public bool? OwnPartnerCompany { get; set; }
        public string? Address { get; set; }
        public int? ZipCodeId { get; set; }
        public string? ZipCode { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int? JobTypeId { get; set; }
        public int? CompanyId { get; set; }
        public int? OrgUnitId { get; set; }
        public int? PartnerId { get; set; }
        public int? GenderId { get; set; }
        public string? BirthPlace { get; set; }
        public string? Citizenship { get; set; }
        public string? BankAccount { get; set; }
        public string? BankAccountAddition { get; set; }
        public string? NoticeType { get; set; }
        public int? NoticeTypeId { get; set; }
        public string? ShoeSize { get; set; }
        public string? SuiteSize { get; set; }
        public string? RoutingNumber { get; set; }
        public string? AccountingCode { get; set; }
        public string? EmployeeNumber { get; set; }
        public string? Photo { get; set; }
        public int? SectorId { get; set; }
    }
}

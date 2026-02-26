namespace IDAProject.Web.Models.Dto.Companies
{
    public class SaveCompanyRequestModel
    {
        public int Id { get; set; }
        public int? IdParentCompany { get; set; }
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
        public string? Logo { get; set; }
        public int? FactoringHouseId { get; set; }
    }
}

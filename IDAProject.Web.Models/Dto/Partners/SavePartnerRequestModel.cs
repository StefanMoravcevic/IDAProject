namespace IDAProject.Web.Models.Dto.Partners
{
    public class SavePartnerRequestModel
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
        public int? PaymentConditionId { get; set; }
        public int? PrimaryContactId { get; set; }
        public int? IncomeTypeId { get; set; }
        public bool Blocked { get; set; }
        public string? BlockedComment { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? RautingNumber { get; set; }
        public string? DisclaimerNote { get; set; }
        public int PartnerCategoryId { get; set; }
        public int? ContactCompanyId { get; set; }
    }
}

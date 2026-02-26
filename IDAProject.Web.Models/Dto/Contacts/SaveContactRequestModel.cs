namespace IDAProject.Web.Models.Dto.Contacts
{
    public class SaveContactRequestModel
    {
        public int Id { get; set; }
        public bool IsCompany { get; set; }
        public string? CompanyName { get; set; }
        /// <summary>
        /// The company (tentant) where the contact belongs to
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// The partner tied to this contact
        /// </summary>
        public int? PartnerId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public int? ZipCodeId { get; set; }
        public int? GenderId { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public bool InvoiceFlag { get; set; }
        /// <summary>
        /// Preferred method of communication (1-phone, 2-mobile, 3-mail)
        /// </summary>
        public int? MethodOfCommunication { get; set; }
        public int? ContactCompanyId { get; set; }

        public string? Ein { get; set; }
        public string? Mc { get; set; }
        public string? Dot { get; set; }
    }
}

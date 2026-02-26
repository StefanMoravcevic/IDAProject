namespace IDAProject.Web.Models.Dto.Contacts
{
    public class ContactDto : SaveContactRequestModel
    {

        public ContactDto()
        {
            TenantCompany = string.Empty;
            PartnerName = string.Empty;
            City = string.Empty;
            State = string.Empty;
            ZipCode = string.Empty;
            Gender = string.Empty;
            ContactCompany = string.Empty;
            ContactCompanyForSorting = string.Empty;
        }

        /// <summary>
        /// The company (tenant) where the contact belongs to
        /// </summary>
        public string TenantCompany { get; set; }

        /// <summary>
        /// The partner name tied to this contact
        /// </summary>
        public string PartnerName { get; set; }

        public string City { get; set; }
        public string State { get; set; }

        public string ZipCode { get; set; }
        public string Gender { get; set; }
        public string ContactCompany { get; set; }
        public string ContactCompanyForSorting { get; set; }

        public string PreferredMethodOfCommunication
        {
            get
            {
                if (MethodOfCommunication == 1)
                {
                    return "Phone";
                }
                if (MethodOfCommunication == 2)
                {
                    return "Mobile";
                }
                if (MethodOfCommunication == 3)
                {
                    return "Email";
                }
                return string.Empty;
            }
        }

        public string Name
        {
            get
            {
                if(IsCompany)
                {
                    return CompanyName!;
                }
                else
                {
                    return $"{FirstName} {LastName}";
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace SiriusCore.Web.Db.MainDatabase
{
    public partial class Contact
    {
        public Contact()
        {
            InverseContactCompany = new HashSet<Contact>();
            PartnerContactCompanies = new HashSet<Partner>();
            PartnerPrimaryContacts = new HashSet<Partner>();
            TourBrokerContacts = new HashSet<Tour>();
            TourCustomerContacts = new HashSet<Tour>();
        }

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
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public bool InvoiceFlag { get; set; }
        /// <summary>
        /// Preferred method of communication (1-phone, 2-mobile, 3-mail)
        /// </summary>
        public int? MethodOfCommunication { get; set; }
        public string? Ein { get; set; }
        public string? Mc { get; set; }
        public string? Dot { get; set; }
        public int? GenderId { get; set; }
        public int? ContactCompanyId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }

        public virtual City? City { get; set; }
        public virtual Contact? ContactCompany { get; set; }
        public virtual AspNetUser? DeletedByNavigation { get; set; }
        public virtual Gender? Gender { get; set; }
        public virtual Partner? Partner { get; set; }
        public virtual State? State { get; set; }
        public virtual ZipCode? ZipCode { get; set; }
        public virtual ICollection<Contact> InverseContactCompany { get; set; }
        public virtual ICollection<Partner> PartnerContactCompanies { get; set; }
        public virtual ICollection<Partner> PartnerPrimaryContacts { get; set; }
        public virtual ICollection<Tour> TourBrokerContacts { get; set; }
        public virtual ICollection<Tour> TourCustomerContacts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.Addresses
{
    public class SaveAddressRequestModel
    {
        public int Id { get; set; }
        public int? BenefitUserId { get; set; }
        public string? StreetName { get; set; }
        public string? StreetNumber { get; set; }
        public int? ZipCodeId { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int? PartnerId { get; set; }
        public int? EmployeeId { get; set; }
        public int? ContactId { get; set; }
        public int? FamilyMemberId { get; set; }
        public int? CompanyId { get; set; }
        public int? OrgId { get; set; }
        public int? FloorNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        public bool IsResidency { get; set; }
        public bool IsFamilyAddress { get; set; }
        public bool IsPermanentResidence { get; set; }
        public bool IsPostalReceivingAddress { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }

    }
}

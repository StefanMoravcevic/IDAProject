using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class Address
{
    public int Id { get; set; }

    public int? BenefitUserId { get; set; }

    public string? StreetName { get; set; }

    public string? StreetNumber { get; set; }

    public int? ZipCodeId { get; set; }

    public int? CityId { get; set; }

    public int? StateId { get; set; }

    public int? PartnerId { get; set; }

    public int? CompanyId { get; set; }

    public bool? UsedForReceivingDocuments { get; set; }

    public bool IsResidency { get; set; }

    public bool IsFamilyAddress { get; set; }

    public int? FloorNumber { get; set; }

    public int? ApartmentNumber { get; set; }

    public bool IsPostalReceivingAddress { get; set; }

    public bool IsPermanentResidence { get; set; }

    public int? EmployeeId { get; set; }

    public int? ContactId { get; set; }

    public DateTime? ValidFrom { get; set; }

    public DateTime? ValidUntil { get; set; }

    public int? OrgId { get; set; }

    public int? FamilyMemberId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? DeletedBy { get; set; }

    public virtual City? City { get; set; }

    public virtual Company? Company { get; set; }

    public virtual AspNetUser? DeletedByNavigation { get; set; }

    public virtual Partner? Partner { get; set; }

    public virtual State? State { get; set; }

    public virtual ZipCode? ZipCode { get; set; }
}

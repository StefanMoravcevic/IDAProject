using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class Item
{
    public int Id { get; set; }

    public string? ItemNo { get; set; }

    public string? ItemDescription { get; set; }

    public string? Description2 { get; set; }

    public string? ItemVehicleTypeCode { get; set; }

    public string? ItemVehicleTypeDescription { get; set; }

    public string? VendorNo { get; set; }

    public string? VendorName { get; set; }

    public string? CountryRegionCode { get; set; }

    public string? CountryRegionName { get; set; }

    public string? CompanyName { get; set; }

    public string? CompanyAdress { get; set; }

    public string? CompanyCity { get; set; }

    public string? CompanyPhoneNo { get; set; }

    public string? BarCode { get; set; }

    public string? CaptionOnDeclaration { get; set; }

    public string? ItemTypeCode { get; set; }

    public string? IsoStandardCode { get; set; }

    public string? IsoDescription { get; set; }

    public bool? Oil { get; set; }

    public decimal? QuantityPerDeclaration { get; set; }

    public string? ItemVendorBrandCode { get; set; }

    public string? CrossReferenceNo { get; set; }
}

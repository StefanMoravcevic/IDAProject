using System;
using System.Collections.Generic;

namespace IDAProject.Web.Db.MainDatabase;

public partial class ShippingStatus
{
    public string? SourceTransportRouteCode { get; set; }

    public string? Status { get; set; }

    public string? WarehouseShipmentNo { get; set; }

    public DateTime? SrcTranspStartDateTime { get; set; }
    public DateTime? SrcTranspCutOffDateTime { get; set; }
    public DateTime? CreationDateTime { get; set; }
    public string? PickingOrderNo { get; set; }
    public string? ItemNo { get; set; }
    public string? No { get; set; }
    public string? SrcTranspRouteDescription { get; set; }
    public string? ItemVendorBrandCode { get; set; }
    public string? CustomerNo { get; set; }
    public string? Customer { get; set; }
}

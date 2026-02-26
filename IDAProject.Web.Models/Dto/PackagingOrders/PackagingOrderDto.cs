using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.PackagingOrders
{
    public class PackagingOrderDto
    {
        public string TourCode { get; set; }
        public string PickingBinCode { get; set; }
        public string PickingNo { get; set; }
        public string ProcessingStatus { get; set; }
        public string UserName { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public DateTime? CutoffDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int? TransportPackagingCode { get; set; }
        public string? TransportPackagingType { get; set; }
        public string? CustomerNo { get; set; }
        public string? CustomerName { get; set; }
        public string? ItemNo { get; set; }
        public string? ItemDescription { get; set; }

    }
}

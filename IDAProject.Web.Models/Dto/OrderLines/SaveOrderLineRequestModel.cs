using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.OrderLines
{
    public class SaveOrderLineRequestModel
    {
        public int Id { get; set; }

        public int CustomerOrderId { get; set; }

        public int? LineNo { get; set; }

        public int? RequestedQuantity { get; set; }

        public int? CheckedQuantity { get; set; }

        public string? PartnerCode { get; set; }

        public bool IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? FebiItemId { get; set; }
        public DateTime? OrderDate { get; set; }

        public string? DayOfWeek { get; set; }

        public string? Segment { get; set; }

    }
}

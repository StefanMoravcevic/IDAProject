using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.OrderLineArchives
{
    public class SaveOrderLineArchiveRequestModel
    {
        public Int32 Id { get; set; }
public Int32 CustomerOrderId { get; set; }
public Int32? LineNo { get; set; }
public Int32? RequestedQuantity { get; set; }
public Int32? CheckedQuantity { get; set; }
public String PartnerCode { get; set; }
public Int32? FebiItemId { get; set; }
public DateTime? OrderDate { get; set; }
public String DayOfWeek { get; set; }
public String Segment { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.OrderHeaderArchives
{
    public class SaveOrderHeaderArchiveRequestModel
    {
        public Int32 Id { get; set; }
        public int? OrderHeaderId { get; set; }
        public DateTime? CreatedDate { get; set; }
public TimeOnly? CreatedTime { get; set; }
public String DeliveryRouteCode { get; set; }
public String PartnerCode { get; set; }

    }
}

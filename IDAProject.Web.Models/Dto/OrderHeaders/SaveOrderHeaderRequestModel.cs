using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.OrderHeaders
{
    public class SaveOrderHeaderRequestModel
    {
        public int Id { get; set; }
        public String CustomerOrderNumber { get; set; }
public DateTime? CreatedDate { get; set; }
//public TimeOnly? CreatedTime { get; set; }
public String DeliveryRouteCode { get; set; }
        public string? PartnerCode { get; set; }

        public bool IsArchived { get; set; }

    }
}

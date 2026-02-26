using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IDAProject.Web.Models.Dto.OrderHeaders
{
    public class CustomerOrderResponse
    {
        [JsonPropertyName("customer_order")]
        public CustomerOrder CustomerOrder { get; set; }
    }
}

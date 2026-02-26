using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IDAProject.Web.Models.Dto.OrderHeaders
{
    public class Header
    {
        [JsonPropertyName("customer_order_number")]
        public string? CustomerOrderNumber { get; set; }

        //[JsonPropertyName("date_of_order")]
        //public DateTime? DateOfOrder { get; set; }

        [JsonPropertyName("partner_code")]
        public string? PartnerCode { get; set; }

        [JsonPropertyName("transport_route")]
        public string? TransportRoute { get; set; }
    }
}

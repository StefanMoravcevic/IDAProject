using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IDAProject.Web.Models.Dto.OrderHeaders
{
    public class CustomerOrder
    {
        [JsonPropertyName("header")]
        public Header Header { get; set; }

        [JsonPropertyName("positions")]
        public List<Position> Positions { get; set; }
    }
}

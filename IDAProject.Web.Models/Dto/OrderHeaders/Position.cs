using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IDAProject.Web.Models.Dto.OrderHeaders
{
    public class Position
    {
        [JsonPropertyName("article_number")]
        public string? ArticleNumber { get; set; }


        [JsonPropertyName("external_order_position")]
        public string? ExternalOrderPosition { get; set; }

        [JsonPropertyName("requested_quantity")]
        public int? RequestedQuantity { get; set; }

        [JsonPropertyName("confirmed_quantity")]
        public int? ConfirmedQuantity { get; set; }

        [JsonPropertyName("external_article_number")]
        public string? ExternalArticleNumber { get; set; }

    }
}

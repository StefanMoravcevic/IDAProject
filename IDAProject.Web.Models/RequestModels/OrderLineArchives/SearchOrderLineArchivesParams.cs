using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.RequestModels.OrderLineArchives
{
    public class SearchOrderLineArchivesParams
    {
        public int? Id { get; set; }
        public int? OrderHeaderArchiveId { get; set; }
        public int? ArticleId { get; set; }
        public int[]? FebiItemId { get; set; }
        public int[]? ContextOrderHeaderId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? Segment { get; set; }
        public int? PartnerCode { get; set; }
        public string? BarCode { get; set; }
        public string? WintArticleNo { get; set; }
        public int? UserId { get; set; }
        public bool? IsArchived { get; set; }
        //<<SearchParams>>
    }
}

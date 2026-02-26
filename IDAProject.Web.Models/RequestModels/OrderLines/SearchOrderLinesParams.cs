using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Models.Dto.OrderLines;

namespace IDAProject.Web.Models.RequestModels.OrderLines
{
    public class SearchOrderLinesParams
    {
        public int? Id { get; set; }
        public int? OrderHeaderId { get; set; }
        public int? ArticleId { get; set; }
        public int[]? FebiItemId { get; set; }
        public int[]? ContextOrderHeaderId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int[]? Segment { get; set; }
        public int[]? PartnerCode { get; set; }
        public string? BarCode { get; set; }
        public string? WintArticleNo { get; set; }
        public int? UserId { get; set; }
        public bool? IsArchived { get; set; }
        public List<SelectedFilterModel>? SelectedFilters { get; set; }
        //<<SearchParams>>
    }
}

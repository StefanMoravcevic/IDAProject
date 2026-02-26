using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.RequestModels.FebiItems
{
    public class SearchFebiItemsParams
    {
        public int? Id { get; set; }
        public string? ArticleNo { get; set; }
        public string? BarCode { get; set; }
        public string? Keyword { get; set; }
        //<<SearchParams>>
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.RequestModels.ScannedLines
{
    public class SearchScannedLinesParams
    {
        public int? Id { get; set; }
        public int? OrderLineId { get; set; }
        public string? Keyword { get; set; }
        //<<SearchParams>>
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.RequestModels.Printers
{
    public class SearchPrintersParams
    {
        public int? Id { get; set; }
        public string? BarCode { get; set; }  

        //<<SearchParams>>
    }
}

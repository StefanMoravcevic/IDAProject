using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Models.Dto.OrderLines;

namespace IDAProject.Web.Models.Dto.Printers
{
    public class PrinterModel
    {
        public OrderLineDto? line { get; set; }
        public int? UserId { get; set; }
    }
}

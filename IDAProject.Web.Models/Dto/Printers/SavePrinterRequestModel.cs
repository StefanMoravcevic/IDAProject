using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.Printers
{
    public class SavePrinterRequestModel
    {
        public Int32 Id { get; set; }
public String Name { get; set; }
public String BarCode { get; set; }
public string Ip4Address { get; set; }
public int? Port { get; set; }

    }
}

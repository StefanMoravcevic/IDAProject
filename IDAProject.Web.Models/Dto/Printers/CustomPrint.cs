using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.Printers
{
    public class CustomPrint
    {
        public string IpAddress { get; set; } = "";
        public int Port { get; set; } = 9100;
        public string CommandData { get; set; } = "";
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.OrderLines
{
    public class SelectedFilterModel
    {
        public string Date { get; set; }
        public string Segment { get; set; }

        public DateTime ParsedDate => DateTime.ParseExact(Date, "yyyy-MM-dd", CultureInfo.CurrentCulture);
    }
}

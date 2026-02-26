using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.Periods
{
    public class SavePeriodRequestModel
    {
        public Int32 Id { get; set; }
public String Code { get; set; }
public String Name { get; set; }
public DateTime? DateFrom { get; set; }
public DateTime? DateTo { get; set; }

        public string? DateFromForFormat { get; set; }
        public string? DateToForFormat { get; set; }

    }
}

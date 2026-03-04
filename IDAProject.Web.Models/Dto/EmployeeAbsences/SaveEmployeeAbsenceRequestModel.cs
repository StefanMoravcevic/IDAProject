using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.EmployeeAbsences
{
    public class SaveEmployeeAbsenceRequestModel
    {
        public Int32 Id { get; set; }
public Int32? EmployeeId { get; set; }
public Int32? AbsenceTypeId { get; set; }
public DateTime? DateFrom { get; set; }
public DateTime? DateTo { get; set; }
public Boolean AllDay { get; set; }
public String? Comment { get; set; }
public TimeOnly? TimeFrom { get; set; }
public TimeOnly? TimeTo { get; set; }

        public string? TimeFromFormatted { get; set; }
        public string? TimeToFormatted { get; set; }
    }
}

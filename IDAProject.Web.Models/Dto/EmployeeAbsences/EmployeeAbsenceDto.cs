using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.EmployeeAbsences
{
    public class EmployeeAbsenceDto : SaveEmployeeAbsenceRequestModel
    {
        public EmployeeAbsenceDto()
        {
        }
        #region Basic data

        public string? AbsenceType { get; set; }
        public string? Group { get; set; }
        public string? Employee { get; set; }

        public string? DateFromFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(DateFrom); }
        }

        public string? DateToFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(DateTo); }
        }

        public string? TimeFromForTableFormatted
        {
            get { return TimeFrom.HasValue ? TimeFrom.Value.ToString("HH:mm") : null; }
        }

        public string? TimeToForTableFormatted
        {
            get { return TimeTo.HasValue ? TimeTo.Value.ToString("HH:mm") : null; }
        }

        #endregion
    }
}

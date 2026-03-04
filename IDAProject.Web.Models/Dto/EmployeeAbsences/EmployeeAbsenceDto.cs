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
            get { return TimeFrom.Value.ToString("HH:mm"); }
        }
        public string? TimeToForTableFormatted
        {
            get { return TimeTo.Value.ToString("HH:mm"); }
        }

        #endregion
    }
}

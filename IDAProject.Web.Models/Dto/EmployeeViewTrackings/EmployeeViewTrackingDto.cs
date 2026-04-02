using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.EmployeeViewTrackings
{
    public class EmployeeViewTrackingDto : SaveEmployeeViewTrackingRequestModel
    {
        public EmployeeViewTrackingDto()
        {
        }
        #region Basic data

        public string? ViewedEmployee { get; set; }

        public string? DateAndTimeFormatted
        {
            get { return DisplayFormatHelpers.FormatDateTime(ViewedFrom); }
        }

        #endregion
    }
}

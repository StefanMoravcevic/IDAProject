using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.Periods
{
    public class PeriodDto : SavePeriodRequestModel
    {
        public PeriodDto()
        {
        }
        #region Basic data

        public string? DateFromFormatted
        {
            get
            {
                return DateFrom.HasValue ? DateFrom.Value.ToString("dd.MM.yyyy HH:mm") : null;
            }
        }

        public string? DateToFormatted
        {
            get
            {
                return DateTo.HasValue ? DateTo.Value.ToString("dd.MM.yyyy HH:mm") : null;
            }
        }

        #endregion
    }
}

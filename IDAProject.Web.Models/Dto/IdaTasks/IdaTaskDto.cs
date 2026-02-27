using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.IdaTasks
{
    public class IdaTaskDto : SaveIdaTaskRequestModel
    {
        public IdaTaskDto()
        {
        }
        #region Basic data

        public string? Project { get; set; }

        public string? DueDateFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(DueDate); }
        }

        #endregion
    }
}

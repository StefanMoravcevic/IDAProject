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
        public string? Employee { get; set; }
        public string? Activity { get; set; }
        public string? Report { get; set; }
        public string? Status { get; set; }
        public DateTime? ProjectDueDate { get; set; }
        public DateTime? PlanDate { get; set; }

        public string? DueDateFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(DueDate); }
        }
        public string? CompletedDateFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(CompletedDate); }
        }
        public string? ProjectDueDateFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(ProjectDueDate); }
        }
        public string? PlanDateFormatted
        {
            get { return DisplayFormatHelpers.FormatDate(PlanDate); }
        }

        #endregion
    }
}

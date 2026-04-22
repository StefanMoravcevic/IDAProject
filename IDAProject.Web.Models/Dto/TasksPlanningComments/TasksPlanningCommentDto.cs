using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.TasksPlanningComments
{
    public class TasksPlanningCommentDto : SaveTasksPlanningCommentRequestModel
    {
        public TasksPlanningCommentDto()
        {
        }
        #region Basic data

        public string? Username { get; set; }
        public string? EnteredUsername { get; set; }
        public string? Photo { get; set; }
        public string? EnteredPhoto { get; set; }
        public string? DisplayTask { get; set; }
        public string? Activity { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? PlanDate { get; set; }


        public string CreatedAtFormatted
        {
            get { return DisplayFormatHelpers.FormatDateTime(CreatedAt); }
        }
        public string CreatedAtFormattedForComment
        {
            get { return DisplayFormatHelpers.FormatDate(CreatedAt); }
        }
        public string PlanDateFormatted
        {
            get { return DisplayFormatHelpers.FormatDateTime(PlanDate); }
        }
        public string PlanDateFormattedForComment
        {
            get { return DisplayFormatHelpers.FormatDate(PlanDate); }
        }

        #endregion
    }
}

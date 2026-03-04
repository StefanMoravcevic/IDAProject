using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAProject.Web.Helpers;

namespace IDAProject.Web.Models.Dto.TasksPlannings
{
    public class TasksPlanningDto : SaveTasksPlanningRequestModel
    {
        public TasksPlanningDto()
        {
        }
        #region Basic data

        public string? ActivityTypeName { get; set; }
        public string? Project { get; set; }
        public string? Task { get; set; }
        public string? RegularActivity { get; set; }
        public string? DisplayTask { get; set; }
        public string? PlanStatus { get; set; }
        public bool? IsFinished { get; set; }

        public string? TimeFromFormatted
        {
            get { return TimeFrom.Value.ToString("HH:mm"); }
        }
        public string? TimeToFormatted
        {
            get { return TimeTo.Value.ToString("HH:mm"); }
        }
        public string? DurationFormatted
        {
            get { return Duration.Value.ToString("HH:mm"); }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.TasksRealizations
{
    public class TasksRealizationDto : SaveTasksRealizationRequestModel
    {
        public TasksRealizationDto()
        {
        }
        #region Basic data

        public string? DisplayTask { get; set; }
        public string? ActivityType { get; set; }
        public bool? IsFinished { get; set; }

        public string? TimeFromFormattedForTable
        {
            get { return TimeFrom.Value.ToString("HH:mm"); }
        }
        public string? TimeToFormattedForTable
        {
            get { return TimeTo.Value.ToString("HH:mm"); }
        }
        public string? DurationFormattedForTable
        {
            get { return Duration.Value.ToString("HH:mm"); }
        }
        #endregion
    }
}

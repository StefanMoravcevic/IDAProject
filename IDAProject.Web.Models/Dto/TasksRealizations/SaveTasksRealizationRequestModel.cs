using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.TasksRealizations
{
    public class SaveTasksRealizationRequestModel
    {
        public Int32 Id { get; set; }
        public Int32? TasksPlanningId { get; set; }
        public Int32? ActivityTypeId { get; set; }
        public Int32? ProjectId { get; set; }
        public Int32? IdaTaskId { get; set; }
        public Int32? RegularActivityId { get; set; }
        public String? Activity { get; set; }
        public String? Report { get; set; }
        public TimeOnly? TimeFrom { get; set; }
        public TimeOnly? TimeTo { get; set; }
        public TimeOnly? Duration { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Boolean Finished { get; set; }
        public int? PlanNo { get; set; }        
        public int? UserId { get; set; }
        public string? TimeFromFormatted { get; set; }
        public string? TimeToFormatted { get; set; }
        public string? DurationFormatted { get; set; }
    }
}

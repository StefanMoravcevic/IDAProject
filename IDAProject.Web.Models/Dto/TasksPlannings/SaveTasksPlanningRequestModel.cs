using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.TasksPlannings
{
    public class SaveTasksPlanningRequestModel
    {
        public Int32 Id { get; set; }
public Int32? UserId { get; set; }
public Int32? ProjectId { get; set; }
public Int32? TaskId { get; set; }
public Int32? RegularActivityId { get; set; }
public Int32? ActivityTypeId { get; set; }
public String ActivityName { get; set; }
public TimeOnly? TimeFrom { get; set; }
public TimeOnly? TimeTo { get; set; }
public TimeOnly? Duration { get; set; }
public Int32? PlanNo { get; set; }
public Int32? PlanStatusId { get; set; }
public DateTime? CreatedAt { get; set; }

    }
}

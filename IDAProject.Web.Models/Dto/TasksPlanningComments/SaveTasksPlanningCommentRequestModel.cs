using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.TasksPlanningComments
{
    public class SaveTasksPlanningCommentRequestModel
    {
        public Int32 Id { get; set; }
public Int32? UserId { get; set; }
public String? Comment { get; set; }
public Int32? TaskPlanningId { get; set; }
public DateTime? CreatedAt { get; set; }
        public int? ParentTaskPlanningCommentId { get; set; }

    }
}

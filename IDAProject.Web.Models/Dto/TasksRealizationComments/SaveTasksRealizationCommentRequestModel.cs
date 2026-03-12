using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.TasksRealizationComments
{
    public class SaveTasksRealizationCommentRequestModel
    {
        public Int32 Id { get; set; }
public Int32? UserId { get; set; }
public String Comment { get; set; }
public Int32? TaskRealizationId { get; set; }
public DateTime? CreatedAt { get; set; }
public Int32? ParentTaskRealizationCommentId { get; set; }

    }
}

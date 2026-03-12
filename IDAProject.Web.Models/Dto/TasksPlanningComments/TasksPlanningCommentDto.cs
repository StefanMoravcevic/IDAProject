using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.TasksPlanningComments
{
    public class TasksPlanningCommentDto : SaveTasksPlanningCommentRequestModel
    {
        public TasksPlanningCommentDto()
        {
        }
        #region Basic data

        public string? Username { get; set; }
        public string? Photo { get; set; }

        #endregion
    }
}

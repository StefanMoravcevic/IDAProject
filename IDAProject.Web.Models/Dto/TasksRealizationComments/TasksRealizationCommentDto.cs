using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.Dto.TasksRealizationComments
{
    public class TasksRealizationCommentDto : SaveTasksRealizationCommentRequestModel
    {
        public TasksRealizationCommentDto()
        {
        }
        #region Basic data

        public string? Username { get; set; }
        public string? Photo { get; set; }

        #endregion
    }
}

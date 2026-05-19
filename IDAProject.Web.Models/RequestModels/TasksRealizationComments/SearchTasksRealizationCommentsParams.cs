using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.RequestModels.TasksRealizationComments
{
    public class SearchTasksRealizationCommentsParams
    {
        public int? Id { get; set; }
        public int? RealizationId { get; set; }
        public int? EmployeeId { get; set; }
        public int? EnteredUserId { get; set; }
        public int? ParentCommentId { get; set; }
        public bool? HideFromHomePage { get; set; }
        public bool? HideFromHomePageAuthor { get; set; }
        //<<SearchParams>>
    }
}

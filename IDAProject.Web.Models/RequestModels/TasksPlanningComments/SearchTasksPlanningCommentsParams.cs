using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.RequestModels.TasksPlanningComments
{
    public class SearchTasksPlanningCommentsParams
    {
        public int? Id { get; set; }
        public int? PlanId { get; set; }
        public int? UserId { get; set; }
        public int? EnteredUserId { get; set; }
        public int? EmployeeId { get; set; }
        public bool? HideFromHomePage { get; set; }
        public bool? HideFromHomePageAuthor { get; set; }
        public int? ParentCommentId { get; set; }
        //<<SearchParams>>
    }
}

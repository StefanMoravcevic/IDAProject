using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.RequestModels.TasksPlannings
{
    public class SearchTasksPlanningsParams
    {
        public int? Id { get; set; }
        public int? ActivityTypeId { get; set; }
        public string? CreatedDate { get; set; }
        public int? UserId { get; set; }
        public bool? Finished { get; set; }
        //<<SearchParams>>
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.RequestModels.TasksRealizations
{
    public class SearchTasksRealizationsParams
    {
        public int? Id { get; set; }
        public string? CreatedDate { get; set; }
        public int? UserId { get; set; }
        public string? StartDate { get; set; }  // dd.MM.yyyy
        public string? EndDate { get; set; }    // dd.MM.yyyy
        //<<SearchParams>>
    }
}

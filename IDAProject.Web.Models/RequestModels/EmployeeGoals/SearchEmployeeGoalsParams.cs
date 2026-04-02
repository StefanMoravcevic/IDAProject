using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.RequestModels.EmployeeGoals
{
    public class SearchEmployeeGoalsParams
    {
        public int? Id { get; set; }
        public int? EmployeeId { get; set; }
        public bool? IsActive { get; set; }
        //<<SearchParams>>
    }
}

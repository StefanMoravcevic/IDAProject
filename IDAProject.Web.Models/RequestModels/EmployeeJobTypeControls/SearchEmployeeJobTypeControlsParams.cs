using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAProject.Web.Models.RequestModels.EmployeeJobTypeControls
{
    public class SearchEmployeeJobTypeControlsParams
    {
        public int? Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? JobTypeId { get; set; }
        //<<SearchParams>>
    }
}
